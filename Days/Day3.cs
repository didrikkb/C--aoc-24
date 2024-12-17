class Day3(string inputData) : IDay
{
    private readonly string data = inputData;

    public string A()
    {
        var list = data.ToList();
        int res = 0;

        while (list.Count > 0)
        {
            (int num, list) = list switch
            {
            ['m', 'u', 'l', '(', .. var rest] => (Parse(rest), rest),
                _ => (0, list[1..])
            };

            res += num;
        }

        return res.ToString();
    }

    public string B()
    {
        var list = data.ToList();
        int res = 0;
        bool skip = false;

        while (list.Count > 0)
        {
            (skip, int num, list) = list switch
            {
            ['m', 'u', 'l', '(', .. var rest] when skip == false => (skip, Parse(rest), rest),
            ['d', 'o', '(', ')', .. var rest] => (false, 0, rest),
            ['d', 'o', 'n', '\'', 't', '(', ')', .. var rest] => (true, 0, rest),
                _ => (skip, 0, list[1..])
            };

            res += num;
        }

        return res.ToString();
    }

    private static int Parse(List<char> list)
    {
        (int num1, list) = ReadNum(list);

        if (list[0] != ',')
        {
            return 0;
        }

        (int num2, list) = ReadNum(list[1..]);

        if (list[0] != ')')
        {
            return 0;
        }

        return num1 * num2;
    }

    private static (int, List<char>) ReadNum(List<char> l) => l switch
    {
    [>= '0' and <= '9', >= '0' and <= '9', >= '0' and <= '9', .. var rest] => ((l[0] - '0') * 100 + (l[1] - '0') * 10 + (l[2] - '0'), rest),
    [>= '0' and <= '9', >= '0' and <= '9', .. var rest] => ((l[0] - '0') * 10 + (l[1] - '0'), rest),
    [>= '0' and <= '9', .. var rest] => (l[0] - '0', rest),
        _ => (0, l)
    };
}