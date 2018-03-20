using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnreadDialogUnitManager : IUnreadDialogUnitManager
{
    private List<IDialogUnit> unreadDialogUnitList = new List<IDialogUnit>();
    public int Count
    {
        get
        {
            return unreadDialogUnitList.Count;
        }
    }
    public bool isEmpty
    {
        get
        {
            return unreadDialogUnitList.Count == 0;
        }
    }

    public void Clear()
    {
        unreadDialogUnitList.Clear();
    }

    public void PushDialogs(params IDialogUnit[] unreadDialogUnit)
    {
        unreadDialogUnitList.AddRange(unreadDialogUnit);
    }

    public IDialogUnit[] GetAllDialogUnit()
    {
        return unreadDialogUnitList.ToArray();
    }

    public IDialogUnit PeekDialogUnit()
    {
        return unreadDialogUnitList[0];
    }

    public IDialogUnit PopDialogUnit()
    {
        if (!isEmpty)
        {
            var dialogUnit = unreadDialogUnitList[0];
            unreadDialogUnitList.RemoveAt(0);
            return dialogUnit;
        }
        throw new IndexOutOfRangeException("已經沒有東西了");
    }
}
