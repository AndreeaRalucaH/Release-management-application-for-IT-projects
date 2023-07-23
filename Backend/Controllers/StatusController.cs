using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatus _statusRepo;

        public StatusController(IStatus statusRepo)
        {
            _statusRepo = statusRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            try
            {
                return (await _statusRepo.Get()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving status data from database!");
            }
        }

        [HttpGet("search/{denumire}")]
        public async Task<ActionResult<Status>> GetStatusByName(string denumire)
        {
            try
            {
                var result = await _statusRepo.GetByDen(denumire);
                if (result == null)
                {
                    return NotFound("No records found");
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving status data from database!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatusById(int id)
        {
            try
            {
                var result = await _statusRepo.GetById(id);
                if (result == null)
                {
                    return NotFound("No records found");
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving status data from database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Status>> CreateStatus([FromBody] Status status)
        {
            try
            {
                if (status == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _statusRepo.Create(status);
                    return CreatedAtAction(nameof(GetStatus), new { id = create.Idstatus }, create);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating status!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Status>> UpdateApp(int id, [FromBody] Status status)
        {
            try
            {
                if (id != status.Idstatus)
                {
                    return BadRequest("ID mismatch");
                }
                else
                {
                    await _statusRepo.Update(status);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating status!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _statusRepo.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _statusRepo.Delete(toDelete.Idstatus);
                return NoContent();
            }
        }
    }
}
