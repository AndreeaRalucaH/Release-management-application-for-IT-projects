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
    public class MediiController : ControllerBase
    {
        private readonly IMedii _mediuRepo;

        public MediiController(IMedii mediuRepo)
        {
            _mediuRepo = mediuRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medii>>> GetMedii()
        {
            try
            {
                return (await _mediuRepo.Get()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving medii data from database!");
            }
        }

        [HttpGet("search/{denumire}")]
        public async Task<ActionResult<Medii>> GetMediuByName(string denumire)
        {
            try
            {
                var result = await _mediuRepo.GetByDen(denumire);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving medii data from database!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medii>> GetMediiById(int id)
        {
            try
            {
                var result = await _mediuRepo.GetById(id);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving medii data from database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Medii>> CreateMediu([FromBody] Medii mediu)
        {
            try
            {
                if (mediu == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _mediuRepo.Create(mediu);
                    return CreatedAtAction(nameof(GetMedii), new { id = create.Idmediu }, create);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating mediu!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Medii>> UpdateMediu(int id, [FromBody] Medii mediu)
        {
            try
            {
                if (id != mediu.Idmediu)
                {
                    return BadRequest("ID mismatch");
                }
                else
                {
                    await _mediuRepo.Update(mediu);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating mediu!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _mediuRepo.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _mediuRepo.Delete(toDelete.Idmediu);
                return NoContent();
            }
        }
    }
}
