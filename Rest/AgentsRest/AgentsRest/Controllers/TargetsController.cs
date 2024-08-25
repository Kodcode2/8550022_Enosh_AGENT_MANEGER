using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TargetsController(ITargetService targetService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<List<TargetModel>>> GetTargets()
        {
            try
            {
                var targets = await targetService.GetTargets();
                return Ok(targets);
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

        [HttpPut("{id}/move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAgentLocation(int id, [FromBody] DirectionDto directionDto)
        {
            try
            {
                var t = await targetService.UpdateTargetLocation(id, directionDto);
                return Created("sucses", t);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }



    }
}
