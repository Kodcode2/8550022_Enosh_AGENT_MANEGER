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

        public async Task CreateMission(AgentModel agent, TargetModel target)
        {
            try
            {
                var rangeAgentFromTarget = GetRangeAgentFromTarget(agent.X, agent.Y, target.X, target.Y);
                
                target.InMission = StatusMission.Active;
                
                await dbContext.Missons.AddAsync(new()
                {
                    AgentId = agent.Id,
                    TargetId = target.Id,
                    TimeRemaind = CalcTime(rangeAgentFromTarget),
                    EndTime = DateTime.Now.AddHours(CalcTime(rangeAgentFromTarget))

                });                  
                
                await dbContext.SaveChangesAsync(); 

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task CreateMissionByAgent(AgentModel agent)
        {
            var range = GetRange(200);
            
            var targets = await dbContext.Targets.Where(t => t.Status == StatusTarget.Live && t.InMission == StatusMission.InActive).ToListAsync();

            targets.Where(t => AgentIsInRange(agent.X, agent.Y, t.X, t.Y, range)).Select(t => CreateMission(agent, t));
        }

        public async Task CreateMissionByTarget(TargetModel target)
        {
            var range = GetRange(200);
            var Agents = await dbContext.Agents.Where(a => a.Status == StatusAgent.Sleep).ToListAsync();
            Agents.Where(a => AgentIsInRange(a.X, a.Y, target.X, target.Y, range)).Select(a => CreateMission(a, target));
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
