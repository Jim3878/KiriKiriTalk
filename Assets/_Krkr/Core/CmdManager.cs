using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckManager;

namespace Krkr
{
    public class CmdManager : TaskController, ICmdManager
    {
        public bool isSkipLine { get; set; }

        public CmdManager(IdleTask idleTask) : base(idleTask)
        {
        }

        public void FlyToNextCmd()
        {
            TranstToNextCmd();
            StateUpdate();
        }

        public void TranstToNextCmd()
        {
            TransToNextTask();
        }

        public void AddDialogCmd(ICmd[] cmd)
        {
            foreach (var c in cmd)
            {
                AddTask(c as IDialogCmd);
            }
        }
    }
}