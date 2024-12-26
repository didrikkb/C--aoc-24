
class Day10(string data) : IDay {
    readonly int[][] input = ParseData(data);

    public string A() {
        List<(int, int)> trailheads = FindTrailheads(input);
        int res = 0;

        foreach ((int, int) trailhead in trailheads) {
            (int x, int y) = trailhead;
            var d = input.Select(a => a.ToArray()).ToArray();

            res += TrailheadScoreA(d, x, y);

        }

        return res.ToString();
    }

    public string B() {
        List<(int, int)> trailheads = FindTrailheads(input);
        int res = 0;

        foreach ((int, int) trailhead in trailheads) {
            (int x, int y) = trailhead;
            var d = input.Select(a => a.ToArray()).ToArray();

            res += TrailheadScoreB(d, x, y);

        }

        return res.ToString();
    }

    private static int TrailheadScoreA(int[][] data, int starti, int startj) {
        int res = 0;
        var s = new Stack<(int, int)>();

        s.Push((starti, startj));

        while (s.Count > 0) {
            (int i, int j) = s.Pop();
            int currVal = data[i][j];

            data[i][j] = -1;

            if (currVal is -1 or 9) {
                if (currVal is 9) {
                    res++;
                }
                continue;
            }

            for (int idx = 1; idx <= 4; idx++) {
                (int ni, int nj) = Directions(idx);
                (int val, bool ok) = ArrGet(data, i + ni, j + nj);

                if (ok && val == currVal + 1) {
                    s.Push((i + ni, j + nj));
                }
            }
        }

        return res;
    }

    private static int TrailheadScoreB(int[][] data, int starti, int startj) {
        int res = 0;
        var s = new Stack<(int, int)>();
        s.Push((starti, startj));

        while (s.Count > 0) {
            (int i, int j) = s.Pop();
            int currVal = data[i][j];

            if (currVal is 9) {
                res++;
                continue;
            }

            for (int idx = 1; idx <= 4; idx++) {
                (int ni, int nj) = Directions(idx);
                (int val, bool ok) = ArrGet(data, i + ni, j + nj);

                if (ok && val == currVal + 1) {
                    s.Push((i + ni, j + nj));
                }
            }
        }

        return res;
    }

    private static (int, bool) ArrGet(int[][] data, int i, int j) {
        if (i >= 0 && j >= 0 && i < data.Length && j < data[0].Length) {
            return (data[i][j], true);
        }
        return (0, false);
    }

    private static List<(int, int)> FindTrailheads(int[][] data) {
        var trailheads = new List<(int, int)>();

        for (int i = 0; i < data.Length; i++) {
            for (int j = 0; j < data.Length; j++) {
                if (data[i][j] == 0) {
                    trailheads.Add((i, j));
                }
            }
        }

        return trailheads;
    }

    private static (int, int) Directions(int n) => n switch {
        1 => (0, 1),
        2 => (1, 0),
        3 => (-1, 0),
        4 => (0, -1),
        _ => throw new Exception()
    };

    private static int[][] ParseData(string data) {
        return data
            .Split('\n')
            .Where(l => l.Length > 0)
            .Select(l => l
                .ToCharArray()
                .Select(x => x - '0')
                .ToArray())
            .ToArray();
    }
}