using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yeh.Parser;

namespace KirikiriTalk
{

    public class Token
    {
        public string name;
        public string operater;
        public object value;
        public DataType dataType;

        public Token(string name)
        {
            this.name = name.ToLower();
        }
        public Token(string name, string operater, string value)
        {
            this.name = name.ToLower();
            this.operater = operater;

            value.TryParser(out dataType, out this.value);
        }
    }
    public class BreakOrder
    {
        
        public string stringWithBrackets;
        public string allString;
        public string title
        {
            get
            {
                return allString.Replace(" ", "").Split(',')[0].ToLower();
            }
        }

        public List<Token> tokenList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origString">原字串</param>
        /// <param name="splitSymbol">分隔字元</param>
        /// <param name="operaters">可用的運算元</param>
        public BreakOrder(string stringWithBrackets, string allString, string splitSymbol, params string[] operaters)
        {
            this.stringWithBrackets = stringWithBrackets;
            this.allString = allString;
            this.SpilitBreakSymbol(splitSymbol, operaters);
        }

        public bool IsTitle(string title)
        {
            return this.title == title.ToLower();
        }



        public void SpilitBreakSymbol(string splitSymbol, params string[] operaters)
        {
            List<string> unSpilitTokenList = new List<string>(allString.Replace(" ","").Split(new string[] { splitSymbol }, System.StringSplitOptions.None));
            string[] spilitToken;
            tokenList = new List<Token>();
            bool hasOperater = false;


            foreach (string token in unSpilitTokenList)
            {
                for (int i = 0; i < operaters.Length; i++)
                {
                    if (token.Contains(operaters[i]))
                    {
                        hasOperater = true;
                        spilitToken = token.Split(new string[] { operaters[i] }, System.StringSplitOptions.None);
                        tokenList.Add(new Token(spilitToken[0], operaters[i], spilitToken[1]));
                        break;
                    }
                }
                if (!hasOperater)
                {
                    tokenList.Add(new Token(token));
                    hasOperater = false;
                }
            }
        }

    }

    public static class BreakOrderGetter
    {
        public static Token GetTokenByName(this BreakOrder order, string tokenName)
        {
            return order.tokenList.Find((x) => x.name == tokenName);
        }

        public static int GetTokenIntValueByName(this BreakOrder order, string tokenName)
        {
            return (int)order.tokenList.Find((x) => x.name == tokenName).value;
        }

        public static float GetTokenFloatValueByName(this BreakOrder order, string tokenName)
        {
            return (float)order.tokenList.Find((x) => x.name == tokenName).value;
        }

        public static string GetTokenStringValueByName(this BreakOrder order, string tokenName)
        {
            return (string)order.tokenList.Find((x) => x.name == tokenName).value;
        }

        public static bool HasToken(this BreakOrder order, string tokenName)
        {
            return order.GetTokenByName(tokenName) != null;
        }

        public static bool HasToken(this BreakOrder order, string tokenName, DataType dataType)
        {
            return order.HasToken(tokenName) && order.GetTokenByName(tokenName).dataType == dataType;
        }
    }
}