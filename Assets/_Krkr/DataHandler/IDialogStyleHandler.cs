using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public interface IDialogStyleHandler
    {
        int count { get; }

        void AddStyle(TextStyle style);
        void RemoveStyle(TextStyle style);
        void RemoveStyle(string header);
        string GetLeftStyle();
        string GetLeftStyle(int index);
        string GetRightStyle();
        string GetRightStyle(int index);
        bool HasStyle(string header);
        TextStyle GetStyle(string header);
    }
}