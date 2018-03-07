using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace KirikiriTalk
{
    public class ZoomInOnStartCommand  : IKirikiriCommand
    {
        float duration;
        Ease ease;
        public ZoomInOnStartCommand() {
            duration = 0.5f;
            ease = Ease.Linear;
        }

        public void Excute(KirikiriController ctrl)
        {
            ctrl.talkBody.localScale = Vector3.zero;
            ctrl.onDialogStart += OnControllerStart;
        }

        public void OnControllerStart(object sender,EventArgs e)
        {
            if(sender is KirikiriController)
            {
                KirikiriController talk = (KirikiriController)sender;
                talk.talkBody.transform.DOScale(1, duration).SetEase(ease);
                talk.onDialogStart -= OnControllerStart;
            }
        }
    }
}