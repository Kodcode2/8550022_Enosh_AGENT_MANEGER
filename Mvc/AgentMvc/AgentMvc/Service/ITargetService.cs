using AgentMvc.Models;

namespace AgentMvc.Service
{
    public interface ITargetService
    {
        List<TargetModel> GetTargets();
        int GetTargetDeadCount();
        int GetTargetLiveCount();
    }
}
