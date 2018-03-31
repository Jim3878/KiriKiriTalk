using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Krkr
{
    public interface ICharaImageHandler
    {

        void AddChara(string name, int type, string located);

        void MoveChara(string name, string located, int layer = 1);

        void SetCharaType(string name, int type);

        void Remove(string name);
    }
}