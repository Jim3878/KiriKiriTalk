using KirikiriTalk.Parser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk
{
    public static class DialogUnitUtility
    {
        public static VariableToken GetTokenByName(this DialogUnit order, string tokenName)
        {
            return order.variableTokenList.Find((x) => x.name == tokenName);
        }

        public static int GetTokenIntValueByName(this DialogUnit order, string tokenName)
        {
            return (int)order.variableTokenList.Find((x) => x.name == tokenName).value;
        }

        public static float GetTokenFloatValueByName(this DialogUnit order, string tokenName)
        {
            return (float)order.variableTokenList.Find((x) => x.name == tokenName).value;
        }

        public static string GetTokenStringValueByName(this DialogUnit order, string tokenName)
        {
            return (string)order.variableTokenList.Find((x) => x.name == tokenName).value;
        }

        public static bool HasToken(this DialogUnit order, string tokenName)
        {
            return order.GetTokenByName(tokenName) != null;
        }

        public static bool HasToken(this DialogUnit order, string tokenName, VariableType dataType)
        {
            return order.HasToken(tokenName) && order.GetTokenByName(tokenName).dataType == dataType;
        }
    }
}