using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class TextStyle
    {
        string style;
        bool hasValue;
        string value;
        public string header
        {
            get
            {
                return style;
            }
        }
        public string leftStyle
        {
            get
            {
                if (hasValue)
                    return string.Format("<{0}={1}>", style, value);
                else
                    return string.Format("<{0}>", style);
            }
        }
        public string rightStyle
        {
            get
            {
                return string.Format("</{0}>", style);
            }
        }

        public TextStyle(string style)
        {
            this.style = style;
            this.hasValue = false;
        }

        public TextStyle(string style, string value)
        {
            this.style = style;
            this.value = value;
            this.hasValue = true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            TextStyle textStyle = obj as TextStyle;
            if (textStyle == null)
            {
                return false;
            }
            return Equals(textStyle);
        }

        public bool Equals(TextStyle textStyle)
        {
            return textStyle.style == style;

        }

        public override int GetHashCode()
        {
            return style.GetHashCode();
        }
    }
    public class DialogStyleHandler : IDialogStyleHandler
    {
        Dictionary<string, TextStyle> styleList = new Dictionary<string, TextStyle>();
        bool hasConvertToArr = false;
        TextStyle[] _styleArr;
        TextStyle[] styleArr
        {
            get
            {
                if (!hasConvertToArr)
                {
                    hasConvertToArr = true;
                    _styleArr = new TextStyle[styleList.Count];
                    int index = 0;
                    foreach (var stylePair in styleList)
                    {
                        _styleArr[index] = stylePair.Value;
                        index++;
                    }
                }
                return _styleArr;
            }
        }
        public int count
        {
            get
            {
                return styleList.Count;
            }
        }

        public void AddStyle(TextStyle style)
        {
            hasConvertToArr = false;
            RemoveStyle(style.header);
            if (style == null)
            {
                throw new ArgumentNullException("style是空的啊");
            }
            styleList.Add(style.header, style);
        }

        public void RemoveStyle(TextStyle style)
        {
            RemoveStyle(style.header);
        }

        public void RemoveStyle(string header)
        {
            hasConvertToArr = false;
            styleList.Remove(header);
        }

        public string GetLeftStyle()
        {
            string leftStyle = "";
            for (int i = 0; i < styleArr.Length; i++)
            {
                leftStyle += GetLeftStyle(i);
            }
            return leftStyle;
        }

        public string GetRightStyle()
        {
            string rightStyle = "";
            for (int i = 0; i < styleArr.Length; i++)
            {
                rightStyle += GetRightStyle(i);
            }
            return rightStyle;
        }

        public bool HasStyle(string header)
        {
            return styleList.ContainsKey(header);
        }

        public TextStyle GetStyle(string header)
        {
            if (!styleList.ContainsKey(header))
                throw new KeyNotFoundException(string.Format("head不見囉~", header));
            return styleList[header];

        }

        public string GetLeftStyle(int index)
        {
            try
            {
                return styleArr[index].leftStyle;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log(string.Format("index爆炸了~\nindex={0}\ncount={1}", index, count));
                throw;
            }
        }

        public string GetRightStyle(int index)
        {
            try
            {
                return styleArr[count - 1 - index].rightStyle;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log(string.Format("index爆炸了~\nindex={0}\ncount={1}", index, count));
                throw;
            }
        }
    }
}