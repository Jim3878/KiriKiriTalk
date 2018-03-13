using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KirikiriTalk
{
    public class MouseClickEvnetArgs : EventArgs
    {
        public int button;
        public MouseClickEvnetArgs(int button)
        {
            this.button = button;
        }
    }

    public class InputInvoker : MonoBehaviour
    {
        #region variable
        public delegate void InputEvent(clickTypeEnum clickType, KeyCode key);
        public enum clickTypeEnum
        {
            UP,
            DOWN,
            CLICK
        }
        public static string prefabsPath = "InputInvoker";
        public static InputInvoker _instance;
        public static InputInvoker instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(prefabsPath));
                    DontDestroyOnLoad(obj);
                    _instance = obj.GetComponent<InputInvoker>();
                }
                return _instance;
            }
        }

        #endregion


        public event InputEvent mouseEvent;
        public event InputEvent keyEvent;

        private void Update()
        {
            KeyCheck();
            MouseCheck();
        }

        void MouseCheck()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Input.GetMouseButtonDown(i))
                {
                    if (mouseEvent != null)
                    {
                        mouseEvent(clickTypeEnum.DOWN, (KeyCode)323 + i);
                    }
                }
                if (Input.GetMouseButtonUp(i))
                {
                    if (mouseEvent != null)
                    {
                        mouseEvent(clickTypeEnum.UP, (KeyCode)323 + i);
                    }
                }
                if (Input.GetMouseButton(i))
                {
                    if (mouseEvent != null)
                    {
                        mouseEvent(clickTypeEnum.CLICK, (KeyCode)323 + i);
                    }
                }
            }
        }

        void KeyCheck()
        {
            if (keyEvent != null)
            {
                foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                {
                    if (key != KeyCode.Mouse0 && key != KeyCode.Mouse1 && key != KeyCode.Mouse2)
                    {
                        if (Input.anyKey)
                        {
                            if (Input.GetKey(key))
                            {
                                keyEvent(clickTypeEnum.CLICK, key);
                            }
                            if (Input.GetKeyDown(key))
                            {
                                keyEvent(clickTypeEnum.DOWN, key);
                            }
                        }
                        if (Input.GetKeyUp(key))
                        {
                            keyEvent(clickTypeEnum.UP, key);
                        }
                    }
                }
            }
        }
    }
}

