using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace KirikiriTalk
{
    public class CommandEventArgs : EventArgs
    {
        public IKirikiriCommand command;
        public CommandEventArgs(IKirikiriCommand command)
        {
            this.command = command;
        }
    }

    public enum CharaPosition
    {
        Left,
        Right,
        Middle,
        LeftLeft,
        RightRight,
        LeftDown,
        RightDown,
        MiddleDown,
    }

    public class KirikiriController : MonoBehaviour
    {
        
        public static CharaPosition CharaPositionParser(string s)
        {
            return (CharaPosition)Enum.Parse(typeof(CharaPosition),s);
        }

        static string prefabsPath = "KirikiriTalk";


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
        public float delayPerChar;
        public int defaultFontSize;
        public Color defaultFontColor;

        

        [HideInInspector]
        public DialogTextHandler textHandler;
        [HideInInspector]
        public float time;
        bool isPause;

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
            this.textHandler = new DialogTextHandler(text);
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
            textHandler.Reset();
            dialogText.text = "";
            isPause = false;
            isDisplayDialog = true;
            time = 0;
            HandleNext();
        }
        

        public void Pause()
        {
            isPause = true;
        }
        public void Continue()
        {
            isPause = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isDisplayDialog && !isPause)
            {
                if (Yeh.Tool.Tools.CheckPerDeltaTime(ref time, delayPerChar))
                {
                    HandleNext();
                }
            }
        }

        void HandleNext()
        {
            if (textHandler.TryToNext())
            {
                if (!textHandler.isFunction)
                {
                    this.nextChar = textHandler.currentChar;
                    if (beforeDialogPrint != null)
                        beforeDialogPrint(this, new EventArgs());
                    UpdatTextDialog();
                    if (afterDialogPrint != null)
                        afterDialogPrint(this, new EventArgs());
                }
                else
                {
                    this.FunctionParser(textHandler.breakOrder);
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
            time -= delayPerChar;
        }

        void UpdatTextDialog()
        {
            dialogText.text += this.nextChar;
        }
        

        void FunctionParser(BreakOrder order)
        {
            if (onPrintBreakOrder != null)
                onPrintBreakOrder(this, new BreakOrderEventArgs(order));
            bool isFoundCase=false;
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
                Debug.LogError("錯誤的指令 : " + order.allString);
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
