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
    public class ReleaseController : ControllerBase
    {
        private readonly IRelease _relRepo;

        public ReleaseController(IRelease relRepo)
        {
            _relRepo = relRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Release>>> GetReleases()
        {
            try
            {
                return (await _relRepo.Get()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving releases data from database!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Release>> GetReleaseById(int id)
        {
            try
            {
                var result = await _relRepo.GetById(id);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving releases data from database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Release>> CreateRelease([FromBody] Release rel)
        {
            try
            {
                if (rel == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _relRepo.Create(rel);
                    return CreatedAtAction(nameof(GetReleases), new { id = create.Idrelease }, create);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating releases!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Release>> UpdateApp(int id, [FromBody] Release rel)
        {
            try
            {
                if (id != rel.Idrelease)
                {
                    return BadRequest("ID mismatch");
                }
                else
                {
                    await _relRepo.Update(rel);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating release!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _relRepo.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _relRepo.Delete(toDelete.Idrelease);
                return NoContent();
            }
        }
    }
}
