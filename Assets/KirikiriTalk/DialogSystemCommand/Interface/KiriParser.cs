﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk
{
    public abstract class KiriParser : IKirikiriCommand
    {
        bool isMatch=false;
        BreakOrder order;
        public void Excute(KirikiriController ctrl)
        {
            if (isMatch)
            {
                Parse(ctrl,order);
            }else
            {
                Debug.LogError("錯誤 : 未通過IsMyCase()檢測便解析");
            }
        }

        protected abstract void Parse(KirikiriController ctrl,BreakOrder order);

        protected abstract bool IsMatch(BreakOrder order);

        protected bool IsMatch(string tokenName)
        {
            return order.GetTokenByName(tokenName) != null;
        }

        public bool IsMyCase(BreakOrder order)
        {
            this.order = order;
            if (IsMatch(order))
            {
                isMatch = true;
                this.order = order;
                return true;
            }
            this.order = null;
            isMatch = false;
            return false;
        }
    }
}