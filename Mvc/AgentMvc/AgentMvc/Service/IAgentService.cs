
using AgentMvc.Models;
using AgentMvc.ViewModel;

namespace AgentMvc.Service
{
    public interface IAgentService
    {
        Task<List<AgentVM>> GetAgents();
    }
}
