using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KiriUtility;

public class CharOutputFactory : IDialogUnitFactory
{
    private Text dialogText;

    public CharOutputFactory(Text text)
    {
        this.dialogText = text;
    }

    public virtual void AddDialog(string dialog)
    {
        dialogText.text += dialog;
    }

    public IDialogUnit BuildDialogUnit(int ID, Dictionary<string, string> keyValuePairs)
    {
        if (!CanBuild(keyValuePairs))
        {
            throw new ArgumentException("這是什麼東西？\n" + keyValuePairs.ToDebugString());
        }
        return new CharOutput(ID, this, keyValuePairs["char"]);
    }

    public bool CanBuild(Dictionary<string, string> keyValuePairs)
    {
        return keyValuePairs.ContainsKey("char");
    }
}
