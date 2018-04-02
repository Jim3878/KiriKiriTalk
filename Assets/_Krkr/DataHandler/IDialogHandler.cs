using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public interface IDialogHandler
    {
        void TypeDialog(string dialog);
        void Clear();
    }
}