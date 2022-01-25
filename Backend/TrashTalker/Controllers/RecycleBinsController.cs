using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashTalker.Database.Repositories.ContainerRepository;
using TrashTalker.Database.Repositories.MeasurementsRepository;
using TrashTalker.Database.Repositories.RecycleBinRepository;
using TrashTalker.Dto.Container;
using TrashTalker.Dto.RecycleBin;
using TrashTalker.Helpers;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;
using TrashTalker.Services;

namespace TrashTalker.Controllers
{
    /// <summary>
    /// This controller manage the recycler bins.
    /// </summary>
    [Route("api/v1/recyclebin")]
    public class RecycleBinsController : Controller
    {
        private readonly IRecycleBinRepository _recycleBinRepository;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IGeoLocationService _geoLocationService;

        /// <summary>
        /// This constructor injects the <see cref="IRecycleBinRepository"/> to be used in this controller.
        /// </summary>
        /// <param name="repoBin"></param>
        /// <param name="measurementRepository"></param>
        /// <param name="geoLocationService"></param>
        public RecycleBinsController(IRecycleBinRepository repoBin, IMeasurementRepository measurementRepository,
            IGeoLocationService geoLocationService)
        {
            _recycleBinRepository = repoBin;
            _measurementRepository = measurementRepository;
            _geoLocationService = geoLocationService;
        }

        /// <summary>
        /// Return all recycle bins.
        /// </summary>
        /// <returns>List of <see cref="RecycleBinDTO"/></returns>
        [HttpGet("all")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<IEnumerable<RecycleBinDTO>>> getAllRecycleBin()
        {
            await _measurementRepository.addMeasurement();
            try
            {
                var allRecycleBins = await _recycleBinRepository.getAllRecycleBin();

                return Ok(allRecycleBins.Select(bin => bin.asDTO()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Adds a new <see cref="CreateRecycleBinDTO"/>.
        /// </summary>
        /// <param name="newBin"></param>
        /// <returns>The new <see cref="RecycleBinDTO"/>.</returns>
        [HttpPost]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RecycleBinDTO>> addRecycleBin([FromBody] CreateRecycleBinDTO newBin)
        {
            if (newBin == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                GeoLocation[] response;
                try
                {
                    response = (await _geoLocationService.getGeoLocation(newBin.zipCode));
                }
                catch (Exception e)
                {
                    return BadRequest($"The zip code {newBin.zipCode} is invalid");
                }

                if (response.Length == 0)
                    return BadRequest($"The zip code {newBin.zipCode} is invalid");

                var addressInfo = response[0];

                var newRecycleBin = newBin.asRecycleBin();

                newRecycleBin.country = "Portugal";
                newRecycleBin.street = addressInfo.Morada;
                newRecycleBin.city = addressInfo.Concelho;
                newRecycleBin.latit = addressInfo.Latitude;
                newRecycleBin.longit = addressInfo.Longitude;

                var resultBin = await _recycleBinRepository.addRecycleBin(newRecycleBin);


                return CreatedAtAction(nameof(getRecycleBinById), new {newRecycleBin.id}, resultBin.asDTO());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Return the <see cref="RecycleBinDTO"/> of the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="RecycleBinDTO"/></returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RecycleBinDTO>> getRecycleBinById(Guid id)
        {
            await _measurementRepository.addMeasurement();
            var resultBin = await _recycleBinRepository.getRecycleBinById(id);
            if (resultBin == null)
                return NotFound($"The RecycleBin with the id \"{id}\" does not exist");

            return Ok(resultBin.asDTO());
        }

        /// <summary>
        /// Return all active <see cref="RecycleBinDTO"/>.
        /// </summary>
        /// <returns><see cref="RecycleBinDTO"/></returns>
        [HttpGet("active")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<IEnumerable<RecycleBinDTO>>> getActiveRecycleBin()
        {
            await _measurementRepository.addMeasurement();

            var resultActiveBin = await _recycleBinRepository.getActiveRecycleBin();

            return Ok(resultActiveBin.Select(bin => bin.asDTO()));
        }

        /// <summary>
        /// Disable the specific <see cref="RecycleBinDTO"/> of the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="RecycleBinDTO"/></returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RecycleBinDTO>> disableRecycleBinAsync(Guid id)
        {
            var targetRecycleBin = await _recycleBinRepository.getRecycleBinById(id);

            var isSelectedInAnyRoute = targetRecycleBin.routes.Any(point =>
                point.route.status == StatusRoute.ONGOING || point.route.status == StatusRoute.PLANNED);

            if (isSelectedInAnyRoute)
                return BadRequest($"Este Ecoponto esta atualmente presente numa Rota");

            var targetBin = await _recycleBinRepository.disableRecycleBin(id);
            return targetBin == null
                ? NotFound($"The RecycleBin with the id \"{id}\" does not exist")
                : StatusCode(StatusCodes.Status200OK, targetBin.asDTO());
        }

        /// <summary>
        /// Update the specific <see cref="RecycleBinDTO"/> of the given id.
        /// </summary>
        /// <param name="updateBin"></param>
        /// <returns><see cref="RecycleBinDTO"/></returns>
        [HttpPut]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RecycleBinDTO>> updateRecycleBin([FromBody] UpdateRecycleBinDTO updateBin)
        {
            if (updateBin == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var targetRecycleBin = await _recycleBinRepository.getRecycleBinById(updateBin.id);

                if (targetRecycleBin == null)
                    return NotFound($"The RecycleBin with the id {updateBin.id} does not exist");

                targetRecycleBin.city = updateBin.city;
                targetRecycleBin.zipCode = updateBin.zipCode;
                targetRecycleBin.country = updateBin.country;
                targetRecycleBin.street = updateBin.street;

                var resultBin = await _recycleBinRepository.updateRecycleBin(targetRecycleBin);

                return Ok(resultBin.asDTO());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Update the specific <see cref="RecycleBinDTO"/> of the given id.
        /// </summary>
        /// <param name="updateBin"></param>
        /// <returns><see cref="RecycleBinDTO"/></returns>
        [HttpPut("active/{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<RecycleBinDTO>> activateRecBin(Guid id)
        {
            try
            {
                var targetRecycleBin = await _recycleBinRepository.getRecycleBinById(id);

                if (targetRecycleBin == null)
                    return NotFound($"The RecycleBin with the id {id} does not exist");


                targetRecycleBin.status = Status.ACTIVE;
                var resultBin = await _recycleBinRepository.updateRecycleBin(targetRecycleBin);

                return Ok(resultBin.asDTO());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}