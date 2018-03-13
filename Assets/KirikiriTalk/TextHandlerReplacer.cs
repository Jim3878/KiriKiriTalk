using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk
{

    public class ReplaceParser : KiriParser
    {
        DialogUnit dialogUnit;

        string orderTitle;
        string newString;

        public ReplaceParser(string orderTitle,string newString)
        {
            this.orderTitle = orderTitle;
            this.newString = newString;
        }

        protected override bool IsMatch(DialogUnit dialogUnit)
        {
            this.dialogUnit = dialogUnit;
            return dialogUnit.header == orderTitle.ToLower();
        }

        protected override void Parse(KirikiriController ctrl, DialogUnit dialogUnit)
        {
            ctrl.dialogCtrl.text = ctrl.dialogCtrl.text.Replace(dialogUnit.stringWithBrackets, newString);
        }
    }



    public class TextHandlerReplacHandler : IKirikiriCommand
    {
        List<ReplaceParser> replacerList;
        public TextHandlerReplacHandler(params ReplaceParser[] replacerArray)
        {
            replacerList = new List<ReplaceParser>(replacerArray);
            
        }

        public void Excute(KirikiriController flow)
        {
            List<DialogUnit> temp = new List<DialogUnit>();
            while (flow.dialogCtrl.TryToNext())
            {
                if (flow.dialogCtrl.isFunction)
                {
                    temp.Add(flow.dialogCtrl.currentDialogUnit);
                }
            }
            foreach (var dialogUnit in temp)
            {
                ReplaceParser rp = replacerList.Find((x) => x.IsMyCase(dialogUnit));
                if (rp != null)
                {
                    flow.SetCommand(rp);
                }
            }
            flow.dialogCtrl.Reset();

        }
    }
}