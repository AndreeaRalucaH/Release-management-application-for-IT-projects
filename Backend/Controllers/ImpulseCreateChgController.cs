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
    public class ImpulseCreateChgController : ControllerBase
    {
        private readonly IImpulseCreate _impulse;

        public ImpulseCreateChgController(IImpulseCreate impulse)
        {
            _impulse = impulse;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ImpulseResult>> CreateImpulse([FromBody]ImpulseCreate impulse)
        {
            try
            {
                if (impulse == null)
                {
                    return BadRequest("The records are empty!");
                }
                else
                {
                    var create = await _impulse.CreateChg(impulse);
                    return create;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating impulse ticket!");
            }
        }
    }
}
