struct Robot {
    public long X;
    public long Y;
    public long Vx;
    public long Vy;
}

class Day14(string data) : IDay {
    readonly Robot[] robots = Parse(data);

    public string A() {
        long rounds = 100;
        (long Xs, long Ys) = (101, 103);
        (long q0, long q1, long q2, long q3) = (0, 0, 0, 0);

        foreach (Robot r in robots) {
            long x0 = r.X;
            long y0 = r.Y;

            long dx = r.Vx < 0 ? Xs + r.Vx : r.Vx;
            long dy = r.Vy < 0 ? Ys + r.Vy : r.Vy;

            long x1 = (x0 + (dx * rounds)) % Xs;
            long y1 = (y0 + (dy * rounds)) % Ys;

            if (x1 == Xs / 2 || y1 == Ys / 2) {
                continue;
            }

            var _ = (x1 < Xs / 2, y1 < Ys / 2) switch {
                (true, true) => q0++,
                (true, false) => q1++,
                (false, true) => q2++,
                (false, false) => q3++,
            };
        }

        return (q0 * q1 * q2 * q3).ToString();
    }

    public string B() {
        (int Xs, int Ys) = (101, 103);

        for (long rounds = 0; rounds < 100000; rounds++) {
            char[,] image = new char[Xs, Ys];

            foreach (Robot r in robots) {
                long x0 = r.X;
                long y0 = r.Y;

                long dx = r.Vx < 0 ? Xs + r.Vx : r.Vx;
                long dy = r.Vy < 0 ? Ys + r.Vy : r.Vy;

                long x1 = (x0 + (dx * rounds)) % Xs;
                long y1 = (y0 + (dy * rounds)) % Ys;

                image[x1, y1] = '#';
            }

            for (int i = 0; i < Xs; i++) {
                for (int j = 0; j < Ys; j++) {
                    if (ClusterSize(image, i, j, Xs, Ys) > 100) {
                        return rounds.ToString();
                    }
                }
            }
        }

        return (-1).ToString();
    }

    private static int ClusterSize(char[,] grid, int i, int j, int x, int y) {
        if (i < 0 || j < 0 || i + 1 >= grid.GetLength(0) || j + 1 >= grid.GetLength(1)) {
            return 0;
        }
        if (grid[i, j] != '#') {
            return 0;
        }

        grid[i, j] = '.';

        return 1
            + ClusterSize(grid, i - 1, j, x, y)
            + ClusterSize(grid, i + 1, j, x, y)
            + ClusterSize(grid, i, j - 1, x, y)
            + ClusterSize(grid, i, j + 1, x, y);
    }

    private static Robot[] Parse(string data) {
        string[] lines = data.Split('\n').Where(l => l.StartsWith("p=")).ToArray();
        return lines.Select(ParseLine).ToArray();
    }

    private static Robot ParseLine(string l) {
        string[] pos = l.Split(' ').Where(x => x.StartsWith("p=")).First()[2..].Split(',');
        string[] vel = l.Split(' ').Where(x => x.StartsWith("v=")).First()[2..].Split(',');

        return new Robot { X = ParseLong(pos[0]), Y = ParseLong(pos[1]), Vx = ParseLong(vel[0]), Vy = ParseLong(vel[1]) };
    }

    private static long ParseLong(string n) {
        if (n[0] == '-') {
            return long.Parse(n[1..]) * -1;
        }

        return long.Parse(n);
    }
}