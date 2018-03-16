using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRunningDialogUnitManager  {

    bool IsEmpty { get; }
    int count { get; }
    void AddDialogUnit(IDialogUnit dialogUnit);
    void RemoveDialogUnit(int ID);
    IDialogUnit GetDialogUnit(int ID);
    IDialogUnit GetTopDialogUnit();
    IDialogUnit[] GetAllDialogUnit();
	
}
