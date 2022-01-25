using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashTalker.Database.Repositories.ContainerRepository;
using TrashTalker.Database.Repositories.PickingRepository;
using TrashTalker.Database.Repositories.RouteRepository;
using TrashTalker.Dto.Picking;
using TrashTalker.Helpers;
using TrashTalker.Models.Enumerations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrashTalker.Controllers
{
    /// <summary>
    /// This controller manage the pickings in the system.
    /// </summary>
    [Route("api/v1/picking")]
    public class PickingController : Controller
    {
        private readonly IPickingRepository _pickingRepository;
        private readonly IContainerRepository _containerRepository;
        private readonly IRouteRepository _routeRepository;

        /// <summary>
        /// This constructor injects the picking repository to be used by the controller. 
        /// </summary>
        /// <param name="pickingRepository"></param>
        /// <param name="containerRepository"></param>
        public PickingController(IPickingRepository pickingRepository, IContainerRepository containerRepository, IRouteRepository routeRepository)
        {
            this._pickingRepository = pickingRepository;
            _containerRepository = containerRepository;
            _routeRepository = routeRepository;
        }

        /// <summary>
        /// Returns the list of pickings stored.
        /// </summary>
        /// <returns>List of <see cref="PickingDTO"/></returns>
        [HttpGet]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<IEnumerable<PickingDTO>>> getPickingsAsync()
        {
            try
            {
                return Ok((await _pickingRepository.getAllPickings()).Select(picking => picking.asDTO()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Return a picking of the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="PickingDTO"/> associated to the provided id</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<PickingDTO>> getPickingById(Guid id)
        {
            try
            {
                var picking = await _pickingRepository.getPickingId(id);
                if (picking == null)
                {
                    return NotFound();
                }

                return picking.asDTO();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Adds a new picking.
        /// </summary>
        /// <param name="createPickingDTO"></param>
        /// <returns>New <see cref="PickingDTO"/></returns>
        [HttpPost]
        [Authorize(Roles = "EMPLOYEE")]
        public async Task<ActionResult<PickingDTO>> addPicking([FromBody] CreatePickingDTO createPickingDTO)
        {
            if (createPickingDTO == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newPicking = createPickingDTO.asPicking();

                var containerPick = await _containerRepository.GetContainer(createPickingDTO.containerId);

                if (containerPick is null)
                    return BadRequest($"The container Id \"{createPickingDTO.containerId}\" does not exist");

                var routeOngoing =
                    (await _routeRepository.getRoutesByStateEmployeeLoggedIn(StatusRoute.ONGOING, getCurrentUserId())).FirstOrDefault();

                if (routeOngoing is null)
                    return BadRequest($"Nao existem Rotas em progresso!");

                var isTheContainerInTheOngoigRoute = routeOngoing.collectPoints.Any(point =>
                    point.recycleBin.containers.Any(container => container.id.Equals(containerPick.id)));

                if (!isTheContainerInTheOngoigRoute)
                    return BadRequest($"The container \"{containerPick.id}\" is not present in the Route");

                newPicking.container = containerPick;
                var resultPicking = await _pickingRepository.addPicking(newPicking, createPickingDTO.containerId);
                if (resultPicking == null)
                    return BadRequest("The container ID doesn´t exist.");
                return CreatedAtAction(nameof(getPickingById), new {newPicking.id}, newPicking.asDTO());
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