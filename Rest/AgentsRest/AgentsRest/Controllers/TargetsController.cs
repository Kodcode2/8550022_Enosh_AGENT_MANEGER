using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetsController(IHttpClientFactory clientFactory, ITargetService targetService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<List<AgentModel>>> GetAgents()
        {
            try
            {
                var agents = await targetService.GetTargets();
                return Ok(agents);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> CreateTarget([FromBody] TargetDto targetDto)
        {
            try
            {
                var t =  await targetService.CreateTarget(targetDto);
                if (t == null)
                {
                    throw new Exception("Target is null");
                }
                return Created("sucses", new IdDto() { Id = t.Id});
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> SetTargetLocation(int id, [FromBody] LocationDto locationDto)
        {
            try
            {
                var t = await targetService.SetTargetLocation(id, locationDto);
                return Created("sucses", t);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}
