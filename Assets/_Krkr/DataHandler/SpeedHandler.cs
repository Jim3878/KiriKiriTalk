using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public class SpeedHandler : ISpeedHandler
    {
        private readonly float defaulDelay = 0.05f;
        private float _delay = 0.5f;
        public float delay
        {
            get
            {
                return _delay;
            }
            set
            {
                _delay = value;
            }
        }
        public float speed
        {
            get
            {
                if (_delay == 0)
                {
                    return -1;
                }
                return 1 / speed;
            }

            set
            {
                if (speed == 0)
                {
                    _delay = defaulDelay;
                }
                else
                {
                    _delay = 1 / value;
                }
            }
        }
    }
}