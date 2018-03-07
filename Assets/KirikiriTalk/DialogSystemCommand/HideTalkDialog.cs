using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace KirikiriTalk
{
    public class HideTalkDialog : KiriParser
    {
        protected override bool IsMatch(BreakOrder order)
        {
            return order.IsTitle("Hide");
        }

        protected override void Parse(KirikiriController ctrl, BreakOrder order)
        {
            ctrl.talkBody.DOScale(0, 0.5f);
            
        }
    }
}