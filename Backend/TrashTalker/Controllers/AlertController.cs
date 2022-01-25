using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashTalker.Database.Repositories.AlertRepository;
using TrashTalker.Dto.Alert;
using TrashTalker.Helpers;
using TrashTalker.Models;

namespace TrashTalker.Controllers
{
    [Route("api/v1/alert")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        /// <summary>
        /// Alert repository needed to interact with the database
        /// </summary>
        private readonly IAlertRepository _alertRepository;

        /// <summary>
        /// This constructor inject the alert repository to be use by the <see cref="Alert"/> controller.
        /// </summary>
        /// <param name="alertRepository"></param>
        public AlertController(IAlertRepository alertRepository)
        {
            this._alertRepository = alertRepository;
        }

        // GET: api/v1/alert
        [HttpGet]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<IEnumerable<AlertDTO>>> getAlertsAsync()
        {
            try
            {
                var alerts = await _alertRepository.getAlerts();
                return Ok(alerts.Select(alert => alert.asDTO()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Return the specific alert of the given id.
        /// </summary>
        /// <param name="id">id of the alert</param>
        /// <returns><see cref="AlertDTO"/></returns>
        // GET api/v1/alert/{id}
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<AlertDTO>> getAlert(Guid id)
        {
            try
            {
                var alert = await _alertRepository.getAlert(id);
                if (alert == null)
                    return NotFound();

                return Ok(alert.asDTO());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Adds a new alert.
        /// </summary>
        /// <param name="createAlertDto"></param>
        /// <returns>Created <see cref="AlertDTO"/></returns>
        // POST api/v1/alert
        [HttpPost]
        [Authorize(Roles = "EMPLOYEE")]
        public async Task<ActionResult<AlertDTO>> addAlert([FromBody] CreateAlertDTO createAlertDto)
        {
            if (createAlertDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var alert = await _alertRepository.addAlert(createAlertDto, getCurrentUserId());

                return CreatedAtAction(nameof(getAlert), new {alert.id}, alert.asDTO());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Disable the specific alert of the given id.
        /// </summary>
        /// <param name="id">id of the alert</param>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> resolveAlert(Guid id)
        {
            try
            {
                var alert = await _alertRepository.getAlert(id);
                if (alert == null)
                    return NotFound();

                await _alertRepository.resolveAlert(alert.id);

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