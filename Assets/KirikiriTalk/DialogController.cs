using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KirikiriTalk.Parser;
namespace KirikiriTalk
{
    public class DialogController
    {
        public string text;

        int _nextIndex;
        string _currentChar;
        public string currentChar
        {
            get
            {
                if (isFunction)
                {
                    return null;
                }
                return _currentChar;
            }
        }

        bool _isFunction;
        public bool isFunction
        {
            get
            {
                return _isFunction;
            }
        }

        DialogUnit _currentDialogUnit;

        public DialogUnit currentDialogUnit
        {
            get
            {
                if (!_isFunction)
                {
                    return null;
                }
                return _currentDialogUnit;
            }
        }

        public DialogController(string text)
        {
            this.text = text;
            _nextIndex = 0;
        }

        public void Reset()
        {
            _nextIndex = 0;
        }

        /// <summary>
        /// 若字串完結回傳false
        /// </summary>
        /// <returns></returns>
        public bool TryToNext()
        {
            if (_nextIndex >= text.Length)
            {
                return false;
            }
            //判斷是否為指令
            if (text[_nextIndex] != '[')
            {
                _currentChar = text[_nextIndex].ToString();
                _nextIndex++;
                _isFunction = false;
            }
            else if (text[_nextIndex + 1] == '[')
            {//判斷指令是否為表示字元'['
                _nextIndex += 2;
                _currentChar = '['.ToString();
                _isFunction = false;
            }
            else
            {//找出指令長度

                int length = 1;
                while (!text[_nextIndex + length].Equals(']')
                    && length <= 1000
                    && _nextIndex + length < text.Length)
                {
                    length++;
                }
                if (length > 1000 || _nextIndex + length >= text.Length)
                {
                    Debug.Log("\\後指令後找不到空白鍵中斷點");
                    return false;
                }
                else
                {
                    DialogUnit breakSymbol = new DialogUnit(text.Substring(_nextIndex, length + 1), text.Substring(_nextIndex + 1, length - 1), ",", "=");

                    _nextIndex += length + 1;
                    _currentDialogUnit = breakSymbol;
                    _currentDialogUnit.SpilitBreakSymbol(",","=");
                    _isFunction = true;
                }
            }
            return true;

        }
    }

}