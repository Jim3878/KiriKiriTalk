using KirikiriTalk.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk
{
    public class ShowDialogImmediatelyCommand : IKirikiriCommand
    {
        KirikiriController ctrl;

        public void Excute(KirikiriController ctrl)
        {
            this.ctrl = ctrl;
            KirikiriTalk.InputInvoker.instance.mouseEvent += OnClick;
        }

        void OnClick(InputInvoker.clickTypeEnum clickType, KeyCode keyCode)
        {
            if ( clickType == InputInvoker.clickTypeEnum.UP)
            {
                ctrl.charDelayRatio = 0;
                //ctrl.onPrintBreakOrder += OnWaitClick;
            }
        }

        void OnWaitClick(object sender, BreakOrderEventArgs e)
        {
            if (sender.Equals(ctrl) && e.breakOrder.IsHeaderEqualTo("WaitClick"))
            {
                ctrl.charDelayRatio = 1;
                ctrl.onPrintBreakOrder -= OnWaitClick;
            }
        }
    }
}