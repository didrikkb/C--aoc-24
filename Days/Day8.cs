class Day8(string data) : IDay
{
    public string A()
    {
        (Dictionary<char, List<(int, int)>> antennaTypes, (int si, int sj)) = Parse(data);
        char[,] grid = new char[si, sj];

        foreach (var antennas in antennaTypes)
        {
            List<(int, int)> posistions = antennas.Value;

            for (int i = 0; i < posistions.Count - 1; i++)
            {
                (int x0, int y0) = (posistions[i].Item1, posistions[i].Item2);

                for (int j = i + 1; j < posistions.Count; j++)
                {
                    (int x1, int y1) = (posistions[j].Item1, posistions[j].Item2);
                    (int dx, int dy) = (x1 - x0, y1 - y0);

                    (grid, bool _) = AddAntinodeToGrid(grid, x0 - dx, y0 - dy);
                    (grid, bool _) = AddAntinodeToGrid(grid, x1 + dx, y1 + dy);
                }
            }
        }

        return CountAntinodes(grid).ToString();
    }

    public string B()
    {
        (Dictionary<char, List<(int, int)>> antennaTypes, (int si, int sj)) = Parse(data);
        char[,] grid = new char[si, sj];

        foreach (var antennas in antennaTypes)
        {
            List<(int, int)> posistions = antennas.Value;

            for (int i = 0; i < posistions.Count - 1; i++)
            {
                (int x0, int y0) = (posistions[i].Item1, posistions[i].Item2);

                for (int j = i + 1; j < posistions.Count; j++)
                {
                    (int x1, int y1) = (posistions[j].Item1, posistions[j].Item2);
                    (int dx, int dy) = (x1 - x0, y1 - y0);

                    bool ok = true;
                    (int x, int y) = (x0, y0);

                    while (ok)
                    {
                        (grid, ok) = AddAntinodeToGrid(grid, x, y);
                        (x, y) = (x - dx, y - dy);
                    }

                    ok = true;
                    (x, y) = (x1, y1);

                    while (ok)
                    {
                        (grid, ok) = AddAntinodeToGrid(grid, x, y);
                        (x, y) = (x + dx, y + dy);
                    }
                }
            }
        }

        return CountAntinodes(grid).ToString();
    }

    private static (char[,], bool) AddAntinodeToGrid(char[,] grid, int x, int y)
    {
        bool ok = true;
        try { grid[x, y] = '#'; } catch { ok = false; }
        return (grid, ok);
    }

    private static int CountAntinodes(char[,] grid)
    {
        int antinodes = 0;

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == '#')
                {
                    antinodes++;
                }
            }
        }

        return antinodes;
    }

    private static (Dictionary<char, List<(int, int)>>, (int, int)) Parse(string data)
    {
        var antennas = new Dictionary<char, List<(int, int)>>();

        char[][] grid = data
            .Split('\n')
            .Where(x => x.Length > 0)
            .Select(x => x.ToArray())
            .ToArray();

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid.Length; j++)
            {
                char c = grid[i][j];

                if (c >= '0' && c <= '9' || c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z')
                {
                    if (!antennas.ContainsKey(grid[i][j]))
                    {
                        antennas[grid[i][j]] = new List<(int, int)>();
                    }
                    antennas[grid[i][j]].Add((i, j));
                }
            }
        }

        return (antennas, (grid.Length, grid[0].Length));
    }
}