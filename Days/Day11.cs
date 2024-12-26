#pragma warning disable IDE0028

class Day11(string data) : IDay {
    readonly Dictionary<(int, long), long> m = new();

    public string A() {
        var stones = Parse(data);
        long res = 0;

        foreach (long stone in stones) {
            res += F(stone, 25);
        }

        return res.ToString();
    }


    public string B() {
        var stones = Parse(data);
        long res = 0;

        foreach (long stone in stones) {
            res += F(stone, 75);
        }

        return res.ToString();
    }


    private long F(long n, int lvl) {
        if (lvl == 0) {
            return 1;
        }

        if (m.TryGetValue((lvl, n), out long v)) {
            return v;
        }

        long res;

        if (n == 0) {
            res = F(n + 1, lvl - 1);
        }
        else if (LongCount(n) % 2 == 0) {
            (long l, long r) = LongSplit(n);
            res = F(l, lvl - 1) + F(r, lvl - 1);
        }
        else {
            res = F(n * 2024, lvl - 1);
        }

        m.Add((lvl, n), res);

        return res;
    }

    private static (long, long) LongSplit(long n) {
        long len = LongCount(n);
        long d = 1;

        for (int i = 0; i < len / 2; i++) {
            d *= 10;
        }

        return (n / d, n % d);
    }

    private static int LongCount(long n) {
        int res = 0;

        while (n > 0) {
            res++;
            n /= 10;
        }

        return res;
    }

    private static List<long> Parse(string input) {
        return input
            .Split(' ')
            .Where(x => Int64.TryParse(x, out long _))
            .Select(Int64.Parse)
            .ToList();
    }
}