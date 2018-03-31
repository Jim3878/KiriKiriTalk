using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Krkr
{
    public class WaitClick : BaseDialogCmd
    {
        private IWaitHandler ctrl;

        public WaitClick(IWaitHandler ctrl)
        {
            this.ctrl = ctrl;
        }

        public override void StateBegin()
        {
            ctrl.Wait();
            ctrl.onResume += OnResume;
        }

        public override void StateEnd()
        {
            base.TouchStateBackToBegin();
        }

        private void OnResume(object o,EventArgs e)
        {
            krController.TranstToNextCmd();
        }

    }
}