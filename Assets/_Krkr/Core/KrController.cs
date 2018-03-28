using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class KrController : StateController, IKrController
    {
        Queue<BaseDialogCmd> mCmdQueue=new Queue<BaseDialogCmd>();
        public event EventHandler onEventCall;

        public KrController()
        {
            Start(new WaitState(this));
        }

        public int CmdCount
        {
            get
            {
                return mCmdQueue.Count;
            }
        }

        public bool isSkip
        {
            get
            {
                return false;
            }
        }

        public void CallEvent(EventArgs e)
        {
            if (onEventCall != null)
            {
                onEventCall(this, e);
            }
        }

        public void AddDialogCmd(params IDialogCmd[] cmds)
        {
            BaseDialogCmd cmd;
            for(int i = 0; i < cmds.Length; i++)
            {
                cmd = (cmds[i] as BaseDialogCmd);
                cmd.SetKrProperty(this);
                mCmdQueue.Enqueue(cmd);
            }
        }

        public void FlyToNextCmd()
        {
            TranstToNextCmd();
            StateUpdate();
        }

        public void TranstToNextCmd()
        {
            if (mCmdQueue.Count > 0)
            {
                TransTo(mCmdQueue.Dequeue());
            }else
            {
                TransTo(new WaitState(this));
            }
        }
    }
}