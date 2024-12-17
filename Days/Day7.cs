class Day7(string inputData) : IDay
{
    private readonly List<(long, List<long>)> data = ParseInput(inputData);

    public string A()
    {
        long res = 0;

        foreach ((long, List<long>) set in data)
        {
            (long sum, List<long> nums) = set;

            if (CalcA(nums[1..], sum, nums[0]))
            {
                res += sum;
            }
        }

        return res.ToString();
    }

    public string B()
    {
        long res = 0;

        foreach ((long, List<long>) set in data)
        {
            (long sum, List<long> nums) = set;

            if (CalcB(nums[1..], sum, nums[0]))
            {
                res += sum;
            }
        }

        return res.ToString();
    }

    private static bool CalcA(List<long> nums, long target, long curr)
    {
        if (curr > target || nums.Count == 0)
        {
            return curr == target;
        }

        return CalcA(nums[1..], target, curr + nums[0]) || CalcA(nums[1..], target, curr * nums[0]);
    }

    private static bool CalcB(List<long> nums, long target, long curr)
    {
        if (curr > target || nums.Count == 0)
        {
            return curr == target;
        }

        (long n, int l) = Reverse(nums[0]);
        long tCurr = curr;

        while (l-- > 0)
        {
            tCurr *= 10;
            tCurr += n % 10;
            n /= 10;
        }

        return CalcB(nums[1..], target, tCurr) || CalcB(nums[1..], target, curr + nums[0]) || CalcB(nums[1..], target, curr * nums[0]);
    }

    private static (long, int) Reverse(long n)
    {
        long m = 0;
        int count = 0;
        while (n > 0)
        {
            count++;
            m *= 10;
            m += n % 10;
            n /= 10;
        }
        return (m, count);
    }

    private static List<(long, List<long>)> ParseInput(string rawData) =>
        rawData
            .Split("\n")
            .Where(l => l.Contains(':'))
            .Select(l => l
                .Split(':')
                .ToArray())
            .Select(x => (long.Parse(x[0]), x[1]
                .Split(' ')
                .Where(x => long.TryParse(x, out long _))
                .Select(x => long.Parse(x))
                .ToList()))
            .ToList();
}