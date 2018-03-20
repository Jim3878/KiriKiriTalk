using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

public class DummyFactoryForCompiler : IDialogUnitFactory
{
    public IDialogUnit BuildDialogUnit(int ID, Dictionary<string, string> keyValuePairs)
    {
        return new DummyUnit(ID, keyValuePairs.ToDebugString());
    }

    public bool CanBuild(Dictionary<string, string> keyValuePairs)
    {
        return true;
    }

    public class DummyUnit : IDialogUnit
    {
        public int ID { set; get; }
        public string contain;

        public DummyUnit(int id, string contain)
        {
            this.ID = id;
            this.contain = contain;
        }

        public void Complete(ITypewriter typewriter)
        {

        }

        public void Excute(ITypewriter typewriter)
        {

        }
    }
}
