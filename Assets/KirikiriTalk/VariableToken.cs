using KirikiriTalk.Parser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableToken
{
    public bool hasValue;
    public string name;
    public string operater;
    public object value;
    public VariableType dataType;

    public VariableToken(string name)
    {
        this.hasValue = false;
        this.name = name.ToLower();
    }
    public VariableToken(string name, string operater, string value)
    {
        this.hasValue = true;
        this.name = name.ToLower();
        this.operater = operater;
        value.TryParser(out dataType, out this.value);
    }
}
