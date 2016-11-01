using System;
using System.Diagnostics;
using System.IO;
using static System.Console;

class Solution
{
    static void Main()
    {
        Write("Press enter to exit...");
        Read();
    }

    // Tests the speed of generating primes and prints the average results of the number of Trials
    static void TestGeneration(int Trials = 1)
    {
        Stopwatch Stopwatch = new Stopwatch();
        for (int ListSize = 1000; ListSize <= 10 * 1000 * 1000; ListSize *= 10)
        {
            long TotalTime = 0;
            for (int i = 0; i < Trials; i++)
            {
                Stopwatch.Start();
                PrimesList Primes = new PrimesList(ListSize);
                TotalTime += Stopwatch.ElapsedMilliseconds;
                Stopwatch.Reset();
            }
            WriteLine("Generating {0} primes took {1} milliseconds", ListSize, TotalTime / Trials);
        }
    }

    // Tests the speed of appending one number at a time and prints the average results of the number of Trials
    static void TestAppending(int Trials = 1)
    {
        Stopwatch Stopwatch = new Stopwatch();
        long TotalTime = 0;
        for (int i = 0; i < Trials; i++)
        {
            Stopwatch.Start();
            PrimesList Primes = new PrimesList();
            for (int j = 0; j < 10 * 1000; j++)
            {
                // Primes.Increase(1); // Fix this!
            }
            TotalTime += Stopwatch.ElapsedMilliseconds;
            Stopwatch.Reset();
        }
        WriteLine("Appending one number at a time took {0} milliseconds to reach ten thousand", TotalTime / Trials);
    }

    // Tests that the list contains all the primes between Min and Max inclusively
    // Uses FileDir as reference
    // Note that this does not ensure that the list isn't generating false primes
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