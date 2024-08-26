using AgentMvc.Models;

namespace AgentMvc.Service
{
    public interface IDataStore
    {
        public List<MissonModel> AllMission { get; set; }
        public List<TargetModel> AllTarget { get; set; }
        public List<AgentModel> AllAgents { get; set; }
        void LoadMissions(List<MissonModel> missons);
        void LoadTargets(List<TargetModel> targets);
        void LoadAgents(List<AgentModel> agents);
    }
}
