using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckManager;

namespace Krkr
{
    public interface ICmd { }

    public class IDialogCmd : ITask, ICmd
    {
        protected ICmdManager queueController
        {
            get
            {
                return controller as ICmdManager;
            }
        }
        
    }
}