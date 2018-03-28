using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class TypeDialog : BaseDialogCmd
    {
        Queue<string> dialogQueue = new Queue<string>();
        float lastTypeTime = 0;
        ISpeedController speedCtrl;
        IDialogStyleController styleCtrl;
        IDialogController dialogCtrl;

        public TypeDialog(IDialogStyleController styleCtrl, ISpeedController speedCtrl, IDialogController dialogCtrl)
        {
            lastTypeTime = 0;
            this.styleCtrl = styleCtrl;
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
            if (krController.isSkip)
            {
                KeyIn();
                krController.TranstToNextCmd();
            }
            else if (Time.time - lastTypeTime > speedCtrl.delay)
            {
                KeyIn();
                krController.TranstToNextCmd();
            }
        }

        private void KeyIn()
        {
            dialogCtrl.TypeDialog(styleCtrl.GetLeftStyle() + dialogQueue.Dequeue() + styleCtrl.GetRightStyle());
        }
        public override void StateEnd()
        {
            lastTypeTime = Time.time;
        }
    }
}