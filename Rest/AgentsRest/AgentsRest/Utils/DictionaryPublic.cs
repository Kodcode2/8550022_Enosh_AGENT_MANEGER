namespace AgentsRest.Utils
{
    public static class DictionaryPublic
    {
        public static Dictionary<string, Dictionary<string, int>> DictDirection = new() {
        {"nw", new (){{"xPlus", -1 },{"yPlus", -1}}},
        {"n", new(){{ "xPlus", 0 },{ "yPlus", -1}}},
        {"ne", new(){{ "xPlus", 1 },{ "yPlus", -1}}},
        {"e", new(){{ "xPlus", 1 },{ "yPlus", 0}}},
        {"se", new(){{ "xPlus", 1 },{ "yPlus", 1}}},
        {"s", new(){{ "xPlus", 0 },{ "yPlus", 1}}},
        {"sw", new(){{ "xPlus", -1 },{ "yPlus", 1}}},
        {"w", new(){{ "xPlus", -1 },{ "yPlus", 0}}},
    };

        public static bool NumberOutOfRange(int num) =>
            num > 1000 || num < 0 || num == -1;

        public static (int, int) GetUpdateDitection(int x, int y, string direction)
        {
            if (NumberOutOfRange(x) || NumberOutOfRange(y))
            {
                throw new Exception("number as not set yet!!");
            }
            var dict = DictDirection[direction];
            int newX = x + dict["xPlus"];
            int newY = y + dict["yPlus"];
            if (NumberOutOfRange(newX) || NumberOutOfRange(newY))
            {
                throw new Exception("Number Out Of Range");
            }
            return (newX, newY);
        }
    }
}
