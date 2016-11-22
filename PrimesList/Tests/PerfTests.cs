using System;
using System.Diagnostics;
using System.IO;
using static System.Math;
using static System.Console;

class PerfTests
{
    static void Main()
    {
        TimePrimeChecking(1);
        Write("Press enter to exit...");
        Read();
    }

    // Returns the time it takes to initializes various sizes of PrimesList
    static void TimePrimeListInitialization(int Trials = 5)
    {
        Stopwatch Stopwatch = new Stopwatch();
        for (int MaxNum = 10 * 1000; MaxNum <= 10 * 1000 * 1000; MaxNum *= 10)
        {
            long TotalTime = 0;
            for (int i = 0; i < Trials; i++)
            {
                Stopwatch.Start();
                PrimesList Primes = new PrimesList(MaxNum);
                TotalTime += Stopwatch.ElapsedMilliseconds;
                Stopwatch.Reset();
            }
            WriteLine("Generating primes until 10^{0}, in one go, took {1} milliseconds", Log(MaxNum, 10), TotalTime / Trials);
        }
    }

    // Returns the time it takes to check, one at a time, if the numbers under a certain limit are prime
    static void TimePrimeChecking(int Trials = 5)
    {
        long TotalTime = 0;
        int Limit = 10 * 1000 * 1000;
        Stopwatch Stopwatch = new Stopwatch();
        for (int i = 0; i < Trials; i++)
        {
            Stopwatch.Start();
            PrimesList Primes = new PrimesList();
            for (int j = 0; j < Limit; j++)
            {
                Primes.Contains(j);
            }
            TotalTime += Stopwatch.ElapsedMilliseconds;
            Stopwatch.Reset();
        }
        WriteLine("Checking numbers under 10^{0} for primes, one at a time, took {1} milliseconds", Log(Limit, 10), TotalTime / Trials);
    }
}