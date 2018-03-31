using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Krkr
{
    public interface IWaitHandler
    {
        bool isWait { get; }
        event EventHandler onResume;
        event EventHandler onWait;
        void Resume();
        void Wait();
    }
}