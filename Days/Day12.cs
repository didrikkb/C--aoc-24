class Day12(string data) : IDay {
    char[][] garden = Parse(data);
    public string A() {
        int res = 0;

        for (int i = 0; i < garden.Length; i++) {
            for (int j = 0; j < garden[i].Length; j++) {
                if (garden[i][j] >= 'A' && garden[i][j] <= 'Z') {
                    char plant = garden[i][j];
                    (int perimeter, int area) = FindPerimeterAndAreaOfPlotA(i, j, plant);
                    res += perimeter * area;
                }
            }
        }

        return res.ToString();
    }

    public string B() {
        int res = 0;


        return res.ToString();
    }

    private (int, int) FindPerimeterAndAreaOfPlotA(int x, int y, char currPlant) {
        if (x < 0 || y < 0 || x >= garden.Length || y >= garden[x].Length) {
            return (1, 0);
        } else if (garden[x][y] == currPlant + 32) {
            return (0, 0);
        } else if (garden[x][y] != currPlant) {
            return (1, 0);
        }

        garden[x][y] = (char)(currPlant + 32);

        (int perimeter, int area) = (0, 1);

        for (int i = 1; i <= 4; i++) {
            (int dx, int dy) = Directions4(i);
            (int perimeter1, int area1) = FindPerimeterAndAreaOfPlotA(x + dx, y + dy, currPlant);

            perimeter += perimeter1;
            area += area1;
        }

        return (perimeter, area);
    }

    private void ResetGarden() {
        garden = Parse(data);
    }

    private static (int, int) Directions4(int n) => n switch {
        1 => (0, 1),
        2 => (1, 0),
        3 => (-1, 0),
        4 => (0, -1),
        _ => throw new Exception()
    };

    private static char[][] Parse(string data) {
        return data
            .Split('\n')
            .Where(l => l.Length > 0)
            .Select(l => l.ToCharArray())
            .ToArray();
    }
}