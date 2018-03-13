using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KirikiriTalk
{
    public class WaitClick : KiriParser
    {
        KirikiriController ctrl;

        protected override bool IsMatch(DialogUnit order)
        {
            return order.IsHeaderEqualTo("waitClick");

        }

        protected override void Parse(KirikiriController ctrl, DialogUnit order)
        {
            if (!ctrl.isPause)
            {
                ctrl.Pause();
                ctrl.WaitingSymbol.gameObject.SetActive(true);
                this.ctrl = ctrl;
                InputInvoker.instance.mouseEvent += OnMouseUp;
            }
        }
        void OnMouseUp(InputInvoker.clickTypeEnum clickType, KeyCode keyCode)
        {
            if (clickType == InputInvoker.clickTypeEnum.UP)
            {
                InputInvoker.instance.mouseEvent -= OnMouseUp;
                ctrl.WaitingSymbol.gameObject.SetActive(false);
                ctrl.Continue();
            }
        }
    }
}