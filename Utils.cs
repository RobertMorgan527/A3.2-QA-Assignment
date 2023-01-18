using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

public class Utils
{
    public static bool isAlphabetic(String inputString)
    {
        Regex r = new Regex("^[a-zA-Z ]+$");
        return r.IsMatch(inputString);
    }

    public static bool hasIllegalChars(String inputString)
    {
        var badChars = new List<string>()
            { " ","\t","\n","\r","[","]","<",">","~",";","'","@" };
        foreach (var c in badChars)
        {
            if (inputString.Contains(c))
            {
                return true;
            }
        }
        return false;
    }

    public static bool hasOneDigit(String inputString)
    {
        foreach (char c in inputString)
        {
            if (char.IsDigit(c))
                return true;
        }
        return false;
    }

    public static bool hasExtraPasswordChars(String inputString)
    {
        foreach (var c in "@$£") //Should include all symbols
        {
            if (inputString.Contains(c))
            {
                return true;
            }
        }
        return false;
    }
}
