using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class TypeDialogFactory : IDialogCmdFactory
    {
        TypeDialog typer;
        ISpeedController speedCtrl;
        IDialogController dialogCtrl;
        //IDialogStyleController styleCtrl;

        public TypeDialogFactory(ISpeedController speedCtrl, IDialogController dialogCtrl)
        {
            //this.styleCtrl = styleCtrl;
            this.speedCtrl = speedCtrl;
            this.dialogCtrl = dialogCtrl;
        }

        public IDialogCmd BuildDialogUnit(Dictionary<string, string> keyValuePairs)
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
