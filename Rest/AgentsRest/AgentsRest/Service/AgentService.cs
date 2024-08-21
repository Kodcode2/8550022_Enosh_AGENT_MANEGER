using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Service
{
    public class AgentService(ApplicationDbContext dbContext) : IAgentService
    {
        public async Task<AgentModel>? CreateAgent(AgentDto agentDto)
        {
            try
            {
                AgentModel agent = new()
                {
                    NickName = agentDto.nickname,
                    PhotoUrl = agentDto.photo_url,

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
                return agent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
