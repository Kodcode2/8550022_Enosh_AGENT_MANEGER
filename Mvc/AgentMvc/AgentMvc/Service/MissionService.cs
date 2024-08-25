using AgentMvc.Models;
using AgentMvc.ViewModel;
using System.Reflection;
using System.Text.Json;

namespace AgentMvc.Service
{
    public class MissionService : IMissionSevice
    {
        private readonly string baseUrl = "https://localhost:7154/Mission";
        public List<MissonModel> _missions;
        private IHttpClientFactory _clientFactory;

        private  MissionService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<MissonModel>> GetMissions()
        {
            var httpClient = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/Agents");

            var result = await httpClient.SendAsync(request);
            List<MissonModel>? missions;

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                missions = JsonSerializer.Deserialize<List<MissonModel>>(
                content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                _missions = missions ?? [];
            }
            return _missions ?? [];
        }

        public double GetHowMenyAgentActive()
        {
            
        }
    }
}
