class Day1(string inputData) : IDay
{
    (int[], int[]) data = Parse(inputData);

    public string A()
    {
        (int[] row1, int[] row2) = data;

        Array.Sort(row1);
        Array.Sort(row2);

        return row1
            .Zip(row2)
            .Select(x => int.Abs(x.First - x.Second))
            .Sum()
            .ToString();
    }

    public string B()
    {
        (int[] row1, int[] row2) = data;

        var m = new Dictionary<int, int>();

        foreach (int val in row2)
        {
            m[val] = m.GetValueOrDefault(val) + 1;
        }

        return row1
            .Select(x => x * (m.TryGetValue(x, out int v) ? v : 0))
            .Sum()
            .ToString();
    }

    private static (int[], int[]) Parse(string data)
    {
        string[] lines = data.Split('\n');

        var row1 = new List<int>();
        var row2 = new List<int>();

        foreach (string line in lines)
        {
            int[] nums = line
                .Split(' ')
                .Select(e => int.TryParse(e, out int x) ? x : -1)
                .Where(x => x >= 0)
                .ToArray();

            if (nums.Length == 2)
            {
                row1.Add(nums[0]);
                row2.Add(nums[1]);
            }
        }

        return (row1.ToArray(), row2.ToArray());
    }
}