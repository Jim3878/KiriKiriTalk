using System;
using System.Collections;
using System.Collections.Generic;
using KiriUtility;
using UnityEngine;

public class DummyTypewriter : ITypewriter {
    private IRunningDialogUnitManager _runningDialogUnitManager;
    public IRunningDialogUnitManager runningDialogUnitManager
    {
        get
        {
            if (_runningDialogUnitManager == null)
            {
                _runningDialogUnitManager = new RunningDialogUnitManager();
            }
            return _runningDialogUnitManager;
        }
    }
    private IUnreadDialogUnitManager _unreadDialogUnitManager;
    public IUnreadDialogUnitManager unreadDialogUnitManager
    {
        get
        {
            if (_unreadDialogUnitManager == null)
            {
                _unreadDialogUnitManager = new UnreadDialogUnitManager();
            }
            return _unreadDialogUnitManager;
        }
    }
    private StateController _stateController;
    public StateController stateController
    {
        get
        {
            if (_stateController == null)
            {
                _stateController = new StateController();
            }
            return _stateController;
        }
    }
    private ITextStyleManager _textStyleManager;
    public ITextStyleManager textStyleManager
    {
        get
        {
            if (_textStyleManager == null)
            {
                _textStyleManager = new TextStyleManager();
            }
            return _textStyleManager;
        }
    }
    public float typeSpeed
    {
        get;set;
    }
    public float lastTypeTime { get; set; }
    public float typeDelay
    {
        get
        {
            return 1 / typeDelay;
        }
    }
    private List<IDialogUnit> untypeDiaogUnit = new List<IDialogUnit>();
    public bool isPause
    {
        get
        {
            return (stateController.CurrentState is PauseState);
        }
    }
    private bool _isTerminate = false;

    public event EventHandler onComplete;

    public bool isTerminate
    {
        get
        {
            return _isTerminate;
        }
    }

    public bool isStart { get; set; }

    public void AddDialogUnitList(IDialogUnit[] dialogUnit)
    {
        unreadDialogUnitManager.PushDialogs(dialogUnit);
    }

    public void StartType()
    {
    }

    public void Terminate()
    {
        _isTerminate = true;
    }
    
    public IState CurrentState()
    {
        return stateController.CurrentState;
    }

    public void Update()
    {
    }

    public void Start()
    {
        isStart = true;
    }

    public void Complete()
    {
        throw new NotImplementedException();
    }
}
