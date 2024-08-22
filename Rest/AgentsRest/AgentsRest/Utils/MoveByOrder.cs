using System.Reflection.Metadata.Ecma335;

namespace AgentsRest.Utils
{
    public static class MoveByOrder
    {

        public static (int difrenceX, int difrenceY) ReturnDiferenceAgentFromTarget
            (int AgentX, int AgentY, int TargetX, int TargetY)
        {
            return (AgentX - TargetX, AgentY - TargetY);
        }



        public static string TheCorrectDirction(int difrenceX, int difrenceY)
        {
            string res = "";
            switch (difrenceY)
            {
                case > 0:
                    res += "n";
                    break;
                case < 0:
                    res += "s";
                    break;
            }
            switch (difrenceX)
            {
                case  > 0 :
                    res += "w";
                    break;
                case < 0:
                    res += "e";
                    break;
            }
            return res;
            /*
            if (difrenceX == difrenceY)
            {
                if (NumIsPusitive(difrenceX) && NumIsPusitive(difrenceY))
                {
                    return "nw";
                }
                else if (!NumIsPusitive(difrenceX) && !NumIsPusitive(difrenceY))
                {
                    return "se";
                }
            }
            else if (Math.Abs(difrenceX) == Math.Abs(difrenceY))
            {
                if (NumIsPusitive(difrenceX))
                {
                    return "sw";
                } 
                else if (NumIsPusitive(difrenceY))
                {
                    return "ne";
                }
            }
            else if (!NumIsPusitive(difrenceX) && !NumIsPusitive(difrenceY))
            {
                if (difrenceX < difrenceY)
                {
                    return "e";
                }
                else if (difrenceX > difrenceY)
                {
                    return "s";
                }
            }
            else if (NumIsPusitive(difrenceX) && NumIsPusitive(difrenceY))
            {
                if (difrenceX > difrenceY)
                {
                    return "w";
                }
                else if (difrenceX < difrenceY)
                {
                    return "n";
                }
            }
            else if (NumIsPusitive(difrenceX) && !NumIsPusitive(difrenceY))
            {
                if (difrenceX > Math.Abs(difrenceY))
                {
                    return "w";
                }
                else if (difrenceX < Math.Abs(difrenceY))
                {
                    return "s";
                }
            }
            else if (!NumIsPusitive(difrenceX) && NumIsPusitive(difrenceY))
            {
                if (Math.Abs(difrenceX) > difrenceY)
                {
                    return "e";
                }
                else if (Math.Abs(difrenceX) < difrenceY)
                {
                    return "w";
                }
            }*/
            throw new Exception("not valid parameter");

        }

    }
}
