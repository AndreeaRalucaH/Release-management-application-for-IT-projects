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
    public class UtilizatoriController : ControllerBase
    {
        private readonly IUtilizatori _utilRepo;

        public UtilizatoriController(IUtilizatori utilRepo)
        {
            _utilRepo = utilRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizatori>>> GetUtilizatori()
        {
            try
            {
                return (await _utilRepo.Get()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving utilizatori data from database!");
            }
        }

        [HttpGet("search/{denumire}")]
        public async Task<ActionResult<Utilizatori>> GetUtilizatoriByName(string denumire)
        {
            try
            {
                var result = await _utilRepo.GetByDen(denumire);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving utilizatori data from database!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizatori>> GetUtilizatoriById(int id)
        {
            try
            {
                var result = await _utilRepo.GetById(id);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving utilizatori data from database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Utilizatori>> CreateApp([FromBody] Utilizatori util)
        {
            try
            {
                if (util == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _utilRepo.Create(util);
                    return CreatedAtAction(nameof(GetUtilizatori), new { id = create.Idutilizator }, create);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating utilizator!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Utilizatori>> UpdateApp(int id, [FromBody] Utilizatori util)
        {
            try
            {
                if (id != util.Idutilizator)
                {
                    return BadRequest("ID mismatch");
                }
                else
                {
                    await _utilRepo.Update(util);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating utilizator!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _utilRepo.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _utilRepo.Delete(toDelete.Idutilizator);
                return NoContent();
            }
        }
    }
}
