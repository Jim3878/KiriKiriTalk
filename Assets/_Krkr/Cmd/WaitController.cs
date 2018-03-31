using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr {
    public class WaitHandler : IWaitHandler
    {
        GameObject waitSymbol;
        public WaitHandler(GameObject waitSymbol)
        {
            waitSymbol.SetActive(false);
            this.waitSymbol = waitSymbol;
        }
        private bool _isWait;
        public bool isWait
        {
            get
            {
                return _isWait;
            }
        }

        public event EventHandler onResume;
        public event EventHandler onWait;

        public void Resume()
        {
            waitSymbol.SetActive(false);
            _isWait = false;
            if (onResume != null)
                onResume(this, new EventArgs());
        }

        public void Wait()
        {
            _isWait = true;
            waitSymbol.SetActive(true);
            if (onWait != null)
                onWait(this, new EventArgs());
        }
    }
}