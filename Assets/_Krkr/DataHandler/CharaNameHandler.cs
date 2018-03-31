using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Krkr
{
    public class CharaNameHandler : ICharaNameHandler
    {
        private Text nameText;
        GameObject nameBox;
        public CharaNameHandler(GameObject nameBox, Text nameText)
        {
            this.nameBox = nameBox;
            this.nameBox.SetActive(false);
            this.nameText = nameText;
            this.nameText.text = "";
        }

        public void SetName(string name)
        {
            nameBox.SetActive(name != "");
            nameText.text = name;
        }
    }
}