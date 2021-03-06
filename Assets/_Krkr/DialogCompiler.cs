﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class DialogCompiler
    {
        private IDialogCmdFactory defaultFactory;
        protected List<ICmd> dialogUnitList = new List<ICmd>();
        private List<IDialogCmdFactory> dialogUnitFactoryList = new List<IDialogCmdFactory>();
        private IFactoryInputConverter unitInputParser;

        public DialogCompiler(IDialogCmdFactory defaultFactory, IFactoryInputConverter unitInputParser)
        {
            this.unitInputParser = unitInputParser;
            this.defaultFactory = defaultFactory;
        }

        public void AddDialogUnitFactory(params IDialogCmdFactory[] dialogUnitFactory)
        {
            dialogUnitFactoryList.AddRange(dialogUnitFactory);
        }

        public ICmd[] Build(string dialog)
        {
            this.dialogUnitList = new List<ICmd>();
            Dictionary<string, string> dialogUnitInput;
            var dialogChar = dialog.ToCharArray();
            //Debug.Log(dialogChar.Length);
            for (int i = 0; i < dialogChar.Length; i++)
            {
                if (dialogChar[i] != '[')
                {
                    //一般文字
                    dialogUnitInput = new Dictionary<string, string>();
                    dialogUnitInput.Add("dialog", dialogChar[i].ToString());
                    dialogUnitList.Add(defaultFactory.BuildDialogUnit( dialogUnitInput));
                }
                else
                {
                    try
                    {
                        //將被中括號包住的字串切割出來，並將迴圈的指標移至右括號後面
                        int length = dialogChar.FindRightBrackets(i) - i + 1;

                        //轉字串、去空白、變小寫、一氣呵成
                        string targetValue = new string(dialogChar, i, length).RemoveBrackets().ToLower();
                        Dictionary<string, string> keyValuePairs = unitInputParser.ToUnitInput(targetValue);
                        dialogUnitList.Add(BuildToDialogUnit(keyValuePairs));

                        i += length - 1;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Debug.Log(string.Format("dialogChar={2}\ni={0}\nlength={1}", i, dialogChar.Length, dialog));
                        throw;
                    }
                }
            }
            return dialogUnitList.ToArray();
        }

        ICmd BuildToDialogUnit(Dictionary<string, string> keyValuePairs)
        {
            foreach (var factory in dialogUnitFactoryList)
            {
                if (factory.CanBuild(keyValuePairs))
                {
                    return factory.BuildDialogUnit( keyValuePairs);
                }
            }
            throw new ArgumentException(string.Format("沒有……哪裡都沒有可以用的unitFactory……\ndialog={0}", keyValuePairs.ToDebugString()));
        }
    }
}