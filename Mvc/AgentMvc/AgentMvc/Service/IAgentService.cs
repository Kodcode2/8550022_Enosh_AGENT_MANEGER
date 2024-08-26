
using AgentMvc.Models;
using AgentMvc.ViewModel;

namespace AgentMvc.Service
{
    public interface IAgentService
    {
        List<AgentVM> GetAgents();

        int GetAgentCount();
        int GetAgentActiveCount();
        int GetAgentSleepCount();


    }
}
