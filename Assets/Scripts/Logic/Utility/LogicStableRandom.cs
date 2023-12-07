using System.Collections.Generic;
using FixMath.NET;

/// <summary>
/// 给定种子的随机工具类
/// 
/// 
/// </summary>
public class LogicStableRandom
{
    private const long multiplier = 0x5DEECE66DL;
    private const long addend = 0xBL;

    private static readonly long mask = (1L << 48) - 1;

    //private static readonly double DOUBLE_UNIT = 1.0 / (1L << 53);
    private long seed = 0;

    public LogicStableRandom(long seed)
    {
        this.seed = seed;
    }

    private int next(int bits)
    {
        seed = (seed * multiplier + addend) & mask;
        return (int)((long)((ulong)seed >> (48 - bits)));
    }

    ///// <summary>
    ///// 随机一个[0,1)的Double
    ///// </summary>
    ///// <returns></returns>
    //public virtual double randomDouble()
    //{
    //    return (((long)(next(26)) << 27) + next(27)) * DOUBLE_UNIT;
    //}

    /// <summary>
    /// 随机一个[0,1)的Fix64
    /// </summary>
    /// <returns></returns>
    public Fix64 randomFix64()
    {
        return randomInt(10000) / (Fix64)10000;
    }

    /// <summary>
    /// 随机一个[0,bound)的整数
    /// </summary>
    /// <param name="bound">整数界限，必须大于零</param>
    /// <returns></returns>
    public virtual int randomInt(int bound)
    {
        if (bound <= 0)
        {
            throw new System.ArgumentException("bound must be positive");
        }

        int r = next(31);
        int m = bound - 1;
        if ((bound & m) == 0) // i.e., bound is a power of 2
        {
            r = (int)((bound * (long)r) >> 31);
        }
        else
        {
            for (int u = r; u - (r = u % bound) + m < 0; u = next(31))
            {
            }
        }

        return r;
    }

    /// <summary>
    /// 随机打乱列表，不会改变原列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns>打乱后的新列表</returns>
    public List<T> randomList<T>(List<T> list)
    {
        List<T> tmpList = new List<T>(list);
        List<T> retList = new List<T>();
        while (tmpList.Count > 0)
        {
            //Select an index and item
            int rdIndex = tmpList.Count == 1 ? 0 : randomInt(tmpList.Count);
            T remove = tmpList[rdIndex];

            //remove it from copyList and add it to output
            tmpList.Remove(remove);
            retList.Add(remove);
        }

        return retList;
    }

    public void Destroy()
    {
        seed = default(long);
    }
}