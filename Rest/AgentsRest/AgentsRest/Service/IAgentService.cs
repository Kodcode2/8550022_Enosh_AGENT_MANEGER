using AgentsRest.Models;
using AgentsRest.Dto;
namespace AgentsRest.Service
{
    public interface IAgentService
    {
        
        Task<List<AgentModel>> GetAgents();
        Task<AgentModel>? CreateAgent(AgentDto agentDto);
        Task<AgentModel> SetAgentLocation(int id, LocationDto locationDto);
        Task<AgentModel> UpdateAgentLocation(int id, DirectionDto directionDto);

    }
}
