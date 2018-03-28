using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{

    public interface IFactoryInputCompiler
    {
        Dictionary<string, string> ToUnitInput(string text);
    }
}
