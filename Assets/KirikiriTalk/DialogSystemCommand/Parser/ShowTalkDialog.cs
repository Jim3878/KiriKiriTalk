using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace KirikiriTalk {
    public class ShowTalkDialog :  KiriParser{

        protected override bool IsMatch(DialogUnit order)
        {
            return order.IsHeaderEqualTo("Show");
        }

        protected override void Parse(KirikiriController ctrl, DialogUnit order)
        {
            ctrl.talkBody.DOScale(1, 0.5f);
                
        }
    }
}