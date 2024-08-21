namespace AgentsRest.Models
{
    public class MissonModel
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int TagetId { get; set; }
        public AgentModel Agent { get; set; }
        public TargetModel Target { get; set; }


    }
}
