using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;


public static class KiriTalkUtility
{
    public static string ToHex(this Color color)
    {
        return string.Format("#{0}{1}{2}", FloatToHex(color.r * 256 - 1), FloatToHex(color.g * 256 - 1), FloatToHex(color.b * 256 - 1));
    }

    public static string FloatToHex(float n, int digits = 1)
    {
        return IntToHex((int)n, digits);
    }

    public static string IntToHex(int n, int digits = 1)
    {
        return n.ToString("x" + digits);
    }

    public static bool StringIsInt(string text)
    {
        return Regex.IsMatch(text, @"(^\+?|^-?)\d+$");
    }

    public static bool StringIsFloat(string text)
    {
        return Regex.IsMatch(text, @"(^\+?|^-?)\d+(\.\d+)?$");
    }

    public static bool StringIsVector2(string text)
    {
        return Regex.IsMatch(text, @"^\((\+?|-?)\d+(\.\d+)?,(\+?|-?)\d+(\.\d+)?\)$");
    }

    public static bool StringIsVector3(string text)
    {
        return Regex.IsMatch(text, @"^\((\+?|-?)\d+(\.\d+)?,(\+?|-?)\d+(\.\d+)?,(\+?|-?)\d+(\.\d+)?\)$");
    }

    public static string CallStack()
    {
        // 1:省略目前位置
        // true:顯示檔案資訊
        var stackTrace = new StackTrace(1, true);
        return stackTrace.ToString();
    }

    public static Vector2 ToVector2(string text)
    {
        try
        {
            if (StringIsVector2(text))
            {
                string[] values = text.Replace("(", "").Replace(")", "").Split(',');
                return new Vector2(float.Parse(values[0]), float.Parse(values[1]));
            }
            else
            {
                throw new Exception(string.Format("這根本就不是Vector2啊！\ntext = {0}", text));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static Vector3 ToVector3(string text)
    {
        try
        {
            if (StringIsVector3(text))
            {
                string[] values = text.Replace("(", "").Replace(")", "").Split(',');
                return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            }
            else
            {
                throw new Exception(string.Format("這根本就不是Vector3啊！\ntext = {0}", text));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
