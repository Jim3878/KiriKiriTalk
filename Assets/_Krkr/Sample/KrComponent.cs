using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Krkr;
public class KrComponent : MonoBehaviour
{
    public GameObject waitSymbol;
    public Text dialogBubble;
    public GameObject nameBox;
    public Text nameText;
    KrLoader krloader;
    IWaitHandler waitHandler;
    ICharaNameHandler nameHandler;

    private void Start()
    {
        nameHandler = new CharaNameHandler(nameBox, nameText);
         waitHandler = new WaitHandler(waitSymbol);
        var styleHandler = new Krkr.DialogStyleHandler();
        krloader = new KrLoader(dialogBubble);

        krloader.SetProperty(styleHandler);
        krloader.AddComdFactory(
            new SetSizeFactory(styleHandler),
           new WaitClickFactory(waitHandler),
           new SetNameFactory(nameHandler));

        krloader.AddDialogStr("[name=\"老鬼\"]meta-programming，[size=50]有人翻做”超程式”還什麼的，不[waitclick]果不管翻什麼，[size=20]要真[name=\"重慶\"]的表達清楚是很不容易，這是在C++ template相關的書上看到的，meta-programming的[name=\"\"]意思就是[waitclick]");
        krloader.Start();
    }

    private void Update()
    {
        krloader.Update();
        if(waitHandler.isWait && Input.GetMouseButtonUp(0))
        {
            waitHandler.Resume();
        }
    }

}
