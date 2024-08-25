using AgentMvc.Models;

namespace AgentMvc.ViewModel
{
    public class AgentVM
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string PhotoUrl { get; set; }
        public StatusAgent Status { get; set; } = StatusAgent.Sleep;
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public string MissionLink { get; set; } = "#";
        public double TimeToEnd { get; set; } = 0;
        public int KillingAmount { get; set; } = 0;
    }
}
