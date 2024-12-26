class Day5(string inputData) : IDay {
    private readonly Dictionary<int, List<int>> rules = ParseRules(inputData);

    private readonly List<int[]> updates = inputData
        .Split("\n")
        .Where(x => x.Contains(','))
        .Select(x => x.Split(',').Select(y => int.Parse(y)).ToArray())
        .ToList();

    public string A() {
        int res = 0;

        foreach (int[] update in updates) {
            if (ValidUpdate(update) == -1) {
                res += update[update.Length / 2];
            }
        }

        return res.ToString();
    }

    public string B() {
        int res = 0;

        foreach (int[] update in updates) {
            int err = ValidUpdate(update);

            if (err == -1) {
                continue;
            }

            int[] tUpdate = update;

            while (err != -1) {
                tUpdate = FixUpdate(tUpdate, err);
                err = ValidUpdate(tUpdate);
            }

            res += tUpdate[tUpdate.Length / 2];
        }

        return res.ToString();
    }

    private int ValidUpdate(int[] update) {
        for (int i = 0; i < update.Length; i++) {
            if (rules.ContainsKey(update[i]) == false) {
                continue;
            }

            List<int> nums = rules[update[i]];

            for (int j = 0; j < i; j++) {
                if (nums.Contains(update[j]) == false) {
                    return j;
                }
            }

            for (int j = i + 1; j < update.Length; j++) {
                if (nums.Contains(update[j]) == true) {
                    return j;
                }
            }
        }

        return -1;
    }

    private static int[] FixUpdate(int[] update, int err) {
        if (err == 0) {
            (update[0], update[^1]) = (update[^1], update[0]);
        }
        else {
            (update[err - 1], update[err]) = (update[err], update[err - 1]);
        }
        return update;
    }

    private static Dictionary<int, List<int>> ParseRules(string input) {
        List<(int, int)> rules = input
            .Split("\n")
            .Where(x => x.Contains('|'))
            .Select(x => x.Split('|'))
            .Select(x => (int.Parse(x[0]), int.Parse(x[1])))
            .ToList();

        var ruleSet = new Dictionary<int, List<int>>();

        foreach ((int, int) set in rules) {
            int key = set.Item2;
            int val = set.Item1;

            if (ruleSet.ContainsKey(key) == false) {
                ruleSet.Add(key, new List<int>());
            }

            ruleSet[key].Add(val);
        }

        return ruleSet;
    }
}