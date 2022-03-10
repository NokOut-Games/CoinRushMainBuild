using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtentionMethods 
{
    public static string ConvertToText(this int score,string formate = "F1")
    {
        float scoreF = (float)score;
        if (scoreF >= 1000000000) return (scoreF / 1000000000).ToString(formate) + "B";
        else if (scoreF >= 1000000) return (scoreF / 1000000).ToString(formate) + "M";
        else if (scoreF >= 1000) return (scoreF / 1000).ToString(formate) + "K";
        else return scoreF.ToString();
    }
    public static string ConvertToText(this float score, string formate = "F1")
    {
        float scoreF = (float)score;
        if (scoreF >= 1000000000) return (scoreF / 1000000000).ToString(formate) + "B";
        else if (scoreF >= 1000000) return (scoreF / 1000000).ToString(formate) + "M";
        else if (scoreF >= 1000) return (scoreF / 1000).ToString(formate) + "K";
        else return scoreF.ToString();
    }
    public static string ConvertToText(this string score, string formate = "F1")
    {
        float scoreF = float.Parse(score);
        if (scoreF >= 1000000000) return (scoreF / 1000000000).ToString(formate) + "B";
        else if (scoreF >= 1000000) return (scoreF / 1000000).ToString(formate) + "M";
        else if (scoreF >= 1000) return (scoreF / 1000).ToString(formate) + "K";
        else return scoreF.ToString();
    }
}
