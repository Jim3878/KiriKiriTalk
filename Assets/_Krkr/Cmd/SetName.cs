using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class SetName:BaseDialogCmd
    {
        string _name;
        ICharaNameHandler _nameHandler;

        public SetName(string name,ICharaNameHandler nameHandler)
        {
            _name = name;
            this._nameHandler = nameHandler;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            _nameHandler.SetName(_name);
            krController.FlyToNextCmd();
        }

    }
}