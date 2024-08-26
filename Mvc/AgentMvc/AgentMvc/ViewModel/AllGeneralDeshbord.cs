using AgentMvc.Models;

namespace AgentMvc.ViewModel
{
    public class AllGeneralDeshbord
    {
        public List<GeneralDashboard> generalDashboard { get; set; }
        public List<AgentVM> agentVMs { get; set; }
        public List<TargetModel> targets { get; set; }
    }
}
