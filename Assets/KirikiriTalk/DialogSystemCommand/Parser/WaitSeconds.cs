using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KirikiriTalk
{
    public class WaitSeconds : KiriParser
    {
        protected override bool IsMatch(DialogUnit order)
        {
            return 
                order.HasToken("wait", KirikiriTalk.Parser.DataType.FLOAT) || order.HasToken("wait", KirikiriTalk.Parser.DataType.INT);
        }

        protected override void Parse(KirikiriController ctrl, DialogUnit order)
        {
            ctrl.time += order.GetTokenFloatValueByName("wait");
        }
    }
}