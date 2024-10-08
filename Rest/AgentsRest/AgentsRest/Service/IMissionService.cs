﻿using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Service
{
   
    public interface IMissionService
    {
        Task<List<MissonModel>> GetMissions();
        Task<MissonModel> UpdateMissionStatus(int id, MissionDto missionDto);
        Task CreateMissionByTarget(TargetModel target);
        Task CreateMissionByAgent(AgentModel agent);
        Task<List<MissonModel>> UpdateMissions();
        Task DeleteIfNotInRange();


    }

}
