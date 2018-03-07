using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Yeh.Parser;

namespace KirikiriTalk
{
    public class AddCharaCommand : KiriParser
    {
        Vector3 from, to;
        Sprite sprite;
        string charaName;
        float duration;


        public AddCharaCommand SetPath(Vector3 from, Vector3 to, float duration)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            return this;
        }
        public AddCharaCommand SetPath(Vector3 to, float duration)
        {
            this.from = Vector3.zero;
            this.to = to;
            this.duration = duration;
            return this;
        }
        protected override bool IsMatch(BreakOrder order)
        {
            return order.IsTitle("setChara") &&
                order.HasToken("to",DataType.STRING)&&
                order.HasToken("from",DataType.STRING)&&
                (order.HasToken("duration",DataType.FLOAT) 
                || order.HasToken("duration",DataType.INT));
        }

        protected override void Parse(KirikiriController ctrl, BreakOrder order)
        {
            
            CharaPosition fromToken 
                = KirikiriController.CharaPositionParser(order.GetTokenStringValueByName("from"));
            CharaPosition toToken 
                = KirikiriController.CharaPositionParser(order.GetTokenStringValueByName("to"));
            String fileToken = order.GetTokenByName("file").value.ToString();
            String charaName = order.GetTokenByName("name").value.ToString();
            float duration = (float)order.GetTokenByName("duration").value;
            CharaBoard cb = CharaBoard.Instance(charaName.ToString(), Resources.Load<Sprite>(fileToken.ToString()), ctrl);
            cb.SetPosition(fromToken);

            cb.transform.DOMove(ctrl.GetCharaPosition(toToken).position, duration);

        }
    }
}