using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class WaitClickFactory : IDialogCmdFactory
    {
        private WaitClick _waitClick;

        public WaitClickFactory(IWaitHandler controller)
        {
            this._waitClick = new WaitClick(controller);
        }
      
        public IDialogCmd BuildDialogUnit(Dictionary<string, string> metaDialog)
        {
            return _waitClick;
        }

        public bool CanBuild(Dictionary<string, string> meta)
        {
            return meta.Count == 1 && meta.ContainsKey("waitclick");
        }
    }
}