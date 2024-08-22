using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController(IAgentService agentService) : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<List<AgentModel>>> GetAgents()
        {
            try
            {
                var agents = await agentService.GetAgents();
                return Ok(agents);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> CreateTarget([FromBody] /*object body*/ AgentDto agentDto)
        {
            try
            {
                //int a = 2;
                var agent = await agentService.CreateAgent(agentDto);
                if (agent == null)
                {
                    throw new Exception("Target is null");
                }
                return Created("sucses", new IdDto() { Id = agent.Id });
                //return Ok(body);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> SetAgentLocation(int id, [FromBody] /*object body*/LocationDto locationDto)
        {
            try
            {
                var t = await agentService.SetAgentLocation(id, locationDto);
                return Created("sucses", t);
                //return Ok(body);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}/move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAgentLocation(int id, [FromBody] /*object body*/DirectionDto directionDto)
        {
            try
            {
                var t = await agentService.UpdateAgentLocation(id, directionDto);
                return Created("sucses", t);
                //return Ok(body);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }

    }
}
