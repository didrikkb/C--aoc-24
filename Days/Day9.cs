class Day9(string data) : IDay
{
    public string A()
    {
        int res = 0;
        string uncompressed = "";
        bool free = false;
        int id = 0;

        foreach (char c in data)
        {
            int n = c - '0';
            if (free)
            {
                for (int i = 0; i < n; i++)
                {
                    uncompressed += ".";
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    uncompressed += (char)(id + '0');
                }
                id++;
            }
            free = !free;
        }

        return uncompressed;
    }

    public string B()
    {
        return "-1";

    }

}