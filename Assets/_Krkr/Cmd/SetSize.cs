using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr {
    public class SetSize : BaseDialogCmd{

        IDialogStyleHandler styleController;
        int size;

        public SetSize(IDialogStyleHandler styleController,int size)
        {
            this.size = size;
            this.styleController = styleController;
        }

        public override void StateUpdate()
        {
            styleController.AddStyle(new TextStyle("Size",size.ToString()));
            krController.FlyToNextCmd();
        }

    }
}