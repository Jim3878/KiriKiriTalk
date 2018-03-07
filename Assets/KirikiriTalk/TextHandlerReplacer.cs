using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk
{

    public class ReplaceParser : KiriParser
    {
        BreakOrder order;

        string orderTitle;
        string newString;

        public ReplaceParser(string orderTitle,string newString)
        {
            this.orderTitle = orderTitle;
            this.newString = newString;
        }

        protected override bool IsMatch(BreakOrder order)
        {
            this.order = order;
            return order.title == orderTitle.ToLower();
        }

        protected override void Parse(KirikiriController ctrl, BreakOrder order)
        {
            ctrl.textHandler.text = ctrl.textHandler.text.Replace(order.stringWithBrackets, newString);
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
            List<BreakOrder> temp = new List<BreakOrder>();
            while (flow.textHandler.TryToNext())
            {
                if (flow.textHandler.isFunction)
                {
                    temp.Add(flow.textHandler.breakOrder);
                }
            }
            foreach (var order in temp)
            {
                ReplaceParser rp = replacerList.Find((x) => x.IsMyCase(order));
                if (rp != null)
                {
                    flow.SetCommand(rp);
                }
            }
            flow.textHandler.Reset();

        }
    }
}