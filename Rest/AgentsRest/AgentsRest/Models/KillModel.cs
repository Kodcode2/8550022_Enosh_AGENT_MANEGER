namespace AgentsRest.Models
{
    public class KillModel
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int TagetId { get; set; }

        public AgentModel Agent { get; set; }

        public DateTime StartMisson { get; set; }
    }
}
