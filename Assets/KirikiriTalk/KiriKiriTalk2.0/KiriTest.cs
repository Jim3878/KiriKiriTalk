using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class KiriTest
{

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator PausePassesPasses()
    {
        DialogCompiler compiler = new DialogCompiler(new PauseUnitFactory());
        var go = new GameObject();
        go.AddComponent<KiriKiriTalk>();
        KiriKiriTalk kiri = go.GetComponent<KiriKiriTalk>();
        string dialog = "[pause]dasfaf";

        kiri.LaunchKiriKiri(compiler.Build(dialog));

        yield return new WaitForSeconds(3);

        Assert.AreEqual(true, kiri.isPause);


        // Use the Assert class to test conditions.
        // yield to skip a frame

    }

    [UnityTest]
    public IEnumerator DefaultDialogUnitPasses()
    {
        //Arrange

        DialogCompiler compiler = new DialogCompiler(new PauseUnitFactory());
        var go = new GameObject();
        go.AddComponent<DummyKiriTalk>();

        //Act
        string dialog = "QSS%^^%^";
        var dialogUnitList = compiler.Build(dialog);
        var dummyTalk = go.GetComponent<DummyKiriTalk>();
        dummyTalk.delayTime = 0.2f;
        //assert
        Assert.AreEqual(dialog.Length, dialogUnitList.Length);
        for (int i = 0; i < dialog.Length; i++)
        {
            dialogUnitList[i].Excute(dummyTalk);
        }
        yield return new WaitForSeconds(2);
        Assert.AreEqual(dialog, dummyTalk.prevDialog);
    }


    class DummyKiriTalk : KiriKiriTalk
    {
        public string prevDialog = "";
        public override void AddDialog(string s)
        {
            prevDialog += s;
        }
    }
}
