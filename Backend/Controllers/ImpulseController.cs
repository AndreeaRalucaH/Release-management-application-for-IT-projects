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
    public class ImpulseController : ControllerBase
    {
        private readonly IImpulse _impulseRepo;

        public ImpulseController(IImpulse impulseRepo)
        {
            _impulseRepo = impulseRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Impulse>>> GetImpulse()
        {
            try
            {
                return (await _impulseRepo.Get()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving impulse data from database!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Impulse>> GetImpulseById(int id)
        {
            try
            {
                var result = await _impulseRepo.GetById(id);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving impulse data from database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Impulse>> CreateImpulse([FromBody] Impulse imp)
        {
            try
            {
                if (imp == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _impulseRepo.Create(imp);
                    return CreatedAtAction(nameof(GetImpulse), new { id = create.Idrelease }, create);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating impulse!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Impulse>> UpdateApp(int id, [FromBody] Impulse imp)
        {
            try
            {
                if (id != imp.Idrelease)
                {
                    return BadRequest("ID mismatch");
                }
                else
                {
                    await _impulseRepo.Update(imp);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating impulse!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _impulseRepo.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _impulseRepo.Delete(toDelete.Idrelease);
                return NoContent();
            }
        }
    }
}
