using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class SetSizeFactory : IDialogCmdFactory
    {
        IDialogStyleController styleController;
        public SetSizeFactory(IDialogStyleController styleController)
        {
            this.styleController = styleController;
        }
        public IDialogCmd BuildDialogUnit( Dictionary<string, string> metaDialog)
        {
            return new SetSize(styleController, metaDialog["size"].ToInt());
        }

        public bool CanBuild(Dictionary<string, string> metaDialog)
        {
            return metaDialog.Count == 1 && metaDialog.ContainsKey("size") && metaDialog["size"].IsInt();
        }
    }
}