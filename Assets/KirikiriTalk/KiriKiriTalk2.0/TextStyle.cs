using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
