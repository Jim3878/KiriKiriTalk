using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiriStateController
{


    BaseKiriState currentState = null;
    private bool isTerminated = false;
    private bool isStarted = false;
    KiriTalk talk;
    
    
    public void TransTo(BaseKiriState state)
    {
        try
        {
            if (isTerminated)
            {
                throw new Exception(string.Format("今天已經打烊了(isEnd=true)，不開放切換狀態唷\n state = {0}", state));
            }
            currentState.StateExit();
            state.StateBegin();
            state.SetProperty(this);
            currentState = state;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Start(BaseKiriState state)
    {
        try
        {
            if (isTerminated)
                throw new Exception(string.Format("已經結束(isTerminated=true)的東西是無法重新開始的\n state = {0}", state));
            if (isStarted)
                throw new Exception(string.Format("開始的機會是僅只一次的！\n state = {0}", state));
            state.StateBegin();
            currentState = state;
            isStarted = true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void StateUpdate()
    {
        try
        {
            if (isTerminated || !isStarted)
            {
                throw new Exception(string.Format("我不管了…(攤)\nisTerminated={0}\nisStarted={1}", isTerminated, isStarted));
            }
            if (currentState == null)
            {
                throw new Exception(string.Format("沒有狀態的狀態機是要人家作什麼啦！"));
            }
            currentState.StateUpdate();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Terminate()
    {
        if (currentState != null)
        {
            currentState.StateExit();
            currentState = null;
        }
        isTerminated = true;
    }

}
