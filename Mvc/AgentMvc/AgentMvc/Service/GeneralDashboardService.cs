using AgentMvc.Models;
using AgentMvc.Service;
using AgentMvc.ViewModel;

namespace AgentMvc.Service
{
    public class GeneralDashboardService(
        IMissionSevice missionSevice, 
        IAgentService agentService, 
        ITargetService targetService
       ,IDataStore dataStore) : IGeneralDashboardService
    {
        public AllGeneralDeshbord GetGeneralDashboard()
        {
            var generalDashboard = new GeneralDashboard()
            {
                SumAgents = agentService.GetAgentCount(),
                SumAgentsActive = agentService.GetAgentActiveCount(),
                SumTargets = targetService.GetTargetLiveCount(),
                SumTargetsKilled = targetService.GetTargetDeadCount(),
                SumMissions = missionSevice.GetMissionCount(),
                SumMissionsAssigned = missionSevice.GetActiveMissionCount(),
                CompareAgentsToTargets = (double)agentService.GetAgentCount() / targetService.GetTargetLiveCount(),
                CompareAgentsDormantsToTargets = (double)agentService.GetAgentSleepCount() / targetService.GetTargetLiveCount()
            };
            AllGeneralDeshbord allGeneral = new()
            {
                generalDashboard = [generalDashboard],
                agentVMs = agentService.GetAgents(),
                targets = targetService.GetTargets()
            };
            return allGeneral;  
        }
    }
}
