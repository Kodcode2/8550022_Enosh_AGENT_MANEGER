using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;
using static AgentsRest.Utils.IsInRangeUtils;
using static AgentsRest.Utils.CalculetrTimeUtils;
using static AgentsRest.Utils.MoveByOrder;
using static AgentsRest.Utils.DictionaryPublic;    

namespace AgentsRest.Service
{
    public class MissionService(ApplicationDbContext dbContext) : IMissionService
    {
        public async Task<List<MissonModel>> GetMissions()
        {
            var a = await dbContext.Missons.ToListAsync();
            if (a != null || a.Count != 0)
            {
                return a;
            }
            throw new Exception("list is null or empty");
        }

        public async Task<MissonModel> UpdateMissionStatus(int id, MissionDto missionDto)
        {
            try
            {
                MissonModel? misson = await dbContext.Missons.FirstOrDefaultAsync(x => x.Id == id);
                if (misson == null)
                {
                    throw new Exception("Misson not faound");
                }
                if (missionDto.status != "assigned")
                {
                    throw new Exception("Misson is alredy assigned");
                }
                misson.Status = StatusMisson.Active;
                await dbContext.SaveChangesAsync();
                return misson;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task? CreateMission()
        {
            try
            {
                var agents = await dbContext.Agents.Where(a => a.Status == StatusAgent.Sleep).ToListAsync();
                var targets = await dbContext.Targets.Where(t => t.Status == StatusTarget.Live).ToListAsync();
                var range = GetRange(200);
                var allmission = await dbContext.Missons.ToListAsync();
                for (int i = 0; i < agents.Count; i++)
                {
                    int agentX = agents[i].X;
                    int agentY = agents[i].Y;  
                    for (int j = 0; j < targets.Count; j++)
                    {
                        int targetX = targets[j].X;
                        int targetY = targets[j].Y;

                        if (allmission.Any(m => m.TargetId == targets[j].Id && m.AgentId == agents[i].Id))
                        {
                            continue;
                        }
                        if (AgentIsInRange(agentX, agentY, targetX, targetY, range))
                        {
                            var rangeAgentFromTarget = GetRangeAgentFromTarget(agentX, agentY, targetX, targetY);
                            await dbContext.Missons.AddAsync(new()
                            {
                                AgentId = agents[i].Id,
                                TargetId = targets[j].Id,
                                TimeRemaind = CalcTime(rangeAgentFromTarget),
                                EndTime = DateTime.Now.AddHours(CalcTime(rangeAgentFromTarget))

                            });                            
                            await dbContext.SaveChangesAsync();
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task UpdateMissions()
        {
            var activeMission = await dbContext.Missons
                .Where(m => m.Status == StatusMisson.Active)
                .ToListAsync();

            foreach (var mission in activeMission)
            {
                var agent = mission.Agent;
                var target = mission.Target;
                var res = ReturnDiferenceAgentFromTarget(agent.X, agent.Y, target.X, target.Y);
                var dir = TheCorrectDirction(res.Item1, res.Item2);
                await UpdateAgentInMissionLocation(agent, dir);
            }

            await dbContext.Missons.Select(m => MissionCompletes(m)).ToListAsync();
            await dbContext.SaveChangesAsync();
        }

        public async Task MissionCompletes(MissonModel misson)
        {
            try
            {
                if (misson.Target.X == misson.Agent.X && misson.Target.Y == misson.Agent.Y)
                {
                    misson.Status = StatusMisson.Finished;
                    misson.Target.Status = StatusTarget.Dead;
                    misson.Agent.Status = StatusAgent.Sleep;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AgentModel> UpdateAgentInMissionLocation(AgentModel agent, string dr)
        {
            try
            {
                if (agent == null)
                {
                    throw new Exception("agent not faound");
                }  
                var newLocation = GetUpdateDitection(agent.X, agent.Y, dr);
                agent.X = newLocation.Item1;
                agent.Y = newLocation.Item2;
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
