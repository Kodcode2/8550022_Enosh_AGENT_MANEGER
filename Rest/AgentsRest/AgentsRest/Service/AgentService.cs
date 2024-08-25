using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;
using static AgentsRest.Utils.DictionaryPublic;

namespace AgentsRest.Service
{
    public class AgentService(ApplicationDbContext dbContext, IMissionService missionService) : IAgentService
    {
        public async Task<AgentModel>? CreateAgent(AgentDto agentDto)
        {
            try
            {
                AgentModel agent = new()
                {
                    NickName = agentDto.nickname,
                    PhotoUrl = agentDto.PhotoUrl,

                };
                await dbContext.Agents.AddAsync(agent);
                await dbContext.SaveChangesAsync();
                return agent;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AgentModel>> GetAgents()
        {
            var a = await dbContext.Agents.ToListAsync();
            if (a != null || a.Count != 0)
            {
                return a;
            }
            throw new Exception("lisr is null or empty");
        }

        public async Task<AgentModel> SetAgentLocation(int id, LocationDto locationDto)
        {
            try
            {
                AgentModel? agent = await dbContext.Agents.FirstOrDefaultAsync(x => x.Id == id);
                if (agent == null)
                {
                    throw new Exception("not faound");
                }
                agent.X = locationDto.x;
                agent.Y = locationDto.y;
                await dbContext.SaveChangesAsync();
                await missionService.CreateMissionByAgent(agent);
                await missionService.DeleteIfNotInRange();
                return agent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AgentModel> UpdateAgentLocation(int id, DirectionDto directionDto)
        {
            try
            {
                AgentModel? agent = await dbContext.Agents.FirstOrDefaultAsync(x => x.Id == id);
                if (agent == null)
                {
                    throw new Exception("agent not faound");
                }
                if (agent.Status == StatusAgent.Active)
                {
                    throw new Exception("agent is active");
                }
                var newLocation = GetUpdateDitection(agent.X, agent.Y, directionDto.direction);
                agent.X = newLocation.Item1;
                agent.Y = newLocation.Item2;
                await missionService.CreateMissionByAgent(agent);
                await missionService.DeleteIfNotInRange();
                await dbContext.SaveChangesAsync();
                return agent;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
