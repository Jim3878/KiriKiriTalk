using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

public class BaseKiriState : IState
{
    private ITypewriter _typewriter;
    protected ITypewriter typewriter
    {
        get
        {
            return _typewriter;
        }
    }
    public BaseKiriState(ITypewriter typewriter)
    {
        this._typewriter = typewriter;
    }
    
}
