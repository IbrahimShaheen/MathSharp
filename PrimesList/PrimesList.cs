using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using static System.Math;

// TODO: check inline todos
// TODO: combine functionality of get prime stream into getPrimesBetween and separate them into a separate class

public class PrimesList
{
    // TODO: Fields are only initialized in constructor!!!
    private BitArray Primes = new BitArray(1);
    private static int LargestPrimeToSieveIndex = (int)Sqrt(Int32.MaxValue) - 1; // Primes larger than this overflow
    private static int PrimeStreamIndex = 0;

    // TODO: is this parameter even needed?
    // Throws an exception if LargestNum < 2
    public PrimesList(int LargestNum = 2)
    {
        if (LargestNum < 2)
        {
            throw new Exception("List cannot include a prime less than 2");
        }
        Primes[0] = true; // First prime, 2, is at index 0
        IncreaseSize(LargestNum - 2);
    }

    // Returns true if Num is prime
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

    // Return an Enumerator containing primes between MinNum and MaxNum, exclusively
    public IEnumerable<int> GetPrimesBetween(int MinNum, int MaxNum)
    {
        EnsureSize(MaxNum);
        if (MinNum < 2) MinNum = 1;
        for (int Num = MinNum + 1; Num < MaxNum; Num++)
        {
            if (Primes[Num - 2])
            {
                yield return Num;
            }
        }
    }

    // Starts a new one PrimeStream at StartNum
    public void StartPrimeStream(int StartNum = 2)
    {
        PrimeStreamIndex = StartNum - 2;
    }

    // Returns the next prime according to PrimeStreamIndex
    // Increases the size of PrimesList using EnsureSize if the next Prime is not in the current list
    public int NextPrime()
    {
        do
        {
            PrimeStreamIndex++;
            EnsureSize(PrimeStreamIndex + 1);
        } while (!Primes[PrimeStreamIndex - 1]);
        return PrimeStreamIndex + 1;
    }

    // Resets PrimeStreamIndex to 0
    public void ResetPrimeStream()
    {
        PrimeStreamIndex = 0;
    }

    // Returns all known primes listed as a string
    public override string ToString()
    {
        StringBuilder PrimesToPrint = new StringBuilder("Primes: ");
        int LastPrime = -1; // Needed to fix fencepost problem with commas
        for (int i = 0; i < Primes.Count; i++)
        {
            if (Primes[i])
            {
                if (LastPrime != -1) PrimesToPrint.Append(LastPrime + ", ");
                LastPrime = i + 2;
            }
        }
        PrimesToPrint.Append(LastPrime);
        return PrimesToPrint.ToString();
    }

    // Increases the size of the Primes list by SizeIncrease
    private void IncreaseSize(int SizeIncrease)
    {
        int OldSize = Primes.Count;
        Primes.Length += SizeIncrease;
        for (int i = OldSize; i < Primes.Length; i++)
        {
            Primes[i] = true;
        }

        int StopSieveIndex = Min(Primes.Count, LargestPrimeToSieveIndex);
        for (int i = 0; i < StopSieveIndex; i++)
        {
            if (Primes[i])
            {
                int Prime = i + 2;

                // TODO: optimize when to calculate p^2 and when to calculate multiples
                // Find an optimal index to start sieving again
                int PrimeSquaredIndex = Prime * Prime - 2;
                int NextToSieveIndex = OldSize - 1 + Prime - (OldSize + 1) % Prime; // Next multiple of Prime after Prime[OldSize - 1]
                int SieveStartIndex = Max(NextToSieveIndex, PrimeSquaredIndex);

                // Start sieve
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