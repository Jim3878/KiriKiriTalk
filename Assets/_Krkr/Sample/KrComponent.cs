using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Krkr;
public class KrComponent : MonoBehaviour {

    public Text dialogBubble;
    KrLoader krloader;
    private void Start()
    {
        krloader = new KrLoader(dialogBubble);
        krloader.AddDialogStr("meta-programming，[size=50]有人翻做”超程式”還什麼的，不果不管翻什麼，[size=20]要真的表達清楚是很不容易，這是在C++ template相關的書上看到的，meta-programming的意思就是");
        krloader.Start();
    }

    private void Update()
    {
        krloader.Update();
    }

}
