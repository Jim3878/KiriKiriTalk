using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class SetSizeFactory : IDialogCmdFactory
    {
        IDialogStyleHandler styleController;
        public SetSizeFactory(IDialogStyleHandler styleController)
        {
            this.styleController = styleController;
        }
        public ICmd BuildDialogUnit( Dictionary<string, string> metaDialog)
        {
            return new SetSize(styleController, metaDialog["size"].ToInt());
        }

        public bool CanBuild(Dictionary<string, string> metaDialog)
        {
            return metaDialog.Count == 1 && metaDialog.ContainsKey("size") && metaDialog["size"].IsInt();
        }
    }
}