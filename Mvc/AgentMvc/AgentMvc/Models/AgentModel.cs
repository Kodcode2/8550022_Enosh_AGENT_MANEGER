namespace AgentMvc.Models
{
    public enum StatusAgent
    {
        Sleep = 0,
        Active = 1
    }
    public class AgentModel
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string PhotoUrl { get; set; }

        public StatusAgent Status { get; set; } = StatusAgent.Sleep;
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        

    }
}
