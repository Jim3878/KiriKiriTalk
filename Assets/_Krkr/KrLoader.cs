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
            var styleCtrl = new DialogStyleController();
            krCtrl = new KrController();
            krCtrl.onEventCall += onEvent;
            dialogQueue = new Queue<IDialogCmd[]>();
            dialogCompiler = new DialogCompiler( new TypeDialogFactory(styleCtrl,this, this), new FactoryInputCompiler());
            dialogCompiler.AddDialogUnitFactory(new SetSizeFactory(styleCtrl));

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
            dialogBubble.text += dialog;
        }

        public void Update()
        {
            krCtrl.StateUpdate();
        }
    }

}