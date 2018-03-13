using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCompiler
{
    private CharOutputFactory defaultFactory;
    protected List<IDialogUnit> dialogUnitList = new List<IDialogUnit>();
    private List<IDialogUnitFactory> dialogUnitFactoryList = new List<IDialogUnitFactory>();

    public DialogCompiler(params IDialogUnitFactory[] dialogUnitFactory)
    {
        dialogUnitFactoryList.AddRange(dialogUnitFactory);
    }

    public IDialogUnit[] Build(string dialog)
    {
        defaultFactory = new CharOutputFactory();
        var dialogChar = dialog.ToCharArray();
        for (int i = 0; i < dialogChar.Length; i++)
        {
            if (dialogChar[i] != '[')
            {
                //一般文字
                dialogUnitList.Add(defaultFactory.BuildDialogUnit(dialogChar[i].ToString()));
            }
            else
            {
                //將被中括號包住的字串切割出來，並將迴圈的指標移至右括號後面
                int leftBracket = i;
                int rightBracket = LocateNextBrackets(dialogChar, leftBracket);
                int unitLength = rightBracket - leftBracket - 1;
                i = rightBracket + 1;
                string subDialog = new string(dialogChar, leftBracket + 1, unitLength);

                //將字串轉成dialogUnity
                dialogUnitList.Add(BuildToDialogUnit(subDialog));
            }
        }
        return dialogUnitList.ToArray();
    }

    int LocateNextBrackets(char[] charArr, int startIndex)
    {
        try
        {
            for (int i = startIndex + 1; i < charArr.Length; i++)
            {
                if (charArr[i] == ']')
                {
                    return i;
                }
            }
            throw new Exception(string.Format("完了，居然找不到下一個']'，終於只能報錯了嗎……？\n剩餘字串 = {0}",
                new string(charArr).Substring(startIndex)));
        }
        catch (Exception)
        {
            throw;
        }
    }

    IDialogUnit BuildToDialogUnit(string dialog)
    {
        try
        {
            foreach (IDialogUnitFactory factory in dialogUnitFactoryList)
            {
                if (factory.CanBuild(dialog))
                {
                    return factory.BuildDialogUnit(dialog);
                }
            }
            throw new Exception(string.Format("沒有……哪裡都沒有可以用的dialogUnitFactory……\ndialog={0}", dialog));
        }
        catch (Exception)
        {
            throw;
        }
    }


}
