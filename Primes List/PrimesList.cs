using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using static System.Math;

public class PrimesList
{
    private BitArray Primes = new BitArray(1);
    private static int LargestPrimeToSieveIndex = (int)Sqrt(Int32.MaxValue) - 2; // Any prime larger than this is useless in sieving

    public PrimesList(int LargestNum = 2)
    {
        Primes[0] = true; // First prime is 2
        IncreaseSize(LargestNum - 2);
    }

    // Returns true if Num is prime
    // Doubles the size of the list if Num is not included
    // Lookup is O(1)
    public bool Contains(int Num)
    {
        EnsureSize(Num);
        int Index = Num - 2;
        if (Index > -1)
        {
            return Primes[Index];
        }
        return false;
    }

    // Return an Enumerator that enumerates over the primes between MinNum and MaxNum, inclusively
    public IEnumerable<int> GetPrimes(int MaxNum, int MinNum = 2)
    {
        EnsureSize(MaxNum); // Should be IncreaseSize
        if (MinNum < 2) MinNum = 2;
        for (int Num = MinNum; Num <= MaxNum; Num++)
        {
            if (Primes[Num - 2])
            {
                yield return Num;
            }
        }
    }

    // Returns all known primes listed as a string
    public override string ToString()
    {
        StringBuilder PrimesPrinted = new StringBuilder();
        PrimesPrinted.Append("Primes: ");
        int LastPrimeInList = -1; // Needed to fix fencepost problem with commas
        for (int i = 0; i < Primes.Count; i++)
        {
            if (Primes[i])
            {
                if (LastPrimeInList != -1) PrimesPrinted.Append(LastPrimeInList + ", ");
                LastPrimeInList = i + 2;
            }
        }
        PrimesPrinted.Append(LastPrimeInList);

        return PrimesPrinted.ToString();
    }

    // Increases the size of the Primes list by SizeIncrease
    private void IncreaseSize(int SizeIncrease)
    {
        int OldSize = Primes.Count;
        Primes.Length += SizeIncrease;
        Primes.SetAll(true);

        int LastIndex = Min(Primes.Count - 1, LargestPrimeToSieveIndex);
        for (int i = 0; i <= LastIndex; i++)
        {
            if (Primes[i])
            {
                int Prime = i + 2;
                int PrimeSquaredIndex = Prime * Prime - 2;
                int NextToSieveIndex = OldSize - 1 + Prime - (OldSize + 1) % Prime; // Index of next number to sieve
                int SieveStartIndex = Max(NextToSieveIndex, PrimeSquaredIndex);
                for (int j = SieveStartIndex; j < Primes.Count; j += Prime)
                {
                    Primes[j] = false;
                }
            }
        }
    }

    // Increases the size of Primes list, by a factor of 1.5, until it contains the number Num
    // This exponential increase allows this class to perform under a loop asking for primes
    private void EnsureSize(int Num)
    {
        while (Num - 1 > Primes.Count)
        {
            IncreaseSize(Primes.Count / 2 + 1);
        }
    }
}