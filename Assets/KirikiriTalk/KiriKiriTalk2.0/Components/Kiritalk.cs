using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Kiritalk : MonoBehaviour
{

    public Text dialog;
    public Transform charaParent;
    public Transform dialogBubble;
    public GameObject ImagePrefabs;
    public List<Transform> charaImagePositions;

    Typewriter writer;
    DialogCompiler compiler;
    bool isInit = false;
    void Init()
    {
        if (!isInit)
        {
            isInit = true;
            writer = new Typewriter();
            writer.onComplete += this.OnComplete;
            compiler = new DialogCompiler(new CharOutputFactory(dialog), new FactoryInputCompiler());
            compiler.AddDialogUnitFactory(
                new SizeUnitFactory(),
                new ColorUnitFactory(),
                new PauseUnitFactory(),
                new WaitUnitFactory(),
                new ShowUnitFactory(dialogBubble),
                new CloseUnitFactory(dialogBubble),
                new AddCharaUnitFactory(new CharaImageManager(), ImagePrefabs, charaParent, charaImagePositions.ToArray())
                );
        }
    }

    private void Start()
    {

        Init();
        AddDialog(@"[hides][wait = 2.5f][setchara,name=""ユーカリ"",file=""Image/CharacterDialog/chara_pic_1_3"",from = ""LeftLeft"", to = ""Left"", duration = 0.5f][wait = 0.2f][show]userNameさん。このあと、
少しお話しませんか〜？[waitclick]");
        writer.Start();
    }

    public void AddDialog(string dialog)
    {
        Init();
        writer.AddDialogUnitList(compiler.Build(dialog));
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (writer.stateController.CurrentState is PauseState)
            {
                writer.stateController.TransTo(new NormalState(writer));
            }
            else if (writer.stateController.CurrentState is NormalState)
            {
                writer.stateController.TransTo(new SkipState(writer));
            }
        }
        writer.Update();
    }
    private void OnComplete(object sender, EventArgs e)
    {
        for (int i = 0; i < this.charaParent.childCount; i++)
        {
            var t = this.charaParent.GetChild(i);
            //t.DOComplete();
            t.DOMove(charaImagePositions[0].position, 0.5f);
        }
        //dialogBubble.DOComplete();
        //dialogBubble.DOKill(true);
        dialogBubble.DOScaleX(0.0001f, 0.2f).SetEase(Ease.Linear).OnComplete(()=> {
            dialogBubble.gameObject.SetActive(false);
        });
        writer.Terminate();
    }
}
