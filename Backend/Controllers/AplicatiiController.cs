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
    public class AplicatiiController : ControllerBase
    {
        private readonly IAplicatii _appRepo;

        public AplicatiiController(IAplicatii appRepo)
        {
            _appRepo = appRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aplicatii>>> GetAplicatii()
        {
            try
            {
                return (await _appRepo.Get()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving applications data from database!");
            }
        }

        [HttpGet("search/{denumire}")]
        public async Task<ActionResult<Aplicatii>> GetAppByName(string denumire)
        {
            try
            {
                var result = await _appRepo.GetByDen(denumire);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving applications data from database!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aplicatii>> GetAppById(int id)
        {
            try
            {
                var result = await _appRepo.GetById(id);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving applications data from database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Aplicatii>> CreateApp([FromBody] Aplicatii apps)
        {
            try
            {
                if (apps == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _appRepo.Create(apps);
                    return CreatedAtAction(nameof(GetAplicatii), new { id = create.Idaplicatie }, create);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating application!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Aplicatii>> UpdateApp(int id, [FromBody] Aplicatii apps)
        {
            try
            {
                if (id != apps.Idaplicatie)
                {
                    return BadRequest("ID mismatch");
                }
                else
                {
                    await _appRepo.Update(apps);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating application!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _appRepo.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _appRepo.Delete(toDelete.Idaplicatie);
                return NoContent();
            }
        }
    }
}
