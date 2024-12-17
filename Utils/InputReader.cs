static class InputReader
{
    public static string GetInput(int day)
    {
        string filePath = Path.Combine("Inputs", $"Day{day}.txt");

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Input file for Day {day} not found.");

        return File.ReadAllText(filePath);
    }
}