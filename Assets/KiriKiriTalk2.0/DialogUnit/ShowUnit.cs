using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowUnit : BaseCompletableDialogUnit
{
    Transform dialogBubble;
    Tween tween;

    public ShowUnit(int ID,Transform dialogBubble, IDialogUnitFactory factory) : base(ID, factory)
    {
        this.dialogBubble = dialogBubble;
    }

    public override void InnerComplete(ITypewriter typeWriter)
    {
        if (tween != null)
        {
            tween.Complete();
        }
    }

    protected override void InnerExcute(ITypewriter typeWriter)
    {
        dialogBubble.gameObject.SetActive(true);
        tween = dialogBubble.DOScaleX(1, 0.5f).OnComplete(() =>
        {
            base.RemoveFromRunning(typeWriter);
        });
    }
}
