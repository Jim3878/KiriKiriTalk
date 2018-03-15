using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseKiriState
{
    KiriTalk _talk;
    KiriStateController _controller;
    protected KiriStateController controller { get { return _controller; } }
    protected KiriTalk talk { get { return _talk; } }
    
    public BaseKiriState(KiriTalk talk)
    {
        this._talk = talk;
    }

    public void SetProperty(KiriStateController controller)
    {
        this._controller = controller;
        this._talk = talk;
    }

    public abstract void StateBegin();
    public abstract void StateUpdate();
    public abstract void StateExit();
}
