using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    [Serializable]
    public class CharaData
    {
        public string name;
        public List<Sprite> Image;
    }

    public class CharaImageHandler : MonoBehaviour,ICharaImageHandler
    {
        public List<CharaData> CharaList;

        public void AddChara(string name, int type, string located)
        {
            throw new NotImplementedException();
        }

        public void MoveChara(string name, string located, int layer = 1)
        {
            throw new NotImplementedException();
        }

        public void Remove(string name)
        {
            throw new NotImplementedException();
        }

        public void SetCharaType(string name, int type)
        {
            throw new NotImplementedException();
        }
    }
}