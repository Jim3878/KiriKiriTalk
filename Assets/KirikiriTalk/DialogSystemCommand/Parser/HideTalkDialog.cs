using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace KirikiriTalk
{
    public class HideTalkDialog : KiriParser
    {
        protected override bool IsMatch(DialogUnit order)
        {
            return order.IsHeaderEqualTo("Hide");
        }

        protected override void Parse(KirikiriController ctrl, DialogUnit order)
        {
            ctrl.talkBody.DOScale(0, 0.5f);
            
        }
    }
}