using System.Collections.Generic;

public abstract class SeriesTester
{
    private HashSet<long> NumbersCache = new HashSet<long>();
    private long MaxNumberCached = 0;

    // Returns true if Num is in this series
    public bool InSeries(long Num)
    {
        if(Num > MaxNumberCached)
        {
            UpdateCache(Num);
        }
        return NumbersCache.Contains(Num);
    }

    // Returns the number in the series with index Index
    public long GetNumAt(long Index)
    {
        long Num = GetFuncVal(Index);
        if (Index > NumbersCache.Count - 1)
        {
            UpdateCache(Num);
        }
        return Num;
    }

    // Updates cache until it includes Num
    // Updates MaxNumberCached to reflect changes
    private void UpdateCache(long Num)
    {
        while (Num > MaxNumberCached)
        {
            long NextNumInSeries = GetFuncVal(NumbersCache.Count);
            NumbersCache.Add(NextNumInSeries);
            MaxNumberCached = NextNumInSeries;
        }
    }

    // Returns the value of the Series at Index
    protected abstract long GetFuncVal(long Index);
}