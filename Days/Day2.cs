class Day2(string inputData) : IDay {
    private readonly List<List<int>> data = Parse(inputData);

    public string A() {
        int res = 0;

        foreach (List<int> nums in data) {
            if (IsGradual(nums)) {
                res++;
            }
        }

        return res.ToString();
    }

    public string B() {
        int res = 0;

        foreach (List<int> nums in data) {
            if (IsGradual(nums) || IsGradualDampened(nums)) {
                res++;
            }
        }

        return res.ToString();
    }

    private static bool IsGradualDampened(List<int> nums) {
        for (int i = 0; i < nums.Count; i++) {
            int t = nums[i];

            nums.RemoveAt(i);

            if (IsGradual(nums)) {
                return true;
            }

            nums.Insert(i, t);
        }

        return false;
    }

    private static bool IsGradual(List<int> nums) {
        if (nums.Count <= 1) {
            return true;
        }

        bool incr = nums[1] - nums[0] >= 0;

        for (int i = 1; i < nums.Count; i++) {
            int diff = nums[i] - nums[i - 1];
            int abs = int.Abs(diff);

            if (diff >= 0 != incr || abs < 1 || abs > 3) {
                return false;
            }
        }
        return true;
    }

    private static List<List<int>> Parse(string data) {
        List<string> lines = data.Split('\n').Where(l => l.Length > 0).ToList();
        var res = new List<List<int>>();

        foreach (string line in lines) {
            List<int> temp = line
                .Split(' ')
                .Where(x => int.TryParse(x, out int _))
                .Select(x => int.Parse(x))
                .ToList();

            res.Add(temp);
        }

        return res;
    }
}