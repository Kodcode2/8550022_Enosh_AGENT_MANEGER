using AgentMvc.Models;
using System.Text.Json;
using AgentMvc.ViewModel;
using System.Text;
using System.Net.Http;
using System.Reflection;
using AgentMvc.Service;
using Microsoft.Extensions.Logging;

namespace AgentMvc.Service
{
    public class AgentService(IHttpClientFactory clientFactory, IDataStore dataStore) : IAgentService
    {
        private readonly string baseUrl = "https://localhost:7154";

        

        public List<AgentVM> GetAgents()
        {
            var agents = dataStore.AllAgents;

            var t = ConvertAgentToVm(agents); 

            return t;
        }

        public int GetAgentCount()
        {
            return dataStore.AllAgents.Count;
        }
        public int GetAgentActiveCount()
        {
            return dataStore.AllAgents.Count(a => a.Status == StatusAgent.Active);
        }
        public int GetAgentSleepCount()
        {
            return dataStore.AllAgents.Count(a => a.Status == StatusAgent.Sleep);
        }



        private List<AgentVM> ConvertAgentToVm(List<AgentModel> agents)
        {
            return agents.Select(x => new AgentVM()
            {
                Id = x.Id,
                NickName = x.NickName,
                PhotoUrl = x.PhotoUrl,
                Status = x.Status,
                X = x.X,
                Y = x.Y,
                TimeToEnd = GetMissionSpeed(x.Id),
                KillingAmount = KillsAmount(x.Id)

            }).ToList();
        }

        private double GetMissionSpeed(int agentId)
        {
            var agent = dataStore.AllMission.FirstOrDefault(m => m.AgentId == agentId && m.Status == StatusMisson.Active);
            return agent != null ? agent.TimeRemaind : 0; 
        }

        private int KillsAmount(int agentId)
        {
            var agent = dataStore.AllMission.Where(m => m.AgentId == agentId && m.Status == StatusMisson.Finished).ToList();
            var res = agent.Count != 0 ? agent.Count : 0;
            return res;
        }






    }
}
