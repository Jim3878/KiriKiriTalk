using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yeh
{
    public class MouseClickEvnetArgs : EventArgs
    {
        public int button;
        public MouseClickEvnetArgs(int button)
        {
            this.button = button;
        }
    }

    public class InputEventComp : MonoBehaviour
    {

        public EventHandler<MouseClickEvnetArgs> onMousuDown;
        public EventHandler<MouseClickEvnetArgs> onMousuUp;


        private void Update()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Input.GetMouseButtonDown(i))
                {
                    onMousuDown(this, new MouseClickEvnetArgs(i));
                }
                if (Input.GetMouseButtonUp(i))
                {
                    onMousuUp(this, new MouseClickEvnetArgs(i));
                }
            }
        }

    }

    public static class InputEvent
    {
        static InputEventComp comp;
        static string prefabsPath = "InputEvent";
        public static EventHandler<MouseClickEvnetArgs> onMouseDown;
        public static EventHandler<MouseClickEvnetArgs> onMouseUp;

        static InputEvent()
        {
            comp = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(prefabsPath)).GetComponent<InputEventComp>();

            comp.onMousuDown += OnMouseDown;
            comp.onMousuUp += OnMouseUp;
            GameObject.DontDestroyOnLoad(comp.gameObject);
        }

        static void OnMouseDown(object sender, MouseClickEvnetArgs e)
        {
            if (onMouseDown != null)
            {
                onMouseDown(sender, e);
            }
        }

        static void OnMouseUp(object sender, MouseClickEvnetArgs e)
        {
            if (onMouseUp != null)
            {
                onMouseUp(sender, e);
            }
        }
    }

}