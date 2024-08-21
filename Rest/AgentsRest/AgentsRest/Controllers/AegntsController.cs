using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AegntsController(IHttpClientFactory clientFactory, IAgentService agentService) : Controller
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

        public async Task<ActionResult> CreateTarget([FromBody] AgentDto agentDto)
        {
            try
            {
                var agent = await agentService.CreateAgent(agentDto);
                if (agent == null)
                {
                    throw new Exception("Target is null");
                }
                return Created("sucses", new IdDto() { Id = agent.Id });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> SetAgentLocation(int id, [FromBody] LocationDto locationDto)
        {
            try
            {
                var t = await agentService.SetAgentLocation(id, locationDto);
                return Created("sucses", t);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
