using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharaImageManager
{
    bool ContainsChara(string name);
    void AddChara(string name,Transform transform);
    Transform GetChara(string name);
}
