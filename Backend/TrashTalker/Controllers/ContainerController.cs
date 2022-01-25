using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashTalker.Database.Repositories.ContainerRepository;
using TrashTalker.Database.Repositories.MeasurementsRepository;
using TrashTalker.Dto.Container;
using TrashTalker.Helpers;
using TrashTalker.Models;
using TrashTalker.Services;

namespace TrashTalker.Controllers
{
    /// <summary>
    /// This controller manage the containers in the system.
    /// </summary>
    [Route("api/v1/container")]
    public class ContainerController : ControllerBase
    {
        private readonly IContainerRepository _containerRepository;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IQrCodeService _qrCodeService;

        /// <summary>
        /// This controller inject the container and measurement repository to be used by the controller.
        /// </summary>
        /// <param name="containerRepository"></param>
        /// <param name="measurementRepository"></param>
        /// <param name="qrCodeService"></param>
        public ContainerController(IContainerRepository containerRepository, IMeasurementRepository measurementRepository,
            IQrCodeService qrCodeService)
        {
            _containerRepository = containerRepository;
            _measurementRepository = measurementRepository;
            _qrCodeService = qrCodeService;
        }

        /// <summary>
        /// Returns the list of available containers.
        /// </summary>
        /// <returns>List of <see cref="ContainerDTO"/></returns>
        [HttpGet]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<IEnumerable<ContainerDTO>>> getContainersAsync()
        {
            await _measurementRepository.addMeasurement();
            try
            {

                for (int i = 200; i < 236; i++)
                {
                    _qrCodeService.generateQRCode(Guid.Parse($"07ebcea5-8b50-4960-92f9-f1b72d8aa{i}"));
                }
                
                var result = await _containerRepository.GetContainers();
                var cast = result.Select(container => container.asDTO());
                return Ok(cast);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Return a specific container by id.
        /// </summary>
        /// <param name="id"> The <see cref="Guid"/> of the <see cref="Container"/></param>
        /// <returns><see cref="ContainerDTO"/> associated to the provided <see cref="Guid"/></returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<ContainerDTO>> getContainer(Guid id)
        {
            await _measurementRepository.addMeasurement();
            try
            {
                var container = await _containerRepository.GetContainer(id);
                if (container == null)
                    return NotFound();

                return container.asDTO();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Adds a new container to a specific recycle bin.
        /// </summary>
        /// <param name="createContainerDto"></param>
        /// <returns>New <see cref="ContainerDTO"/></returns>
        [HttpPost]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<ContainerDTO>> addContainer([FromBody] CreateContainerDTO createContainerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                if (createContainerDto == null)
                    return BadRequest();

                var container = createContainerDto.asContainer();

                var createdCont = await _containerRepository.addContainer(container, createContainerDto.idRecBin);

                if (createdCont == null)
                    return BadRequest("The Recycle Bin ID doesn´t exist.");

                _qrCodeService.generateQRCode(container.id);

                return CreatedAtAction(nameof(getContainer), new {container.id}, container.asDTO());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Update the container of the given id. 
        /// </summary>
        /// <param name="updateContainerDto"></param>
        /// <param name="id"></param>
        /// <returns>Updated <see cref="ContainerDTO"/></returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<ContainerDTO>> UpdateContainer(UpdateContainerDTO updateContainerDto, Guid id)
        {
            try
            {
                var container = await _containerRepository.GetContainer(id);
                if (container == null)
                    return NotFound();

                container.height = updateContainerDto.height;
                container.width = updateContainerDto.width;
                container.depth = updateContainerDto.depth;

                return (await _containerRepository.UpdateContainer(container)).asDTO();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Disable the container of the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ContainerDTO"/> disabled</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> disableContainer(Guid id)
        {
            try
            {
                var container = await _containerRepository.GetContainer(id);
                if (container is null)
                    return NotFound();

                await _containerRepository.disableContainer(container.id);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Returns a list of containers on alert
        /// </summary>
        /// <returns><see cref="List{T}" /> of <see cref="ContainerDTO" /></returns>
        [HttpGet("containersOnAlert")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<List<ContainerAlertDTO>>> getContainersOnAlert()
        {
            var containerAlertDtos = (await _containerRepository.GetContainersOnAlert()).Select(c => c.asAlertDTO());
            var ordered = containerAlertDtos.OrderByDescending(c => c.percentageOccupied);
            return ordered.ToList();
        }
        
    }
}