using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace KirikiriTalk {
    public class ShowTalkDialog :  KiriParser{

        protected override bool IsMatch(BreakOrder order)
        {
            return order.IsTitle("Show");
        }

        protected override void Parse(KirikiriController ctrl, BreakOrder order)
        {
            ctrl.talkBody.DOScale(1, 0.5f);
                
        }
    }
}