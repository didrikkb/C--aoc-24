class Day4(string inputData) : IDay {
    private readonly char[][] data = inputData.Split("\n").Select(x => x.ToCharArray()).ToArray();

    public string A() {
        int res = 0;

        for (int i = 0; i < data.Length; i++) {
            for (int j = 0; j < data[i].Length; j++) {
                for (int k = 1; k <= 8; k++) {
                    (int xd, int yd) = Directions(k);
                    (int x, int y) = (i, j);
                    bool xmas = true;
                    char[] word = { 'X', 'M', 'A', 'S' };

                    for (int l = 0; l < 4; l++) {
                        if (!(x >= 0 && x < data.Length && y >= 0 && y < data[i].Length)) {
                            xmas = false;
                            break;
                        }
                        if (data[x][y] != word[l]) {
                            xmas = false;
                            break;
                        }
                        (x, y) = (x + xd, y + yd);
                    }

                    if (xmas) {
                        res += 1;
                    }
                }
            }
        }

        return res.ToString();
    }

    public string B() {
        int res = 0;

        for (int i = 1; i < data.Length - 1; i++) {
            for (int j = 1; j < data[i].Length - 1; j++) {
                if (data[i][j] != 'A') {
                    continue;
                }
                int xmas = 0;

                if (data[i + 1][j + 1] == 'M' && data[i - 1][j - 1] == 'S') {
                    xmas += 1;
                }
                if (data[i + 1][j + 1] == 'S' && data[i - 1][j - 1] == 'M') {
                    xmas += 1;
                }
                if (data[i - 1][j + 1] == 'M' && data[i + 1][j - 1] == 'S') {
                    xmas += 1;
                }
                if (data[i - 1][j + 1] == 'S' && data[i + 1][j - 1] == 'M') {
                    xmas += 1;
                }

                if (xmas == 2) {
                    res += 1;
                }
            }
        }

        return res.ToString();
    }

    private static (int, int) Directions(int n) => n switch {
        1 => (0, 1),
        2 => (1, 0),
        3 => (1, 1),
        4 => (0, -1),
        5 => (-1, 0),
        6 => (-1, -1),
        7 => (1, -1),
        8 => (-1, 1),
        _ => throw new Exception()
    };
}