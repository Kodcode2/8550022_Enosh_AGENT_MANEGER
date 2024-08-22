using static AgentsRest.Utils.MoveByOrder;
namespace AgentsRest.Utils
{
    public static class IsInRangeUtils
    {
        public static double GetRange(double range)
        {
            var r = Math.Sqrt(Math.Pow(range, 2));
            return r;
        }
        public static double GetRangeAgentFromTarget(int AgentX, int AgentY, int TargetX, int TargetY)
        {
            var distance = Math.Sqrt((Math.Pow(AgentX - TargetX, 2) + Math.Pow(AgentY - AgentY, 2)));
            return distance;
        }
        public static bool AgentIsInRange(int AgentX, int AgentY, int TargetX, int TargetY, double range)
        {
            var distance = Math.Sqrt((Math.Pow(AgentX - TargetX, 2) + Math.Pow(AgentY - AgentY, 2)));
            if (distance < 0)
            { throw new Exception("x or y i not valid"); }
            if (distance <= range)
            { return true; }
            return false;
        }
    }
}

