using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Service
{
    public interface ITargetService
    {
        Task<List<TargetModel>> GetTargets();
        Task<TargetModel>? CreateTarget(TargetDto targetDto);
        Task<TargetModel> SetTargetLocation(int id, LocationDto locationDto);
    }
}
