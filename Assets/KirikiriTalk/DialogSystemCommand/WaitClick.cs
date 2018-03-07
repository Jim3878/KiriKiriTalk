using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KirikiriTalk
{
    public class WaitClick : KiriParser
    {
        KirikiriController ctrl;

        protected override bool IsMatch(BreakOrder order)
        {
            return order.IsTitle("waitClick"); 
                
        }

        protected override void Parse(KirikiriController ctrl, BreakOrder order)
        {
            ctrl.Pause();
            ctrl.WaitingSymbol.gameObject.SetActive(true);
            this.ctrl = ctrl;
            Yeh.InputEvent.onMouseUp += OnMouseUp;
        }
        void OnMouseUp(object sender,Yeh.MouseClickEvnetArgs e)
        {
            Yeh.InputEvent.onMouseUp -= OnMouseUp;
            ctrl.WaitingSymbol.gameObject.SetActive(false);
            ctrl.Continue();
        }
    }
}