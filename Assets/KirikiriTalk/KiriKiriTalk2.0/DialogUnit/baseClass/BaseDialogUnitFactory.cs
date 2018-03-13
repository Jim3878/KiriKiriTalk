using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public abstract class BaseDialogUnitFactory : IDialogUnitFactory
{
    public List<KeyValuePair<string, string>> dialogUnitValue;

    public bool CanBuild(string dialog)
    {
        var lowerDialog= dialog.ToLower();
        if (IsHeadMached(GetHeader(lowerDialog)))
        {
            dialogUnitValue = Spilit(lowerDialog);
            return IsValueTypeMached(dialogUnitValue);
        }
        return false;
    }

    public IDialogUnit BuildDialogUnit(string dialog)
    {
        if (dialogUnitValue == null)
        {
            dialogUnitValue = Spilit(dialog);
        }
        return Build(dialogUnitValue);
    }

    string GetHeader(string dialog)
    {
        try
        {
            return dialog.Split(',')[0].Split('=')[0];
        }
        catch (Exception)
        {
            Debug.LogError(string.Format("沒有打名字(header)是不能通過的唷(皺眉)！\ndialog = {0}", dialog));
            throw;
        }
    }

    List<KeyValuePair<string, string>> Spilit(string dialog)
    {
        try
        {
            //先分割','字元
            List<string> fieldList = new List<string>();
            fieldList.AddRange(dialog.Split(','));
            if (fieldList.Count < 1)
            {
                throw new Exception(string.Format("括號裡的東西呢？還不快去補上！\ndialog = {0}", dialog));
            }

            List<KeyValuePair<string, string>> pair = FieldList2ValuePair(fieldList);

            return pair;
        }
        catch (Exception)
        {
            throw;
        }
    }

    List<KeyValuePair<string, string>> FieldList2ValuePair(List<string> fieldList)
    {
        List<KeyValuePair<string, string>> pair = new List<KeyValuePair<string, string>>();
        for (int i = 0; i < fieldList.Count; i++)
        {
            if (fieldList[i].Contains("="))
            {
                //參數有賦值
                string[] spilitedField = fieldList[i].Split('=');
                if (spilitedField.Length > 2)
                {
                    throw new Exception(string.Format("一個式子只會有一個等號啦！\ndialog = {0}", fieldList[i]));
                }

                string valueName = fieldList[i].Split('=')[0];
                string value = fieldList[i].Split('=')[1];
                pair.Add(new KeyValuePair<string, string>());
            }
            else
            {
                //參數無賦值
                if (i != 0)
                {
                    throw new Exception(string.Format("除了第一個參數(header)，後面的參數都要賦值啦！\ndialog = {0}", fieldList[i]));
                }
                else
                {
                    pair.Add(new KeyValuePair<string, string>(fieldList[i], null));
                }
            }
        }
        return pair;
    }

    protected abstract bool IsHeadMached(string header);
    protected abstract bool IsValueTypeMached(List<KeyValuePair<string, string>> value);
    protected abstract IDialogUnit Build(List<KeyValuePair<string, string>> Value);
}