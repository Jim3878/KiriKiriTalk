using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk {
    public class ClearDialog : KiriParser
    {
        protected override bool IsMatch(DialogUnit order)
        {
            return order.IsHeaderEqualTo("Clear");
        }

        protected override void Parse(KirikiriController ctrl, DialogUnit order)
        {
            ctrl.dialogText.text = "";
        }
    }
}