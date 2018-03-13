using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogUnitFactory
{
    //判斷字串能否轉換 - 生成用
    bool CanBuild(string dialog);
    //將字串轉換成DialogUnit - 生成用
    IDialogUnit BuildDialogUnit(string dialog);
}
