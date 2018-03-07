using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirikiriTalk
{

    public abstract class DialogDecorator
    {
        protected KirikiriController system;

        public DialogDecorator(KirikiriController system)
        {
            this.system = system;
        }

        public abstract string Decorate(string nextChar);

        
    }
}
