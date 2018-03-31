using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public interface IDialogController
    {
        void SetProperty(IDialogStyleHandler styleController);
        void TypeDialog(string dialog);
        void Clear();
    }
}