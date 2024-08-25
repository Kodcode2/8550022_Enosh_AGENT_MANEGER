using AgentMvc.Models;
using System.Text.Json;
using AgentMvc.ViewModel;
using System.Text;
using System.Net.Http;
using System.Reflection;

namespace AgentMvc.Service
{
    public class AgentService : IAgentService
    {
        private readonly string baseUrl = "https://localhost:7154";

        public List<AgentModel> _agents = [];


        public async Task<List<AgentVM>> GetAgents()
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/Agents");
     
            var result = await httpClient.SendAsync(request);
            List<AgentModel>? agents;
            List<AgentVM> agentsVM = [];
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                agents = JsonSerializer.Deserialize<List<AgentModel>>(
                content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                agentsVM = ConvertAgentToVm(agents);
                _agents = agents;
            }
            return agentsVM;
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

            }).ToList();
        }
        /*private AgentVM UpdatedAgentToVm(AgentVM agent, List<MissonModel> missons)
        {
            
            var agentMission = missons.Where(missons => missons.AgentId == agent.Id).ToList();
            var mission = agentMission.FirstOrDefault(m => m.Status == StatusMisson.Active);
            agent.TimeToEnd = mission.TimeRemaind;
            agent.KillingAmount == agentMission.Where(m => m.Status == StatusMisson.Finished).ToList().Count();


        }*/

    }
}
