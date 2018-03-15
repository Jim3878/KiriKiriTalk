using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KiriTalk : MonoBehaviour
{
    //介面
    [SerializeField]
    Text dialogField;
    public string currentDialogText
    {
        get
        {
            return dialogField.text;
        }
    }

    //輸入值
    IDialogUnit[] dialogUnitArr;

    //時序管理
    [HideInInspector]
    public float delayTime = 0.5f;
    float beginTime;
    [HideInInspector]
    int currentDialogUnitIndex;

    //其它
    [HideInInspector]
    List<TextStyle> currentStyle = new List<TextStyle>();
    string _log = "";
    KiriStateController _stateController;

    public List<IDialogUnit> runningDialogUnitList = new List<IDialogUnit>();

    public bool isRunning = false;
    public bool isPause = false;
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
        _log += string.Format("\nAddDialog({0})\n{1}\n\n", dialog, KiriTalkUtility.CallStack());
    }

    public void LaunchKiriKiri(IDialogUnit[] dialogCtrlArr)
    {
        if (!isRunning)
        {
            isRunning = true;
            _stateController = new KiriStateController();
            this.dialogUnitArr = dialogCtrlArr;
            _stateController.Start(new NormalState(this));
        }
        _log += string.Format("\nLaunchKiriKiri()\n{0}\n{1}\nisRunning = {2}\n\n", dialogCtrlArr.Length, KiriTalkUtility.CallStack(),isRunning);
    }

    // Update is called once per frame
    void Update()
    {
        _stateController.StateUpdate();
    }

    public IDialogUnit GetCurrentDialogUnit()
    {
        return dialogUnitArr[currentDialogUnitIndex];
    }

    public void NextDialogUnit()
    {
        if (currentDialogUnitIndex < dialogUnitArr.Length - 1)
        {
            currentDialogUnitIndex++;
            _log += string.Format("\nNextDialogUnit()\ncurrentDialogUnitIndex = {0}\n{1}\n\n", currentDialogUnitIndex, KiriTalkUtility.CallStack());
        }
        else
        {
            _log += string.Format("\nNextDialogUnit()\nisRunning = false\n\n", KiriTalkUtility.CallStack());
            isRunning = false;
        }
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Terminate()
    {
        _stateController.Terminate();
        isRunning = false;
        _log += string.Format("\nTerminate()\n{1}\n\n", KiriTalkUtility.CallStack());
    }

    public void Restart()
    {
        if (isRunning)
        {
            throw new NotImplementedException();
        }
    }

    public void ExcuteDialogUnit(IDialogUnit dialogUnit)
    {
        this.runningDialogUnitList.Add(dialogUnit);
        dialogUnit.Excute(this);
    }

    public void TransTo(BaseKiriState state)
    {
        _stateController.TransTo(state);
        _log += string.Format("\nTransTo({0})\n{1}\n\n", state, KiriTalkUtility.CallStack());
    }

    public void ForTest()
    {
        Debug.Log("如果你在不是Test的情況下見到這行字，代表你幹了蠢事。\n" + KiriTalkUtility.CallStack());
        GameObject go = new GameObject();
        go.AddComponent<Text>();
        dialogField = go.GetComponent<Text>();
    }

    #region TRACE_LOG
    [ContextMenu("Trace Log")]
    public void TraceLog()
    {
        string dataName = "/KiriTalk_Log.txt";
        System.IO.File.WriteAllText(Application.persistentDataPath + dataName, _log);
        Debug.Log(string.Format("我已經把Log檔丟到{0}了", Application.persistentDataPath + dataName));
    }

    [ContextMenu("Trace Log")]
    public void ClearLog()
    {
        _log = "";
        Debug.Log(string.Format("l…log檔怎麼空了？"));
    }
    #endregion

}
