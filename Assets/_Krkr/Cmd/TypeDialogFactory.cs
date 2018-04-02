using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class TypeDialogFactory : IDialogCmdFactory
    {
        TypeDialog typer;
        ISpeedHandler speedCtrl;
        IDialogHandler dialogCtrl;
        //IDialogStyleController styleCtrl;

        public TypeDialogFactory(ISpeedHandler speedCtrl, IDialogHandler dialogCtrl)
        {
            //this.styleCtrl = styleCtrl;
            this.speedCtrl = speedCtrl;
            this.dialogCtrl = dialogCtrl;
        }

        public ICmd BuildDialogUnit(Dictionary<string, string> keyValuePairs)
        {
            if (typer == null)
            {
                typer = new TypeDialog(speedCtrl, dialogCtrl);
            }
            typer.PushDialog(keyValuePairs["dialog"]);
            return typer;
        }

        public bool CanBuild(Dictionary<string, string> keyValuePairs)
        {
            return keyValuePairs.Count == 1 && keyValuePairs.ContainsKey("dialog");
        }
    }
}
