using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public interface IDialogStyleHandler
    {
        void AddStyle(TextStyle style);
        void RemoveStyle(TextStyle style);
        void RemoveStyle(string header);
        bool HasStyle(string header);
    }
}