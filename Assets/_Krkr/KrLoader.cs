using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Krkr
{
    public class KrLoader : IKrLoader, IDialogController, ISpeedController
    {
        private readonly float defaulDelay = 0.05f;
        private float _delay = 0.05f;
        private Text dialogBubble;
        private KrController krCtrl;
        private DialogCompiler dialogCompiler;
        private Queue<IDialogCmd[]> dialogQueue;
        private IDialogStyleHandler styleCtrl;

        public float delay
        {
            get
            {
                return _delay;
            }
            set
            {
                _delay = value;
            }
        }
        public float speed
        {
            get
            {
                if (_delay == 0)
                {
                    return -1;
                }
                return 1 / speed;
            }

            set
            {
                if (speed == 0)
                {
                    _delay = defaulDelay;
                }
                else
                {
                    _delay = 1 / value;
                }
            }
        }
        public event EventHandler onEvent;

        public KrLoader(Text dialogBubble)
        {
            krCtrl = new KrController();
            krCtrl.onEventCall += onEvent;
            dialogQueue = new Queue<IDialogCmd[]>();

            dialogCompiler = new DialogCompiler(new TypeDialogFactory(this, this), new FactoryInputCompiler());

            this.dialogBubble = dialogBubble;
        }

        public void AddDialogStr(string str)
        {
            this.dialogQueue.Enqueue(dialogCompiler.Build(str));
        }

        public void Clear()
        {
            dialogBubble.text = "";
        }

        public void Kill(bool isComplete = true)
        {

        }

        public void Start()
        {
            if (dialogQueue.Count > 0)
                krCtrl.AddDialogCmd(dialogQueue.Dequeue());
            else
            {
                Debug.LogError("[KrController]貯列內沒有任何對話");
            }
        }

        public void TypeDialog(string dialog)
        {
            dialogBubble.text += styleCtrl.GetLeftStyle() + dialog + styleCtrl.GetRightStyle();
        }

        public void Update()
        {
            krCtrl.StateUpdate();
        }

        public void AddComdFactory(params IDialogCmdFactory[] factorys)
        {
            foreach (var f in factorys)
            {
                dialogCompiler.AddDialogUnitFactory(f);
            }
        }

        public void SetProperty(IDialogStyleHandler styleController)
        {
            this.styleCtrl = styleController;
        }
    }
}