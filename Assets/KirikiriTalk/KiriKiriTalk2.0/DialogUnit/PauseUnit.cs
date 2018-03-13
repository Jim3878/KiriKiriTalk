using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUnit : BaseDialogUnit
{
    public override void Excute(KiriKiriTalk kirikiriTalke)
    {
        kirikiriTalke.isPause = true;
    }
}
