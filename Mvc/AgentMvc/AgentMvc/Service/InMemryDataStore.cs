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

        public List<List<List<string>>> Metrix(List<TargetModel> targets, List<AgentModel> agents)
        {
            List<List<List<string>>> metrixList = [];
            for (int i = 0; i < 100; i++)
            {
                metrixList.Add([]);
            }
            foreach (var metrix in metrixList)
            {
                for (int i = 0; i < 100; i++)
                {
                    metrix.Add([]);
                }
            }
            targets.ForEach(t =>
            {
                if (t.Y < 100 && t.X < 100)
                {
                    
                metrixList[t.Y][t.X].Add("T");
                }
            });
            agents.ForEach(t =>
            {
                if (t.Y < 100 && t.X < 100)
                {
                    metrixList[t.Y][t.X].Add("A");
                }
            });
            return metrixList;
        }
    }
}
