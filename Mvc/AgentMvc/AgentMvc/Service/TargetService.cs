using AgentMvc.Models;
using System.Text.Json;

namespace AgentMvc.Service
{
    public class TargetService(IHttpClientFactory clientFactory) : ITargetService
    {
        private readonly string baseUrl = "https://localhost:7154/Targets";

        public async Task<List<TargetModel>> GetTargets()
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}");
            
            var result = await httpClient.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                List<TargetModel>? Targets = JsonSerializer.Deserialize<List<TargetModel>>(
                content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return Targets ?? [];
            }
            throw new Exception("the reqwest is not complete");
        }
    }
}
