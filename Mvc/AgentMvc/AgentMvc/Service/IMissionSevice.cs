using AgentMvc.Models;

namespace AgentMvc.Service
{
    public interface IMissionSevice
    {

        Task<List<MissonModel>> GetMissions();
    }
}
