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
    public class MissionService(IServiceProvider serviceProvider) : IMissionService
    {

        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        
        public async Task<List<MissonModel>> GetMissions()
        {
            var _context = DbContextFactory.CreateDbContext(serviceProvider);
            var a = await _context.Missons.Include(m => m.Agent)
                .Include(m => m.Target).ToListAsync();
            if (a != null || a.Count != 0)
            {
                return a;
            }
            throw new Exception("list is null or empty");
        }

        public async Task<MissonModel> UpdateMissionStatus(int id, MissionDto missionDto)
        {
            var _context = DbContextFactory.CreateDbContext(serviceProvider);
            try
            {
                MissonModel? misson = await _context.Missons.Include(m => m.Agent).Include(m => m.Target).FirstOrDefaultAsync(x => x.Id == id);
                if (misson == null)
                {
                    throw new Exception("Misson not faound");
                }
                if (missionDto.status != "assigned")
                {
                    throw new Exception("incorect text");
                }
                if (misson.Target.InMission == StatusMission.Active)
                {
                    throw new Exception("target is alredy in mission");
                }
                if (misson.Agent.Status == StatusAgent.Active)
                {
                    throw new Exception("agent is alredt assigned to mission");
                }
                misson.Status = StatusMisson.Active;
                misson.Agent.Status = StatusAgent.Active;
                misson.Target.InMission = StatusMission.Active;
                await _context.SaveChangesAsync();
                return misson;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task CreateMission(AgentModel agent, TargetModel target)
        {
            try
            {
                await _semaphore.WaitAsync();
                await CreateMissionIfNotExixtElseUpdate(agent, target);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task CreateMissionByAgent(AgentModel agent)
        {
            var _context = DbContextFactory.CreateDbContext(serviceProvider);
            var range = GetRange(200);

            var targets = await _context.Targets.Where(t => t.Status == StatusTarget.Live && t.InMission == StatusMission.InActive).ToListAsync();

            var targetInRange = targets.Where(t => AgentIsInRange(agent.X, agent.Y, t.X, t.Y, range)).ToList();

            var tasks = targetInRange.Select(async target => await CreateMission(agent, target)).ToArray();

            Task.WaitAll(tasks);

            //await dbContext.SaveChangesAsync();
        }


        public async Task CreateMissionByTarget(TargetModel target)
        {
            var _context = DbContextFactory.CreateDbContext(serviceProvider);
            var range = GetRange(200);
            var Agents = await _context.Agents.Where(a => a.Status == StatusAgent.Sleep).ToListAsync();
            var agentInRange = Agents.Where(a => AgentIsInRange(a.X, a.Y, target.X, target.Y, range)).ToList();
            var tasks = agentInRange.Select(async a => await CreateMission(a, target)).ToArray();

            Task.WaitAll(tasks);
            await _context.SaveChangesAsync();
        }


        public async Task<List<MissonModel>> UpdateMissions()
        {
            var _context = DbContextFactory.CreateDbContext(serviceProvider);
            var activeMission = await _context.Missons
                .Where(m => m.Status == StatusMisson.Active)
                .Include(m => m.Agent)
                .Include(m => m.Target)
                .ToListAsync();
            if (activeMission != null)
            {
                activeMission.ForEach(UpdateMission);
                await _context.SaveChangesAsync();
            }
            return activeMission.Any() ? activeMission : [];
        }

        private void MissionCompletes(MissonModel mission)
        {
            mission.Status = StatusMisson.Finished;
            mission.Target.Status = StatusTarget.Dead;
            mission.Agent.Status = StatusAgent.Sleep;
            mission.Target.InMission = StatusMission.InActive;

        }

        public void UpdateAgentInMissionLocation(AgentModel agent, string dr)
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


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private async Task CreateMissionIfNotExixtElseUpdate(AgentModel agent, TargetModel target)
        {
            var _context = DbContextFactory.CreateDbContext(serviceProvider);
            var missions = await _context.Missons.ToListAsync();

            //if(AgentAndTargetInActiveMission(agent.Id, target.Id, missions)){return; }  

            var rangeAgentFromTarget = GetRangeAgentFromTarget(agent.X, agent.Y, target.X, target.Y);
            var mission = missions.Where(m => m.AgentId == agent.Id && m.TargetId == target.Id).FirstOrDefault();

            if (missions.Count != 0 && AgentAndTargetInActiveMission(agent.Id, target.Id, missions) || AgentAndTargetInMission(agent.Id, target.Id, missions))
            {
                mission.TimeRemaind = CalcTime(rangeAgentFromTarget);
                mission.EndTime = DateTime.Now.AddHours(CalcTime(rangeAgentFromTarget));
            }
            else
            {
                await _context.Missons.AddAsync(new()
                {
                    AgentId = agent.Id,
                    TargetId = target.Id,
                    TimeRemaind = CalcTime(rangeAgentFromTarget),
                    EndTime = DateTime.Now.AddHours(CalcTime(rangeAgentFromTarget))

                });

            }
            await _context.SaveChangesAsync();
        }

        private bool AgentAndTargetInActiveMission(int agentId, int targetId, List<MissonModel> missons)
        {

            return missons.Any(mission => mission.TargetId == targetId && mission.AgentId == agentId && mission.Status == StatusMisson.Active);
        }

        private void UpdateMission(MissonModel mission)
        {
            var agent = mission.Agent;
            var target = mission.Target;
            var res = ReturnDiferenceAgentFromTarget(agent.X, agent.Y, target.X, target.Y);
            var dirction = TheCorrectDirction(res.Item1, res.Item2);
            if (dirction != "")
            {
                UpdateAgentInMissionLocation(agent, dirction);
            }
            else
            {
                MissionCompletes(mission);
            }

        }

        public async Task DeleteIfNotInRange()
        {
            var _context = DbContextFactory.CreateDbContext(serviceProvider);
            var allMissions = await _context.Missons
                .Include(m => m.Agent)
                .Include(m => m.Target)
                .ToListAsync();
            //allMissions.ForEach(m => _context.Missons.Remove(m => ));
            _context.Missons.RemoveRange(allMissions.Where(m => !AgentIsInRange(m.Agent.X, m.Agent.Y, m.Target.X, m.Target.Y, 200)));
            await _context.SaveChangesAsync();
            
        }

        private bool AgentAndTargetInMission(int agentId, int targetId, List<MissonModel> missons)
        {
            return missons.Any(mission => mission.TargetId == targetId && mission.AgentId == agentId);
        }



    }
}
