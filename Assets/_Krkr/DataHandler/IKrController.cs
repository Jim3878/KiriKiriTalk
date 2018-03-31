using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Krkr
{
    public interface IKrController
    {
        bool isSkip { get; }
        void TranstToNextCmd();
        void FlyToNextCmd();
        void CallEvent(EventArgs e);
    }
}