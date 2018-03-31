using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class FactoryInputCompiler : IFactoryInputCompiler
    {
        public Dictionary<string, string> ToUnitInput(string text)
        {
            Dictionary<string, string> unitInput = new Dictionary<string, string>();

            string[] preParse = text.SpiltToField();
            string[] keyValuePair;
            for (int i = 0; i < preParse.Length; i++)
            {
                keyValuePair = preParse[i].Split('=');
                if (keyValuePair.Length == 0)
                {
                    throw new ArgumentException(string.Format("有無值的區間\n{0}", text));
                }
                else if (keyValuePair.Length == 1)
                {
                    unitInput.Add(keyValuePair[0], null);
                }
                else if (keyValuePair.Length == 2)
                {
                    unitInput.Add(keyValuePair[0], keyValuePair[1]);
                }
                else
                {
                    throw new ArgumentException(string.Format("檢測到大量等號！\n{0}", text));
                }
            }

            return unitInput;
        }
    }
}
