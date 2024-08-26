using AgentMvc.Models;
using Humanizer;

namespace AgentMvc.ViewModel
{
    public class MissionVM
    {
        public int Id { get; set; }
        public StatusMisson Status { get; set; } = StatusMisson.NotActive;

        public int agentId {  get; set; }
        public double distance { get; set; }
        public string AgentName { get; set; }

        public int AgentX { get; set; }
        public int AgentY { get; set; }
        public string TargetName { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public double TimeRemaind { get; set; }

    }
}
