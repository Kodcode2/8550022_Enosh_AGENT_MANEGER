using AgentMvc.Dto;
using AgentMvc.Models;
using AgentMvc.ViewModel;
using System.Reflection;
using System.Text.Json;

namespace AgentMvc.Service
{
    public class MissionService(IHttpClientFactory _clientFactory, IDataStore dataStore) : IMissionSevice
    {
        private readonly string baseUrl = "https://localhost:7154";

        

        public List<MissionVM> GetMissiosVM()
        {
            var _missions = dataStore.AllMission;

            return _missions.Select(ConvertToVm).ToList();
        }

        private MissionVM ConvertToVm(MissonModel model)
        {
            return new MissionVM {
                Id = model.Id,
                agentId = model.AgentId,
                Status = model.Status,
                distance = model.TimeRemaind * 5,
                AgentName = model.Agent.NickName,
                AgentX = model.Agent.X,
                AgentY = model.Agent.Y,
                TargetName = model.Target.Name ,
                TargetX = model.Target.X ,
                TargetY = model.Target.Y,
                TimeRemaind = model.TimeRemaind
             };
        }


        public int GetMissionCount()
        {
            return dataStore.AllMission.Count;
        }

        public int GetActiveMissionCount()
        {
            return dataStore.AllMission.Count(m => m.Status == StatusMisson.Active);
        }
        public int GetFinshedMissionCount()
        {
            return dataStore.AllMission.Count(m => m.Status == StatusMisson.Finished);
        }

        public async Task<MissionVM?> AssignedMission(int missionId)
        {
            var httpClient = _clientFactory.CreateClient();
            var missionRequest = new HttpRequestMessage(HttpMethod.Put, $"{baseUrl}/Missions/{missionId}");

            missionRequest.Content = JsonContent.Create(new MissionDto { status = "assigned" });

            var missionResolt = await httpClient.SendAsync(missionRequest);
            if (missionResolt.IsSuccessStatusCode)
            {
                return GetMissiosVM().FirstOrDefault(m => m.Id == missionId);
            }
            return null;
        }

        public double GetMissionSpeed(int agentId)
        {
            return GetMissiosVM().FirstOrDefault(m => m.agentId == agentId).TimeRemaind;
        }
        public int GetKillsAmount(int agentId)
        {
            return GetMissiosVM().Where(m => m.agentId == agentId).Count(a => a.Status == StatusMisson.Finished);
        }
    }
}
