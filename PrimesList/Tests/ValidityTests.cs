using System;
using System.IO;
using static System.Console;

// TODO: all tests should report false positives and false negatives
class ValidityTests
{
    static void Main()
    {
        PrimesList Primes = new PrimesList();
        Write("Press enter to exit...");
        Read();
    }

    // Tests that the list contains all the primes between Min and Max inclusively
    // Uses FileDir as reference
    static void TestContainsPrimes(int Min, int Max, string FileDir)
    {
        int WrongPrimes = 0;
        PrimesList Primes = new PrimesList(Max);
        WriteLine("Generated primes.");

        foreach (string Line in File.ReadAllLines(FileDir))
        {
            int Prime = Int32.Parse(Line);
            if (!Primes.Contains(Prime))
            {
                WrongPrimes++;
                WriteLine("{0} is not contained in the list!", Prime);
            }
        }
        WriteLine("Found {0} wrong primes.", WrongPrimes);
    }
}