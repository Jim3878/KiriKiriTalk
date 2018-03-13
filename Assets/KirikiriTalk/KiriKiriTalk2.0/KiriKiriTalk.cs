using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KiriKiriTalk : MonoBehaviour
{

    [SerializeField]
    Text dialogField;
    float beginTime;
    IDialogUnit[] dialogUnitArr;

    [HideInInspector]
    public float delayTime;
    [HideInInspector]
    public int currentDialogUnit;
    [HideInInspector]
    List<TextStyle> currentStyle = new List<TextStyle>();
    public bool isPause = false;
    public bool isEnd
    {
        get
        {
            return currentDialogUnit == dialogUnitArr.Length - 1;
        }
    }
    #region TextStyle
    public void AddStyle(TextStyle style)
    {
        RemoveStyle(style);
        currentStyle.Add(style);
    }

    public void RemoveStyle(TextStyle style)
    {
        RemoveStyle(style.header);
    }

    public void RemoveStyle(string header)
    {
        currentStyle.RemoveAll((s) => s.header == header);
    }

    public string GetLeftStyle()
    {
        string leftStyle = "";
        for (int i = 0; i < currentStyle.Count; i++)
        {
            leftStyle += currentStyle[i];
        }
        return leftStyle;
    }

    public string GetRightStyle()
    {
        string rightStyle = "";
        for (int i = currentStyle.Count - 1; i >= 0; i--)
        {
            rightStyle += currentStyle[i];
        }
        return rightStyle;
    }

    #endregion

    public virtual void AddDialog(string dialog)
    {
        dialogField.text += dialog;
    }

    public void LaunchKiriKiri(IDialogUnit[] dialogCtrlArr)
    {
        this.dialogUnitArr = dialogCtrlArr;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogUnitArr != null && !isEnd && !isPause)
        {
            if (Time.time - beginTime > delayTime)
            {
                beginTime = Time.time;
                dialogUnitArr[currentDialogUnit].Excute(this);
                currentDialogUnit++;
            }
        }
    }

}
