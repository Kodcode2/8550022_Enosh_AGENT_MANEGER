
using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;
using static AgentsRest.Utils.DictionaryPublic;

namespace AgentsRest.Service
{
    public class TargetService(ApplicationDbContext dbContext, IMissionService missionService) : ITargetService
    {
        public async Task<TargetModel>? CreateTarget(TargetDto targetDto)
        {
            try
            {
                TargetModel target = new()
                {
                    Name = targetDto.name,
                    PhotoUrl = targetDto.PhotoUrl,
                    Position = targetDto.position
                };
                await dbContext.Targets.AddAsync(target);
                await dbContext.SaveChangesAsync();
                return target;

            } catch (Exception ex) 
            {
                throw new Exception("");
            }
                
        }

        public async Task<List<TargetModel>> GetTargets()
        {
            var a = await dbContext.Targets.ToListAsync();
            if (a != null || a.Count != 0)
            {
                return a;
            }
            throw new Exception("lisr is null or empty");
        }

        public async Task<TargetModel> SetTargetLocation(int id, LocationDto locationDto)
        {
            try
            {
                TargetModel? target = await dbContext.Targets.FirstOrDefaultAsync(x => x.Id == id);
                if (target == null)
                {
                    throw new Exception("not faound");
                }
                target.X = locationDto.x;
                target.Y = locationDto.y;

                await dbContext.SaveChangesAsync();
                await missionService.CreateMissionByTarget(target);
                await missionService.DeleteIfNotInRange();
                await dbContext.SaveChangesAsync();
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task<TargetModel> UpdateTargetLocation(int id, DirectionDto directionDto)
        {
            try
            {
                TargetModel? target = await dbContext.Targets.FirstOrDefaultAsync(x => x.Id == id);
                if (target == null)
                {
                    throw new Exception("agent not faound");
                }
                /*if (agent.Status == StatusAgent.Active)
                {
                    throw new Exception("agent is active");
                }*/
                var newLocation = GetUpdateDitection(target.X, target.Y, directionDto.direction);
                target.X = newLocation.Item1;
                target.Y = newLocation.Item2;
                await missionService.CreateMissionByTarget(target);
                await dbContext.SaveChangesAsync();
                await dbContext.SaveChangesAsync();
                return target;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
