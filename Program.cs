class Program
{
    static void Main(string[] args)
    {
        int dayN = int.Parse(args[0]);
        IDay day = Day.Get(dayN);

        Console.WriteLine(day.A());
        Console.WriteLine(day.B());
    }
}