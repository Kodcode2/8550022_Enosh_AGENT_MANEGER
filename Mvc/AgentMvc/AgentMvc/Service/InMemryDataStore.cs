using AgentMvc.Models;

namespace AgentMvc.Service
{
    public class InMemryDataStore : IDataStore
    {
        public List<MissonModel> AllMission { get; set; } = [];
        public List<TargetModel> AllTarget { get; set; } = [];
        public List<AgentModel> AllAgents { get; set; } = [];

        public void LoadAgents(List<AgentModel> agents)
        {
            AllAgents = agents;
        }

        public void LoadMissions(List<MissonModel> missons)
        {
            AllMission = missons;
        }

        public void LoadTargets(List<TargetModel> targets)
        {
            AllTarget = targets;
        }
    }
}
