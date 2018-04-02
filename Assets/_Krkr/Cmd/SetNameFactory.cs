using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class SetNameFactory : IDialogCmdFactory
    {
        ICharaNameHandler nameHandler;

        public SetNameFactory(ICharaNameHandler nameHandler)
        {
            this.nameHandler = nameHandler;
        }

        public ICmd BuildDialogUnit(Dictionary<string, string> metaDialog)
        {
            return new SetName(metaDialog["name"].ToStringNormalize(), nameHandler);
        }

        public bool CanBuild(Dictionary<string, string> meta)
        {
            return meta.Count == 1 && meta.ContainsKey("name") && meta["name"].IsString();
        }
    }
}