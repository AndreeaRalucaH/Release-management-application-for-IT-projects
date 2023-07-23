using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DurateReleaseController : ControllerBase
    {
        private readonly IDurateRelease _durateRepo;

        public DurateReleaseController(IDurateRelease durateRepo)
        {
            _durateRepo = durateRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Duraterelease>>> GetDurate()
        {
            try
            {
                return (await _durateRepo.Get()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving duraterelease data from database!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Duraterelease>> GetDurataById(int id)
        {
            try
            {
                var result = await _durateRepo.GetById(id);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving durate data from database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Duraterelease>> CreateDurata([FromBody] Duraterelease durate)
        {
            try
            {
                if (durate == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _durateRepo.Create(durate);
                    return CreatedAtAction(nameof(GetDurate), new { id = create.Iddurata }, create);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating duraterelease!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Duraterelease>> UpdateDurata(int id, [FromBody] Duraterelease durate)
        {
            try
            {
                if (id != durate.Iddurata)
                {
                    return BadRequest("ID mismatch");
                }
                else
                {
                    await _durateRepo.Update(durate);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating duraterelease!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _durateRepo.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _durateRepo.Delete(toDelete.Iddurata);
                return NoContent();
            }
        }
    }
}
