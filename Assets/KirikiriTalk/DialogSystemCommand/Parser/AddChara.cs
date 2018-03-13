using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KirikiriTalk.Parser;

namespace KirikiriTalk
{
    public class AddChara : KiriParser
    {
        Vector3 from, to;
        Sprite sprite;
        string charaName;
        float duration;


        public AddChara SetPath(Vector3 from, Vector3 to, float duration)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            return this;
        }
        public AddChara SetPath(Vector3 to, float duration)
        {
            this.from = Vector3.zero;
            this.to = to;
            this.duration = duration;
            return this;
        }
        protected override bool IsMatch(DialogUnit order)
        {
            return order.IsHeaderEqualTo("setChara") &&
                order.HasToken("to",VariableType.STRING)&&
                order.HasToken("from",VariableType.STRING)&&
                (order.HasToken("duration",VariableType.FLOAT) 
                || order.HasToken("duration",VariableType.INT));
        }

        protected override void Parse(KirikiriController ctrl, DialogUnit order)
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