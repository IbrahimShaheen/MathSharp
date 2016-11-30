using System;
using System.Collections.Generic;

public class SequenceGenerator
{
    private Func<int, int> SequenceFunction;
    private HashSet<int> SequenceCache;
    private long MaxNumberCached;

    public SequenceGenerator(Func<int, int> SequenceFunction)
    {
        this.SequenceFunction = SequenceFunction;
        SequenceCache = new HashSet<int>();
        MaxNumberCached = 0;
    }

    // Returns true if Num is in this sequence
    public bool InSequence(int Num)
    {
        if (Num > MaxNumberCached)
        {
            UpdateCache(Num);
        }
        return SequenceCache.Contains(Num);
    }

    // Returns the number in the series with index Index
    public int GetNumAt(int Index)
    {
        int Num = SequenceFunction(Index);
        if (Index > SequenceCache.Count - 1)
        {
            UpdateCache(Num);
        }
        return Num;
    }

    // Updates cache until it includes Num
    // Updates MaxNumberCached to reflect changes
    private void UpdateCache(int Num)
    {
        while (Num > MaxNumberCached)
        {
            int NextNumInSeries = SequenceFunction(SequenceCache.Count);
            SequenceCache.Add(NextNumInSeries);
            MaxNumberCached = NextNumInSeries;
        }
    }
}