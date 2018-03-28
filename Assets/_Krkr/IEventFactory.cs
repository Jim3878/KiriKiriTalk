using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Krkr
{
    public interface IEventFactory
    {
        EventArgs CreatEventArgs(object o);
    }
}