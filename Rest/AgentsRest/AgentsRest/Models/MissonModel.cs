namespace AgentsRest.Models
{
    public enum StatusMisson
    {
        NotActive = 0,
        Finished = 2,
        Active = 1
    }
    public class MissonModel
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int TagetId { get; set; }
        public StatusMisson Status { get; set; } = StatusMisson.NotActive;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AgentModel Agent { get; set; }
        public TargetModel Target { get; set; }


    }
}
