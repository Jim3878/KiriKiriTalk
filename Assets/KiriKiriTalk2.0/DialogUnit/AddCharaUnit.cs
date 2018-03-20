using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AddCharaUnit : BaseCompletableDialogUnit
{
    AddCharaUnitFactory mFactory
    {
        get
        {
            return factory as AddCharaUnitFactory;
        }
    }

    Tween tween;
    string name, from, to, file;
    float duration;

    public AddCharaUnit(int ID, string name, string file, string from, string to, float duration, IDialogUnitFactory factory) : base(ID, factory)
    {
        this.name = name;
        this.from = from;
        this.to = to;
        this.duration = duration;
        this.file = file;
        
    }

    public override void InnerComplete(ITypewriter typeWriter)
    {
        if (tween == null)
            throw new Exception("tween 消失了!");
        tween.Complete();
    }

    protected override void InnerExcute(ITypewriter typeWriter)
    {
        tween = mFactory.moveChara(name, file, from, to, duration);
        tween.OnComplete(() =>
        {
            base.RemoveFromRunning(typeWriter);
        });
    }
}
