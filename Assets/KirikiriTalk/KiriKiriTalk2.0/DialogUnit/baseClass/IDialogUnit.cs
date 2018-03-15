using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogUnit
{
    //執行
    void Excute(KiriTalk talk);

    //立即完成當前功作
    void Complete(KiriTalk talk);
}
