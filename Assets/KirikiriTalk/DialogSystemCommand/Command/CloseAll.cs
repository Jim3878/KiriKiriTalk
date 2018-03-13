using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace KirikiriTalk
{
    public class CloseAll : IKirikiriCommand
    {
        public void Excute(KirikiriController ctrl)
        {
            ctrl.talkBody.DOScale(0, 0.5f);
            for(int i = 0; i < ctrl.CharaLayer.childCount; i++)
            {
                ctrl.CharaLayer.GetChild(i).DOMove(ctrl.GetCharaPosition(CharaPosition.LEFT_LEFT).position, 0.5f);
            }
        }
    }
}