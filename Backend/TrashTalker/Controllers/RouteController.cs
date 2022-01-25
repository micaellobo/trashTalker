using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashTalker.Database.Repositories.RouteRepository;
using TrashTalker.Dto.Route;
using TrashTalker.Helpers;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;
using TrashTalker.Services;

namespace TrashTalker.Controllers
{
    /// <summary>
    /// This controller manage the routes.
    /// </summary>
    [Route("api/v1/routes")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IRouteOptimizationService _routeOptimizationService;
        private readonly IDistanceMatrixService _distanceMatrixService;

        /// <summary>
        /// This constructor inject the route repository and matrix service to be used by the route controller.
        /// </summary>
        /// <param name="routeRepository"></param>
        public RouteController(IRouteRepository routeRepository, IRouteOptimizationService routeOptimizationService,
            IDistanceMatrixService distanceMatrixService)
        {
            _routeRepository = routeRepository;
            _routeOptimizationService = routeOptimizationService;
            _distanceMatrixService = distanceMatrixService;
        }


        /// <summary>
        /// Return all route available.
        /// </summary>
        /// <returns><see cref="RouteDto"/></returns>
        [HttpGet]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutesAsync()
        {
            try
            {
                var routes = await _routeRepository.getRoutes();
                return Ok(routes.Select(route => route.asDto()).OrderBy(dto => dto.dateCriation));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Gets the Routes associated to the current Employee
        /// </summary>
        /// <returns><see cref="List{T}"/> of <see cref="Route"/></returns>
        [HttpGet("myRoutes")]
        [Authorize(Roles = "EMPLOYEE")]
        public async Task<ActionResult<IEnumerable<RouteDto>>> getRoutesEmployeeLoggedIn()
        {
            try
            {
                var routes = await _routeRepository.getRoutesEmployeeLoggedIn(getCurrentUserId());
                return Ok(routes.Select(route => route.asDto()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Return the specific route by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="RouteDto"/></returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RouteDto>> getRouteById(Guid id)
        {
            try
            {
                var route = await _routeRepository.getRoute(id);
                if (route == null)
                    return NotFound($"The Route with the id \"{id}\" does not exist");

                return route.asDto();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Return the specific route by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="RouteDto"/></returns>
        [HttpGet("myRoutes/{id:guid}")]
        [Authorize(Roles = "EMPLOYEE")]
        public async Task<ActionResult<RouteDto>> getRouteByIdEMployeeLoggedIn(Guid id)
        {
            try
            {
                var route = await _routeRepository.getRouteByIdEmployeeLoggedIn(id, getCurrentUserId());
                if (route == null)
                    return BadRequest($"The Route with id \"{id}\" does not exist");

                return route.asDto();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Adds a new route.
        /// </summary>
        /// <param name="createRouteDto"></param>
        /// <returns><see cref="RouteDto"/></returns>
        [HttpPost]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RouteDto>> addRouteAsync([FromBody] CreateRouteDto createRouteDto)
        {
            if (createRouteDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //Verify for repeated RecycleBins in the path
                if (createRouteDto.recycleBinIds.Count != createRouteDto.recycleBinIds.Distinct().Count())
                    return BadRequest($"The path of the Route has repeated RecycleBins");

                var routeDb = await _routeRepository.addRoute(createRouteDto);
                return CreatedAtAction(nameof(getRouteById), new {routeDb.id}, routeDb.asDto());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Create an automatic <see cref="Route"/> 
        /// </summary>
        /// <param name="createRouteDto"><see cref="createPendingRoute"/>Parameters to create the Route</param>
        /// <returns><see cref="RouteDto"/></returns>
        [HttpPost("auto")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RouteDto>> createPendingRoute([FromBody] CreateAutomaticRoute createRouteDto)
        {
            if (createRouteDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var routeDb = await _routeRepository.createPendingRoute(createRouteDto);

                return CreatedAtAction(nameof(getRouteById), new {routeDb.id}, routeDb.asDto());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Create an automatic <see cref="Route"/> 
        /// </summary>
        /// <param name="createRouteDto"><see cref="createPendingRoute"/>Parameters to create the Route</param>
        /// <returns><see cref="RouteDto"/></returns>
        [HttpPost("auto/{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RouteDto>> confirmeRouteCreation(Guid id)
        {
            try
            {
                var routeDb = await _routeRepository.getRoute(id);

                if (routeDb is null)
                    BadRequest("Rota não existe");

                if (routeDb.status != StatusRoute.PENDING)
                    return BadRequest($"The Route most be in status {StatusRoute.PENDING.ToString().capitalizeFirstLetter()}");

                routeDb.status = StatusRoute.PLANNED;

                await _routeRepository.confirmeRouteCreation(routeDb);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Deletes an pending <see cref="Route"/> 
        /// </summary>
        /// <param name="createRouteDto"><see cref="createPendingRoute"/>Parameters to create the Route</param>
        /// <returns><see cref="RouteDto"/></returns>
        [HttpDelete("auto/{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RouteDto>> deletePendingRoute(Guid id)
        {
            try
            {
                var routeDb = await _routeRepository.getRoute(id);

                if (routeDb is null)
                    BadRequest("Rota não existe");

                if (routeDb.status != StatusRoute.PENDING)
                    return BadRequest($"The Route most be in status {StatusRoute.PENDING.ToString().capitalizeFirstLetter()}");

                routeDb.status = StatusRoute.CANCELED;

                await this._routeRepository.deletePendingRoute(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Starts an <see cref="Route"/>
        /// </summary>
        /// <param name="id"><see cref="Guid"/> of the <see cref="Route"/> to start</param>
        /// <returns></returns>
        [HttpPut("start/{id:guid}")]
        [Authorize(Roles = "EMPLOYEE")]
        public async Task<ActionResult> startRoute(Guid id)
        {
            try
            {
                var routesOngoing = await _routeRepository.getRoutesByStateEmployeeLoggedIn(StatusRoute.ONGOING, getCurrentUserId());

                if (routesOngoing.Count > 0)
                    return BadRequest($"At this moment, the employee {User.Identity?.Name} is already carrying out a Route");

                var route = await _routeRepository.getRouteByIdEmployeeLoggedIn(id, getCurrentUserId());

                if (route is null)
                    return BadRequest($"The Route with id \"{id}\" does not exist");
                //Apenas podem ser comecadas rotas que iniciamente estavam planeadas
                if (route.status != StatusRoute.PLANNED)
                    return BadRequest($"Only is possible to start a Route that is in {StatusRoute.PLANNED.ToString().capitalizeFirstLetter()}");

                route.status = StatusRoute.ONGOING;

                await _routeRepository.updateRoute(route);
                
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Finish the specific route of given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="finishRoute"></param>
        /// <returns><see cref="RouteDto"/></returns>
        [HttpPut("finish/{id:guid}")]
        [Authorize(Roles = "EMPLOYEE")]
        public async Task<ActionResult<RouteDto>> finishRoute(Guid id, [FromBody] FinishRouteDto finishRoute)
        {
            if (finishRoute == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var route = await _routeRepository.getRoute(id);

                if (route == null)
                    return NotFound($"The Route with the id \"{id}\" does not exist");

                if (route.status != StatusRoute.ONGOING)
                    return BadRequest($"The route with id\"{route.id}\" is not being executed");

                route.distanceTravelledKm = finishRoute.distanceTravelledKm;
                route.status = StatusRoute.FINISHED;
                route.dateEnd = DateTime.Now;

                await _routeRepository.updateRoute(route);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        private Guid getCurrentUserId()
        {
            var value = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(value, out var guid);
            return guid;
        }
    }
}