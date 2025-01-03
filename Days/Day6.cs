enum Direction {
    Up,
    Down,
    Left,
    Right
}

class Day6(string inputData) : IDay {
    private char[][] map;
    private Direction dir = Direction.Up;
    private (int, int) pos = (0, 0);
    private (int, int) obst = (-1, -1);

    public string A() {
        CreateMap();

        while (true) {
            int state = Peek();

            if (state == 1) {
                Turn();
                continue;
            } else if (state == -1) {
                break;
            }

            Move();
        }

        return CountVisited().ToString();
    }

    public string B() {

        int res = 0;

        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[i].Length; j++) {
                CreateMap();

                int visits = 0;

                obst = (i, j);

                while (true) {
                    int state = Peek();

                    if (state == 1) {
                        Turn();
                        continue;
                    } else if (state == -1) {
                        break;
                    }

                    Move();

                    if (visits++ > map.Length * map[0].Length) {
                        res += 1;
                        break;
                    }
                }
            }
        }

        return res.ToString();
    }

    private void FindStartPos() {
        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map.Length; j++) {
                if (map[i][j] == '^') {
                    pos = (i, j);
                    map[pos.Item1][pos.Item2] = 'X';

                    return;
                }
            }
        }
    }

    private void Move() {
        pos = dir switch {
            Direction.Up => (pos.Item1 - 1, pos.Item2),
            Direction.Down => (pos.Item1 + 1, pos.Item2),
            Direction.Left => (pos.Item1, pos.Item2 - 1),
            Direction.Right => (pos.Item1, pos.Item2 + 1),
            _ => throw new Exception()
        };

        map[pos.Item1][pos.Item2] = 'X';
    }

    private void Turn() {
        dir = dir switch {
            Direction.Up => Direction.Right,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Right => Direction.Down,
            _ => throw new Exception()
        };
    }

    private int Peek() {
        (int x, int y) = dir switch {
            Direction.Up => (pos.Item1 - 1, pos.Item2),
            Direction.Down => (pos.Item1 + 1, pos.Item2),
            Direction.Left => (pos.Item1, pos.Item2 - 1),
            Direction.Right => (pos.Item1, pos.Item2 + 1),
            _ => throw new Exception()
        };
        if ((x, y) == obst) {
            return 1;
        }
        if (x < 0 || x >= map.Length || y < 0 || y >= map[x].Length) {
            return -1;
        }
        if (map[x][y] == '#') {
            return 1;
        }
        return 0;
    }

    private int CountVisited() {
        int visited = 0;

        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map.Length; j++) {
                if (map[i][j] is 'X') {
                    visited += 1;
                }
            }
        }

        return visited;
    }

    private void Print() {
        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map.Length; j++) {
                Console.Write(map[i][j]);
            }
            Console.Write("\n");
        }
        Console.Write("\n");
    }

    private void CreateMap() {
        map = inputData.Split('\n').Select(l => l.ToArray()).Where(x => x.Length > 0).ToArray();
        dir = Direction.Up;
        FindStartPos();
    }
}