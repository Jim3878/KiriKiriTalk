using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public interface IKrLoader
    {
        event EventHandler onEvent;
        void AddComdFactory(params IDialogCmdFactory[] factory);
        void AddDialogStr(string s);
        void Start();
        void Kill(bool isComplete = true);
        void Update();
    }
}