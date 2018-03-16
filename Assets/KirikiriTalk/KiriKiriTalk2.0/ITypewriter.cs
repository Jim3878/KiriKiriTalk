using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;
using System;

public interface ITypewriter  {

    event EventHandler onComplete;
    float lastTypeTime { get; set; }
    float typeSpeed { get; set; }
    float typeDelay { get; }
    StateController stateController { get; }
    IRunningDialogUnitManager runningDialogUnitManager { get; }
    IUnreadDialogUnitManager unreadDialogUnitManager { get; }
    ITextStyleManager textStyleManager { get; }
    bool isPause { get; }
    bool isStart { get; }
    bool isTerminate { get; }
    void Start();
    void AddDialogUnitList(IDialogUnit[] dialogUnit);
    void Update();
    void Complete();
    void Terminate();
}
