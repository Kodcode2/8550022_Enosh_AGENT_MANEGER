using AgentMvc.Models;
using System.Text.Json;

namespace AgentMvc.Service
{
    public class TargetService(IHttpClientFactory clientFactory, IDataStore dataStore) : ITargetService
    {
        private readonly string baseUrl = "https://localhost:7154";




        public List<TargetModel> GetTargets()
        {

            return dataStore.AllTarget;

        }

        public int GetTargetLiveCount()
        {
            return dataStore.AllTarget.Count;
        }
        public int GetTargetDeadCount()
        {
            return dataStore.AllTarget.Count(t => t.Status == StatusTarget.Dead);
        }

    }
}
