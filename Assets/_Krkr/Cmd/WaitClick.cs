using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Krkr
{
    public class WaitClick : IDialogCmd
    {
        private IWaitHandler ctrl;
        

        public WaitClick(IWaitHandler ctrl)
        {
            this.ctrl = ctrl;
            
        }

        public override void StateBegin()
        {
            ctrl.Wait();
            queueController.isSkipLine = false;
            ctrl.onResume += OnResume;
        }

        public override void StateEnd()
        {
            base.BackToBeginState();
        }

        private void OnResume(object o,EventArgs e)
        {
            queueController.isSkipLine = false;
            queueController.TranstToNextCmd();
        }

    }
}