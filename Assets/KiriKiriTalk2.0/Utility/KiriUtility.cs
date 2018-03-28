using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Krkr
{
    public enum TypeEnum
    {
        NULL,
        INT,
        FLOAT,
        STRING,
        VECTOR_2,
        VECTOR_3,
        COLOR_HEX
    }

    public static class Utility
    {
        public static string ToHex(this Color color)
        {
            return string.Format("#{0}{1}{2}", ToHex(color.r * 256 - 1), ToHex(color.g * 256 - 1), ToHex(color.b * 256 - 1));
        }

        public static string ToHex(float n, int digits = 1)
        {
            return ToHex((int)n, digits);
        }

        public static string ToHex(int n, int digits = 1)
        {
            return n.ToString("x" + digits);
        }

        public static bool IsColorHex(this string text)
        {
            if (text == null)
            {
                return false;
            }
            return Regex.IsMatch(text, @"^#[0-9abcdef]{6}$");
        }

        public static bool IsInt(this string text)
        {
            if (text == null)
            {
                return false;
            }
            int o;
            return int.TryParse(text, out o);
            //return Regex.IsMatch(text, @"(^\+?|^-?)\d+$");
        }

        public static bool IsFloat(this string text)
        {
            float o;
            if (text == null)
            {
                return false;
            }

            string s = Regex.Replace(text, "f$", "");

            return float.TryParse(s, out o);
            //return Regex.IsMatch(text, @"(^\+?|^-?)\d+(\.\d+)?$");
        }

        public static bool IsVector2(this string text)
        {
            if (text == null)
            {
                return false;
            }
            return Regex.IsMatch(text, @"^\((\+?|-?)\d+(\.\d+)?,(\+?|-?)\d+(\.\d+)?\)$");
        }

        public static bool IsVector3(this string text)
        {
            if (text == null)
            {
                return false;
            }
            return Regex.IsMatch(text, @"^\((\+?|-?)\d+(\.\d+)?,(\+?|-?)\d+(\.\d+)?,(\+?|-?)\d+(\.\d+)?\)$");
        }

        public static string CallStack()
        {
            // 1:省略目前位置
            // true:顯示檔案資訊
            var stackTrace = new StackTrace(1, true);
            return stackTrace.ToString();
        }

        public static int ToInt(this string text)
        {
            try
            {
                return Int32.Parse(text);
            }
            catch (Exception)
            {
                UnityEngine.Debug.Log(text);
                throw;
            }
        }

        public static bool IsString(this string text)
        {
            return Regex.IsMatch(text, @"^"".*""$");
        }

        public static float ToFloat(this string text)
        {
            try
            {
                string s = Regex.Replace(text, "f$", "");
                return Single.Parse(s);
            }
            catch (Exception)
            {
                UnityEngine.Debug.Log(text);
                throw;
            }
        }

        public static Vector2 ToVector2(this string text)
        {
            try
            {
                if (IsVector2(text))
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

        public static Vector3 ToVector3(this string text)
        {
            try
            {
                if (IsVector3(text))
                {
                    string[] values = text.Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
                    return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
                }
                else
                {
                    throw new Exception(string.Format("這根本就不是Vector3啊！\ntext = {0}\n{1}", text, CallStack()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string ToStringNormalize(this string text)
        {
            if (!text.IsString())
            {
                throw new ArgumentException(string.Format("字串不是文字\n{0}", text));
            }
            return Regex.Replace(Regex.Replace(text, "^\"", ""), "\"$", "");
        }

        public static string[] SpiltToField(this string text)
        {
            //string field = @"((?=[^0-9]),(?=[^0-9]))|(,(?=[^0-9]))|((?=[^0-9]),)");

            return Regex.Split(text, @"(?![0-9]),(?![0-9])");
        }

        public static int FindRightBrackets(this char[] text, int startIndex)
        {
            char[] targetChar = new char[text.Length - startIndex];
            for (int i = 0; i < targetChar.Length; i++)
            {
                targetChar[i] = text[startIndex + i];
            }

            if (text[startIndex] != '[')
                throw new ArgumentException("沒有左括號\n" + new string(targetChar));

            for (int i = 0; i < targetChar.Length; i++)
            {
                if (targetChar[i] == ']')
                {
                    return i + startIndex;
                }
            }
            string s = new string(targetChar);
            throw new ArgumentException("找不到右括號\n" + s);
        }

        public static string RemoveBrackets(this string text)
        {
            Regex leftBrackets = new Regex(@"^\[");
            Regex rightBrackets = new Regex(@"\]$");

            if (!leftBrackets.IsMatch(text))
            {
                throw new ArgumentException("找不到左括號\n" + text);
            }
            if (!rightBrackets.IsMatch(text))
            {
                throw new ArgumentException("找不到右括號\n" + text);
            }

            return rightBrackets.Replace(leftBrackets.Replace(text.Replace(" ", ""), ""), "");

        }

        public static string RemoveBrackets(this string text, out int length)
        {
            length = text.Length;
            return text.RemoveBrackets();
        }

        public static string ToDebugString(this Dictionary<string, string> values)
        {
            string o = "[";
            int i = 0;
            foreach (var pair in values)
            {
                if (i != 0)
                    o += ",";
                i++;
                o += pair.Key;
                if (pair.Value != null)
                {
                    o += "=";
                    o += pair.Value;
                }
            }
            o += "]";
            return o;
        }

        public static IDialogUnit[] CatchNoDelayUnit(this ITypewriter typerwriter)
        {
            List<IDialogUnit> dontDelayList = new List<IDialogUnit>();
            var manager = typerwriter.unreadDialogUnitManager;
            while (!manager.isEmpty && !(manager.PeekDialogUnit() is IDelayable))
            {
                var unit = manager.PopDialogUnit();
                dontDelayList.Add(unit);
            }

            return dontDelayList.ToArray();

        }

        public static bool IsValueMatch(this Dictionary<string, string> keyValuePairs, string key, TypeEnum type)
        {
            if (keyValuePairs.ContainsKey(key))
            {
                switch (type)
                {
                    case TypeEnum.NULL:
                        return keyValuePairs[key] == null;
                    case TypeEnum.INT:
                        return keyValuePairs[key].IsInt();
                    case TypeEnum.FLOAT:
                        return keyValuePairs[key].IsFloat();
                    case TypeEnum.STRING:
                        return keyValuePairs[key].IsString();
                    case TypeEnum.VECTOR_2:
                        return keyValuePairs[key].IsVector2();
                    case TypeEnum.VECTOR_3:
                        return keyValuePairs[key].IsVector3();
                    case TypeEnum.COLOR_HEX:
                        return keyValuePairs[key].IsColorHex();
                    default:
                        return false;
                }
            }
            return false;
        }
    }
}