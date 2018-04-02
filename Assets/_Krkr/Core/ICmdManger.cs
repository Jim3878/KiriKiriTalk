using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Krkr
{
    public interface ICmdManager
    {
        bool isSkipLine { get; set; }
        void TranstToNextCmd();
        void FlyToNextCmd();
    }
}