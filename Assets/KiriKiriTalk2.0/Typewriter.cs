using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KiriUtility;


public class Typewriter : ITypewriter
{
    public event EventHandler onComplete;
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
    private IDialogStyleController _textStyleManager;
    public IDialogStyleController textStyleManager
    {
        get
        {
            if (_textStyleManager == null)
            {
                _textStyleManager = new DialogStyleController();
            }
            return _textStyleManager;
        }
    }
    public float typeSpeed { get; set; }
    private float _lastTypeTime;
    public float lastTypeTime
    {
        get
        {
            return _lastTypeTime;
        }
        set
        {
            _lastTypeTime = value;
        }
    }
    public float typeDelay
    {
        get
        {
            return 1 / typeSpeed;
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
    public bool isTerminate
    {
        get
        {
            return _isTerminate;
        }
    }
    private bool _isStart = false;
    public bool isStart
    {
        get
        {
            return _isStart;
        }
    }
    public void AddDialogUnitList(IDialogUnit[] dialogUnit)
    {
        this.unreadDialogUnitManager.PushDialogs(dialogUnit);
    }

    public void Start()
    {
        _isStart = true;
        typeSpeed = 5;
        stateController.Start(new NormalState(this));
        //stateController.TransTo(new NormalState(this));
    }

    public void Update()
    {
        stateController.StateUpdate();
        if (!isPause && unreadDialogUnitManager.isEmpty)
        {
            Complete();
        }
    }

    public void Terminate()
    {
        _isTerminate = true;
    }

    public void Complete()
    {
        if (onComplete != null)
            this.onComplete(this, new EventArgs());
    }
}
