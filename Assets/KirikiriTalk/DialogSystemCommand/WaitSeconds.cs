using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KirikiriTalk
{
    public class WaitSeconds : KiriParser
    {
        protected override bool IsMatch(BreakOrder order)
        {
            return 
                order.HasToken("wait", Yeh.Parser.DataType.FLOAT) || order.HasToken("wait", Yeh.Parser.DataType.INT);
        }

        protected override void Parse(KirikiriController ctrl, BreakOrder order)
        {
            ctrl.time += order.GetTokenFloatValueByName("wait");
        }
    }
}