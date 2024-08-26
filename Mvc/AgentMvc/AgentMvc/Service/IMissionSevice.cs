using AgentMvc.Models;
using AgentMvc.ViewModel;

namespace AgentMvc.Service
{
    public interface IMissionSevice
    {

        int GetFinshedMissionCount();
        Task<MissionVM> AssignedMission(int missionId);
        int GetActiveMissionCount();
        int GetMissionCount();
        List<MissionVM> GetMissiosVM();
        double GetMissionSpeed(int agentId);
    }
}
