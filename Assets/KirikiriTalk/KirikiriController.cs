using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using KirikiriTalk.Event;

namespace KirikiriTalk
{

    public enum CharaPosition
    {
        LEFT,
        RIGHT,
        MIDDLE,
        LEFT_LEFT,
        RIGHT_RIGHT,
        LEFT_DOWN,
        RIGHT_DOWN,
        MIDDLE_DOWN,
    }

    public class KirikiriController : MonoBehaviour
    {
        #region Instance
        static string prefabsPath = "KirikiriTalk";
        public static CharaPosition CharaPositionParser(string s)
        {
            return (CharaPosition)Enum.Parse(typeof(CharaPosition), s.ToUpper());
        }
        #endregion

        #region VARIABLE

        #endregion

        [SerializeField]
        public Canvas canvas;
        [SerializeField]
        public Text dialogText;
        [SerializeField]
        public Text nameText;
        [SerializeField]
        public Image dialogBoxBG;
        [SerializeField]
        public Image nameBoxBG;
        [SerializeField]
        public Transform talkBody;
        [SerializeField]
        public Transform CharaLayer;
        [SerializeField]
        public Transform WaitingSymbol;
        [SerializeField]
        public Transform CharaPosition;
        public bool isDisplayDialog;
        public float charDelay;
        public float charDelayRatio = 1;
        public int defaultFontSize;
        public Color defaultFontColor;

        [HideInInspector]
        public DialogController dialogCtrl;
        [HideInInspector]
        public float time;
        public bool isPause { get { return _isPause; } }
        bool _isPause;

        List<DialogDecorator> dialogDecoratorList;
        List<KiriParser> dialogParserList;

        public string nextChar;
        public EventHandler onDialogStart;
        public EventHandler onDialogComplete;
        public EventHandler beforeDialogPrint;
        public EventHandler afterDialogPrint;
        public EventHandler<CommandEventArgs> onCommand;
        public EventHandler<BreakOrderEventArgs> onPrintBreakOrder;

        public static KirikiriController Instance(int sortingOrder = 99)
        {
            GameObject prefabs = Resources.Load<GameObject>(prefabsPath);
            prefabs = GameObject.Instantiate<GameObject>(prefabs);
            KirikiriController manager = prefabs.GetComponent<KirikiriController>();
            manager.Initialize(sortingOrder);

            return manager;
        }

        public Transform GetCharaPosition(CharaPosition position)
        {
            return CharaPosition.Find(position.ToString());
        }

        public KirikiriController AddParser(params KiriParser[] parser)
        {
            if (dialogParserList == null)
            {
                dialogParserList = new List<KiriParser>();
            }
            dialogParserList.AddRange(parser);
            return this;
        }

        public void Initialize(int sortingOrder)
        {
            canvas.sortingOrder = sortingOrder;
            isDisplayDialog = false;
        }

        public void SetDialogString(string text)
        {
            this.dialogCtrl = new DialogController(text);
        }

        public CharaBoard GetCharaBoardByName(String name)
        {
            CharaBoard cb = CharaLayer.Find(name).GetComponent<CharaBoard>();
            if (cb == null)
            {
                Debug.LogError("Missing chara " + name);
            }
            return cb;
        }

        public KirikiriController SetCommand(IKirikiriCommand cmd)
        {
            if (onCommand != null)
            {
                onCommand(this, new CommandEventArgs(cmd));
            }
            cmd.Excute(this);
            return this;
        }

        public void Play()
        {
            if (onDialogStart != null)
            {
                onDialogStart(this, new EventArgs());
            }
            dialogCtrl.Reset();
            dialogText.text = "";
            _isPause = false;
            isDisplayDialog = true;
            time = 0;
            HandleNext();
        }


        public void Pause()
        {
            _isPause = true;
        }
        public void Continue()
        {
            _isPause = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isDisplayDialog && !_isPause)
            {
                if (charDelay * charDelayRatio > 0)
                {
                    if (KirikiriTalk.Tool.Tools.CheckPerDeltaTime(ref time, charDelay * charDelayRatio))
                    {
                        HandleNext();
                    }
                }
                else
                {
                    while (isDisplayDialog && !_isPause && charDelay * charDelayRatio <= 0)
                    {
                        HandleNext();
                    }
                }
            }
        }

        void HandleNext()
        {

            if (dialogCtrl.TryToNext())
            {
                if (!dialogCtrl.isFunction)
                {
                    this.nextChar = dialogCtrl.currentChar;
                    if (beforeDialogPrint != null)
                        beforeDialogPrint(this, new EventArgs());
                    UpdatTextDialog();
                    if (afterDialogPrint != null)
                        afterDialogPrint(this, new EventArgs());
                }
                else
                {
                    this.FunctionParser(dialogCtrl.currentDialogUnit);
                    ToNextImmediately();
                }

            }
            else
            {
                isDisplayDialog = false;

                if (onDialogComplete != null)
                {
                    onDialogComplete(this, new EventArgs());
                }
                this.SetCommand(new CloseAll());
            }
        }

        public void ToNextImmediately()
        {
            time -= charDelay * charDelayRatio;
        }

        void UpdatTextDialog()
        {
            dialogText.text += this.nextChar;
        }


        void FunctionParser(DialogUnit order)
        {
            if (onPrintBreakOrder != null)
                onPrintBreakOrder(this, new BreakOrderEventArgs(order));
            bool isFoundCase = false;
            if (dialogParserList == null)
            {
                dialogParserList = new List<KiriParser>();
            }
            foreach (KiriParser k in dialogParserList)
            {
                if (k.IsMyCase(order))
                {
                    this.SetCommand(k);
                    isFoundCase = true;
                    break;
                }
            }
            if (!isFoundCase)
            {
                Debug.LogError("錯誤的指令 : " + order.wholeString);
            }
            //Debug.Log(order.title);
        }

        public void AddDialogDecorator(DialogDecorator decorator)
        {
            if (dialogDecoratorList == null)
            {
                dialogDecoratorList = new List<DialogDecorator>();
            }
            dialogDecoratorList.Add(decorator);
        }

        public void RemoveDialogDecorator(DialogDecorator decorator)
        {
            if (dialogDecoratorList == null)
            {
                dialogDecoratorList = new List<DialogDecorator>();
            }
            dialogDecoratorList.Remove(decorator);
        }
    }

}
