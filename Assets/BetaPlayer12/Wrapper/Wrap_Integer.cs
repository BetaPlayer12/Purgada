using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wrap_Integer {

    //// <summary>
    /// Rotates the Integer along min(inclusive) and max (inclusive)
    /// </summary>
    /// <param name="_int">number to rotate</param>
    /// <param name="min"</param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Rotate(this int _int, int min, int max)
    {
        var numberCount = max - min + 1; //the amount of numbers between min and max inclusive
        numberCount += min < 0 && max > 0 ? 1 : 0;

        if (_int < min)
        {
            return _int + numberCount;
        }
        else if (_int > max)
        {
            return _int - numberCount;
        }
        return _int;
    }

    //// <summary>
    /// Rotates the Integer along min(inclusive) and max (inclusive)
    /// </summary>
    /// <param name="_int">number to rotate</param>
    /// <param name="min"</param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static void Rotate(ref int _int, int min, int max)
    {
        var numberCount = max - min + 1; //the amount of numbers between min and max inclusive
        numberCount += min < 0 && max > 0 ? 1 : 0;

        if (_int < min)
        {
            _int = _int + numberCount;
        }
        else if (_int > max)
        {
            _int =_int - numberCount;
        }
        return;
    }

    /// <summary>
    /// Rotates the Integer along min(inclusive) and max (exclusive)
    /// </summary>
    /// <param name="_int"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int RotateIndex(this int _int, int min, int max) =>
         Rotate(_int,min,max-1);

    /// <summary>
    /// Rotates the Integer along min(inclusive) and max (exclusive)
    /// </summary>
    /// <param name="_int"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static void RotateIndex(ref int _int, int min, int max) =>
         Rotate(_int, min, max - 1);
}
