
using AgentMvc.Models;
using System.Net.Http;
using System.Text.Json;

namespace AgentMvc.Service
{
    public class BackroundDataService(IHttpClientFactory _clientFactory, IDataStore dataStore) : BackgroundService
    {

        private readonly string baseUrl = "https://localhost:7154";
        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                var httpClient = _clientFactory.CreateClient();
                var missionRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/Missions");
                var missionResolt = await httpClient.SendAsync(missionRequest);


                if (missionResolt.IsSuccessStatusCode)
                {
                    var content = await missionResolt.Content.ReadAsStringAsync();
                    var missions = JsonSerializer.Deserialize<List<MissonModel>>(
                    content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (missions != null)
                    {
                        dataStore.LoadMissions(missions);
                    }

                }
                var httpClient2 = _clientFactory.CreateClient();
                var targetRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/Targets");
                var targetResult = await httpClient2.SendAsync(targetRequest);
                if (targetResult.IsSuccessStatusCode)
                {
                    var content = await targetResult.Content.ReadAsStringAsync();
                    var targets = JsonSerializer.Deserialize<List<TargetModel>>(
                    content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (targets != null)
                    {
                        dataStore.LoadTargets(targets);
                    }

                }
                var httpClient3 = _clientFactory.CreateClient();
                var agentRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/Agents");
                var agentResult = await httpClient3.SendAsync(agentRequest);
                if (agentResult.IsSuccessStatusCode)
                {
                    var content = await agentResult.Content.ReadAsStringAsync();
                    var agents = JsonSerializer.Deserialize<List<AgentModel>>(
                    content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (agents != null)
                    {
                        dataStore.LoadAgents(agents);
                    }

                }
                }catch (Exception ex)
                {
                    int a = 0;
                }
                
            }
        }
    }
}
