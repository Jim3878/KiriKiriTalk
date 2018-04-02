using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class TypeDialog : IDialogCmd
    {
        //字串隊列
        Queue<string> dialogQueue = new Queue<string>();
        float lastTypeTime = 0;
        ISpeedHandler speedCtrl;
        IDialogHandler dialogCtrl;

        public TypeDialog( ISpeedHandler speedCtrl, IDialogHandler dialogCtrl)
        {
            lastTypeTime = 0;
            this.speedCtrl = speedCtrl;
            this.dialogCtrl = dialogCtrl;
        }

        public override void StateBegin()
        {
            lastTypeTime = Time.time + speedCtrl.delay;
            base.StateBegin();
        }
        public void PushDialog(string dialog)
        {
            dialogQueue.Enqueue(dialog);
        }

        public override void StateUpdate()
        {
            if (queueController.isSkipLine)
            {
                KeyIn();
                queueController.TranstToNextCmd();
            }
            else if (Time.time - lastTypeTime > speedCtrl.delay)
            {
                KeyIn();
                queueController.TranstToNextCmd();
            }
        }

        private void KeyIn()
        {
            dialogCtrl.TypeDialog(dialogQueue.Dequeue());
        }
        public override void StateEnd()
        {
            lastTypeTime = Time.time;
        }
    }
}