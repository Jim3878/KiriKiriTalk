using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public interface IDialogCmdFactory
    {
        //判斷字串能否轉換 - 生成用
        bool CanBuild(Dictionary<string, string> meta);
        //將字串轉換成DialogUnit - 生成用
        ICmd BuildDialogUnit(Dictionary<string, string> metaDialog);
    }
}
