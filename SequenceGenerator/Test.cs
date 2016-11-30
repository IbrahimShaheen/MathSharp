using static System.Console;

class SeriesGeneratorTest
{
    // Use the C# unit test framework (it's really nice, has test cases and all)
    static void Main()
    {
        SequenceGenerator Squares = new SequenceGenerator(x => x * x);
        WriteLine(Squares.InSequence(64));
        Write("Done!");
        Read();
    }
}