using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface IFactoryInputCompiler
    {
        Dictionary<string, string> ToUnitInput(string text);
    }

