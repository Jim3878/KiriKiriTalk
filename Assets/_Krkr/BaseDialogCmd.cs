using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public interface IDialogCmd { }

    public class BaseDialogCmd : IState, IDialogCmd
    {
        protected IKrController krController;

        public void SetKrProperty(IKrController ctrl)
        {
            this.krController = ctrl;
        }
    }
}