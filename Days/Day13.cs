struct Game {
    public (long, long) A;
    public (long, long) B;
    public (long, long) Prize;
}

class Day13(string data) : IDay {
    readonly Game[] games = Parse(data);

    public string A() {
        long res = 0;

        foreach (Game game in games) {
            var t = FindPrize(game);

            if (t != long.MaxValue) {
                res += t;
            }
        }

        return res.ToString();
    }

    public string B() {
        long res = 0;

        foreach (Game game in games) {
            var nGame = new Game { Prize = (game.Prize.Item1 + 10000000000000, game.Prize.Item2 + 10000000000000), A = game.A, B = game.B };
            var t = FindPrize(nGame);

            if (t != long.MaxValue) {
                res += t;
            }
        }

        return res.ToString();
    }

    private static long FindPrize(Game g) {
        (long x, long y) = SolveTwoEquationSystem(g.A.Item1, g.B.Item1, g.Prize.Item1, g.A.Item2, g.B.Item2, g.Prize.Item2);

        if (g.A.Item1 * x + g.B.Item1 * y == g.Prize.Item1 && g.A.Item2 * x + g.B.Item2 * y == g.Prize.Item2) {
            return 3 * x + y;
        }

        return 0;
    }

    static (long, long) SolveTwoEquationSystem(long ax, long ay, long suma, long bx, long by, long sumb) {
        return (Solve(ax, ay, suma, bx, by, sumb), Solve(ay, ax, suma, by, bx, sumb));
    }

    static long Solve(long a, long xa, long suma, long b, long xb, long sumb) {
        long lcm = Lcm(xa, xb);

        a *= lcm / xa;
        b *= lcm / xb;

        suma *= lcm / xa;
        sumb *= lcm / xb;

        a -= b;
        suma -= sumb;

        return suma / a;
    }

    static long Lcm(long a, long b) {
        return a * b / Gcd(a, b);
    }

    static long Gcd(long a, long b) {
        long c = a % b;

        if (c == 0) {
            return b;
        }

        return Gcd(b, c);
    }

    private static long NumFilter(string s) {
        long res = 0;

        foreach (char c in s.ToCharArray()) {
            if (c >= '0' && c <= '9') {
                res *= 10;
                res += c - '0';
            }
        }

        return res;
    }

    private static Game[] Parse(string input) {
        return input
            .Split("\n\n")
            .Select(ParseGame)
            .Where(g => g.HasValue)
            .Select(g => g.Value)
            .ToArray();
    }

    private static Game? ParseGame(string g) {
        var gm = new Game();

        foreach (string l in g.Split('\n')) {
            (long, long) v;
            try {
                (v.Item1, v.Item2) = GetLineValue(l);
            } catch {
                return null;
            }
            if (l.StartsWith("Button A:")) {
                gm.A = v;
            } else if (l.StartsWith("Button B:")) {
                gm.B = v;
            } else if (l.StartsWith("Prize:")) {
                gm.Prize = v;
            }
        }

        return gm;
    }

    private static (long, long) GetLineValue(string l) {
        (long x, long y) = (long.MinValue, long.MinValue);

        foreach (string s in l.Split(' ')) {
            if (s.Contains('X')) {
                x = NumFilter(s);
            } else if (s.Contains('Y')) {
                y = NumFilter(s);
            }
        }

        if (long.MinValue == x || y == long.MinValue) {
            throw new Exception();
        }

        return (x, y);
    }
}