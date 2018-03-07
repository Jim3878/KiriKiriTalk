using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KirikiriTalk
{
    public interface IKirikiriCommand
    {
        void Excute(KirikiriController ctrl);
    }
}