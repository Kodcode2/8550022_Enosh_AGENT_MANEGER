using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Service
{
   
    public interface IMissionService
    {
        Task<List<MissonModel>> GetMissions();
        Task<MissonModel> UpdateMissionStatus(int id, MissionDto missionDto);
        Task? CreateMission();
        Task UpdateMissions();
    }

}
