using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuckManager;

namespace Krkr
{
    public class KrController
    {

        public Text dialogBox;

        private CmdManager cmdManager;

        private DialogCompiler dialogCompiler;
        private Queue<ICmd[]> dialogQueue;
        private DialogStyleHandler styleHandler;
        private DialogHandler dialogHandler;
        public WaitHandler waitHandler;
        private CharaNameHandler nameHandler;
        private SpeedHandler speedHandler;


        public event EventHandler onEvent;

        public KrController(Text dialogBox,GameObject waitSymbol,GameObject nameObject,Text nameBox)
        {

            cmdManager = new CmdManager(new IdleTask());

            //資料池初始化
            speedHandler = new SpeedHandler();
            dialogQueue = new Queue<ICmd[]>();
            styleHandler = new DialogStyleHandler();
            dialogHandler = new DialogHandler(dialogBox, styleHandler);
            waitHandler = new WaitHandler(waitSymbol);
            nameHandler = new CharaNameHandler(nameObject, nameBox);

            //文本編譯器初始化
            dialogCompiler = new DialogCompiler(new TypeDialogFactory(speedHandler, dialogHandler), new FactoryInputConverter());
            dialogCompiler.AddDialogUnitFactory(
                            new SetSizeFactory(styleHandler),
                            new WaitClickFactory(waitHandler),
                            new SetNameFactory(nameHandler));
            
        }

        public void AddDialogStr(string str)
        {
            this.dialogQueue.Enqueue(dialogCompiler.Build(str));
        }

        public void Kill(bool isComplete = true)
        {

        }

        public void Begin()
        {
            if (dialogQueue.Count > 0)
                cmdManager.AddDialogCmd(dialogQueue.Dequeue());
            else
            {
                Debug.LogError("[KrController]貯列內沒有任何對話");
            }
        }


        public void Update()
        {
            cmdManager.StateUpdate();
        }

        public void Resume()
        {
            waitHandler.Resume();
        }

        public void SetSkipLine(bool value)
        {
            cmdManager.isSkipLine = value;
        }

        public void AddComdFactory(params IDialogCmdFactory[] factorys)
        {
            foreach (var f in factorys)
            {
                dialogCompiler.AddDialogUnitFactory(f);
            }
        }

    }
}