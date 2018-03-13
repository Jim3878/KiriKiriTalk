using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk.Event
{
    public class CommandEventArgs : EventArgs
    {
        public IKirikiriCommand command;
        public CommandEventArgs(IKirikiriCommand command)
        {
            this.command = command;
        }
    }

    public class BreakOrderEventArgs : System.EventArgs
    {
        public DialogUnit breakOrder;
        public BreakOrderEventArgs(DialogUnit breakOrder)
        {
            this.breakOrder = breakOrder;
        }
    }
}
