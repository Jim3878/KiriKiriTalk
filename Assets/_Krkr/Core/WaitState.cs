using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class WaitState : IState
    {
        KrController controller;
        public WaitState(KrController controller)
        {
            this.controller = controller;
        }

        public override void StateUpdate()
        {
            if (controller.CmdCount != 0)
            {
                controller.FlyToNextCmd();
            }
        }
    }
}