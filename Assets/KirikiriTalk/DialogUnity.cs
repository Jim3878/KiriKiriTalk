using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KirikiriTalk.Parser;
using System;

namespace KirikiriTalk
{
    public class DialogUnitFactory
    {
        List<BaseDialogUnit> dialogUnitList = new List<BaseDialogUnit>();

        public void AddDialogUnit(params BaseDialogUnit[] dialogUnits)
        {
            dialogUnitList.AddRange(dialogUnits);
        }

        public BaseDialogUnit BuildDialogUnit(string dialog)
        {
            try
            {
                foreach (BaseDialogUnit dialogUnit in dialogUnitList)
                {
                    if (dialogUnit.CanConvert(dialog))
                    {
                        return dialogUnit.ParserToDialogUnit(dialog);
                    }
                }
                throw new Exception(string.Format("對、對不起，因為找不到能對應的DialogUnit，所以……\ndialog = {0}",dialog));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public abstract class BaseDialogUnit
    {
        //判斷字串能否轉換 - 生成用
        public abstract bool CanConvert(string dialog);
        //將字串轉換成DialogUnit - 生成用
        public abstract BaseDialogUnit ParserToDialogUnit(string dialog);
        //執行
        public abstract void Excute(KirikiriController ctrl);
    }

    public class DialogUnit
    {
        public bool isFunction;
        public string stringWithBrackets;
        public string wholeString;
        public string header;
        public List<VariableToken> variableTokenList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wholeString">原字串</param>
        /// <param name="splitSymbol">分隔字元</param>
        /// <param name="operaters">可用的運算元</param>
        public DialogUnit(string stringWithBrackets, string wholeString, string splitSymbol, params string[] operaters)
        {
            this.stringWithBrackets = stringWithBrackets;
            this.wholeString = wholeString;
            this.ConvertToToken(splitSymbol, operaters);
            this.header = wholeString.Replace(" ", "").Split(',')[0].ToLower();
        }

        public bool IsHeaderEqualTo(string header)
        {
            return this.header == header.ToLower();
        }

        public void ConvertToToken(string splitSymbol, params string[] operaters)
        {
            //將字串分割成token字串
            List<string> preTalkenList = new List<string>(wholeString.Replace(" ", "").Split(new string[] { splitSymbol }, System.StringSplitOptions.None));

            //將token字串Parser至token
            string[] spilitToken;
            variableTokenList = new List<VariableToken>();
            bool hasValue = false;
            foreach (string token in preTalkenList)
            {
                //判斷token內容是否有賦值
                for (int i = 0; i < operaters.Length; i++)
                {
                    if (token.Contains(operaters[i]))
                    {
                        hasValue = true;
                        spilitToken = token.Split(new string[] { operaters[i] }, System.StringSplitOptions.None);
                        variableTokenList.Add(new VariableToken(spilitToken[0], operaters[i], spilitToken[1]));
                        break;
                    }
                }
                if (!hasValue)
                {
                    variableTokenList.Add(new VariableToken(token));
                    hasValue = false;
                }
            }
        }

    }
}