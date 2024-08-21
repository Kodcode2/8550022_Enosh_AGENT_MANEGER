
using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Service
{
    public class TargetService(ApplicationDbContext dbContext) : ITargetService
    {
        public async Task<TargetModel>? CreateTarget(TargetDto targetDto)
        {
            try
            {
                TargetModel target = new()
                {
                    Name = targetDto.name,
                    PhotoUrl = targetDto.photo_url,
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
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
