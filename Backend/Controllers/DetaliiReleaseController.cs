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
    public class DetaliiReleaseController : ControllerBase
    {
        private readonly IDetaliiRelease _detaliiRepo;

        public DetaliiReleaseController(IDetaliiRelease detaliiRepo)
        {
            _detaliiRepo = detaliiRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detaliirelease>>> GetDetaliiRelease()
        {
            try
            {
                return (await _detaliiRepo.Get()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving detaliirelease data from database!");
            }
        }

        [HttpGet("search/{denumire}")]
        public async Task<ActionResult<IEnumerable<Detaliirelease>>> GetDetaliiReleaseByName(string denumire)
        {
            try
            {
                var result = (await _detaliiRepo.GetByDen(denumire)).ToList();
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving detaliirelease data from database!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Detaliirelease>> GetDetaliiRelById(int id)
        {
            try
            {
                var result = await _detaliiRepo.GetById(id);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving detaliirelease data from database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Detaliirelease>> CreateDetaliiRelease([FromBody] Detaliirelease detalii)
        {
            try
            {
                if (detalii == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _detaliiRepo.Create(detalii);
                    return CreatedAtAction(nameof(GetDetaliiRelease), new { id = create.Idrelease }, create);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating detaliirelease!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Detaliirelease>> UpdateProd(int id, [FromBody] Detaliirelease detalii)
        {
            try
            {
                if (id != detalii.Idrelease)
                {
                    return BadRequest("ID mismatch");
                }
                else
                {
                    await _detaliiRepo.Update(detalii);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating detaliirelease!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _detaliiRepo.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _detaliiRepo.Delete(toDelete.Idrelease);
                return NoContent();
            }
        }

        [HttpGet("searchDate/{date}")]
        public async Task<ActionResult<IEnumerable<Detaliirelease>>> GetDetaliiRelMatchDate(string date)
        {
            try
            {
                var newDate = Convert.ToDateTime(date);
                var result = await _detaliiRepo.GetMatchDate(newDate);
                if(result == null)
                {
                    return NotFound("No records found!");
                }
                else
                {
                    return result.ToArray();
                }
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieve detaliirelease match dates!");
            }
        }
    }
}
