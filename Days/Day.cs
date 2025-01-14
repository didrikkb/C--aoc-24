interface IDay {
    string A();
    string B();
}

class Day {
    public static IDay Get(int n) {
        string data = InputReader.GetInput(n);

        return n switch {
            1 => new Day1(data),
            2 => new Day2(data),
            3 => new Day3(data),
            4 => new Day4(data),
            5 => new Day5(data),
            6 => new Day6(data),
            7 => new Day7(data),
            8 => new Day8(data),
            9 => new Day9(data),
            10 => new Day10(data),
            11 => new Day11(data),
            12 => new Day12(data),
            13 => new Day13(data),
            14 => new Day14(data),
            _ => throw new Exception(),
        };
    }
}