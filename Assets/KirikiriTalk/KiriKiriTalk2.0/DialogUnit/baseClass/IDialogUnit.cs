using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogUnit
{
    int ID { get; }
    //執行
    void Excute(ITypewriter typewriter);
    //立即完成當前功作
    void Complete(ITypewriter typewriter);
}
