using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunningDialogUnitManager:IRunningDialogUnitManager  {
    private Dictionary<int, IDialogUnit> dialogUnitList=new Dictionary<int, IDialogUnit>();
    public bool IsEmpty
    {
        get
        {
            return dialogUnitList.Count == 0;
        }
    }
    public int count {
        get
        {
            return dialogUnitList.Count;
        }
    }

    public void Clear()
    {
        dialogUnitList.Clear();
    }

    public void AddDialogUnit(IDialogUnit dialogUnit)
    {
        try
        {
            if (dialogUnit == null) throw new ArgumentNullException();
            dialogUnitList.Add(dialogUnit.ID, dialogUnit);
        }
        catch (ArgumentNullException)
        {
            Debug.Log(string.Format("你丟空值進來作啥？"));
            throw;
        }
        catch (ArgumentException)
        {
            Debug.Log(string.Format("為什麼你會有相同ID的DialogUnit！？\nID = {0},\ndialogUnit = {1}",dialogUnit.ID,dialogUnit));
            throw;
        }
        catch (Exception)
        {
            Debug.Log(string.Format("你到底是何方神聖…！？"));
            throw;
        }
    }

    public void RemoveDialogUnit(int id)
    {
        dialogUnitList.Remove(id);
    }

    public IDialogUnit GetDialogUnit(int id)
    {
        return dialogUnitList[id];
    }

    public IDialogUnit GetTopDialogUnit()
    {
        try
        {
            int idMax = -1;
            foreach (var dialogUnitPair in dialogUnitList)
            {
                if (dialogUnitPair.Key > idMax)
                {
                    idMax = dialogUnitPair.Key;
                }
            }
            return dialogUnitList[idMax];
        }
        catch (ArgumentException)
        {
            Debug.Log(string.Format("已經沒有再執行的DialogUnit了唷"));
            throw;
        }
    }

    public IDialogUnit[] GetAllDialogUnit()
    {
        IDialogUnit[] result=new IDialogUnit[dialogUnitList.Count];
        int index = 0;
        foreach (var dialogUnitPair in dialogUnitList)
        {
            result[index] = dialogUnitPair.Value;
            index++;
        }
        return result;
    }
    
}
