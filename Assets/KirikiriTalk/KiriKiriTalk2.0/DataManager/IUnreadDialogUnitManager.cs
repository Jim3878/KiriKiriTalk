using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnreadDialogUnitManager {
    int Count { get; }
    bool isEmpty { get; }
    void PushDialogs(params IDialogUnit[] unreadDialogUnit);
    IDialogUnit[] GetAllDialogUnit();
    IDialogUnit PeekDialogUnit();
    IDialogUnit PopDialogUnit();
}
