using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Krkr
{
    public class DialogHandler : IDialogHandler
    {
        private DialogStyleHandler styleHandler;
        private Text dialogBox;

        public DialogHandler(Text dialogBox,  DialogStyleHandler styleHandler)
        {
            this.styleHandler = styleHandler;
            this.dialogBox = dialogBox;
        }

        public void Clear()
        {
            dialogBox.text = "";
        }

        public void TypeDialog(string dialog)
        {
            dialogBox.text += styleHandler.GetLeftStyle() + dialog + styleHandler.GetRightStyle();
        }
    }
}