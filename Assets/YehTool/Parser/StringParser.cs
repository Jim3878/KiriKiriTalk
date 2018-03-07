using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeh.Parser
{
    public enum DataType
    {
        INT,
        FLOAT,
        STRING
    }

    public static class ColorParser
    {
        public static string ToHex(this Color color)
        {
            string hex = "#" + Mathf.CeilToInt(255 * color.r).ToString("X2") + Mathf.CeilToInt(255 * color.g).ToString("X2") + Mathf.CeilToInt(255 * color.b).ToString("X2") + Mathf.CeilToInt(255 * color.a).ToString("X2");
            return hex;
        }
    }
    public static class StringParser
    {

        public static object ToEnum(this string s, System.Type type)
        {
            return System.Enum.Parse(type, s);
        }
        
        public static bool FailParser(string data, out DataType dataType, out object value)
        {
            dataType = default(DataType);
            value = default(object);
            Debug.LogError(string.Format("錯誤數值 :{0}", data));
            return false;
        }

        public static bool TryParser(this string data, out DataType dataType, out object value)
        {
            if (data[0] == '\"')
            {
                if (data[data.Length - 1] == '\"')
                {
                    dataType = DataType.STRING;
                    value = data.Substring(1, data.Length - 2);
                    return true;
                }
                else
                {
                    return FailParser(data, out dataType, out value);
                }
            }
            else if (data[data.Length - 1] == 'f')
            {
                dataType = DataType.FLOAT;
                float temp;
                if (!float.TryParse(data.Substring(0, data.Length - 1), out temp))
                {
                    return FailParser(data, out dataType, out value);
                }
                else
                {
                    dataType = DataType.FLOAT;
                    value = temp;
                    return true;
                }
            }
            else
            {
                float tempFloat;
                int tempInt;
                if (!float.TryParse(data, out tempFloat))
                {
                    if (!int.TryParse(data, out tempInt))
                    {
                        return FailParser(data, out dataType, out value);
                    }
                    dataType = DataType.INT;
                    value = tempInt;
                    return true;
                }
                dataType = DataType.FLOAT;
                value = tempFloat;
                return true;
            }
        }


    }
}