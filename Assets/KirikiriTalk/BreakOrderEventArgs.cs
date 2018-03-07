using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk
{
    public class BreakOrderEventArgs : System.EventArgs
    {
        public BreakOrder breakOrder;
        public BreakOrderEventArgs(BreakOrder breakOrder)
        {
            this.breakOrder = breakOrder;
        }
    }
}
