using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System.Collections;
using System;


public class LSMKiriTest {

    [UnityTest]
    public IEnumerator KiriTalk_TransToSkip_WillShowDialogPerFrame()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject go = new GameObject();
        go.AddComponent<KiriTalk>();
        var talk = go.GetComponent<KiriTalk>();
        talk.ForTest();
        var compiler = new DialogCompiler();
        string dialog = "asdfgh";
        var dialogUnits = compiler.Build(dialog);

        talk.LaunchKiriKiri(dialogUnits);
        talk.TransTo(new SkipState(talk));
        //talk.TransTo()
       
        yield return null;
        Assert.AreEqual(1, talk.currentDialogText.Length);
        yield return null;
        Assert.AreEqual(2, talk.currentDialogText.Length);
        yield return null;
        Assert.AreEqual(3, talk.currentDialogText.Length);
        yield return null;
        Assert.AreEqual(4, talk.currentDialogText.Length);
    }

    [UnityTest]
    public IEnumerator Kiritalk_TransToPauseOnSkip_WillTrunToPause()
    {
        GameObject go = new GameObject();
        go.AddComponent<KiriTalk>();
        var talk = go.GetComponent<KiriTalk>();
        talk.ForTest();
        var compiler = new DialogCompiler(new PauseUnitFactory());
        string dialog = "asdf[pause]gh";
        var dialogUnits = compiler.Build(dialog);

        talk.LaunchKiriKiri(dialogUnits);
        talk.TransTo(new SkipState(talk));
        //talk.TransTo()

        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        Assert.AreEqual(4, talk.currentDialogText.Length);
    }

    [UnityTest]
    public IEnumerator PauseUnit_CanPause()
    {
        DialogCompiler compiler = new DialogCompiler(new PauseUnitFactory());
        var go = new GameObject();
        go.AddComponent<KiriTalk>();
        KiriTalk talk = go.GetComponent<KiriTalk>();
        string dialog = "[pause]dasfaf";
        talk.ForTest();

        talk.LaunchKiriKiri(compiler.Build(dialog));
        yield return new WaitForSeconds(3);

        try
        {
            Assert.AreEqual(true, talk.isPause);
        }
        catch (Exception)
        {
            talk.TraceLog();
        }
    }

    [UnityTest]
    public IEnumerator KiriTalk_CanShowDialog()
    {
        //Arrange

        DialogCompiler compiler = new DialogCompiler(new PauseUnitFactory());
        var go = new GameObject();
        go.AddComponent<KiriTalk>();
        string dialog = "QSS%^^%^";
        var dialogUnitList = compiler.Build(dialog);
        var talk = go.GetComponent<KiriTalk>();
        talk.delayTime = 0.1f;
        talk.ForTest();

        //Act
        talk.LaunchKiriKiri(dialogUnitList);
        yield return new WaitForSeconds(2);

        //assert
        try
        {
            Assert.AreEqual(dialog, talk.currentDialogText);
        }
        catch (Exception)
        {
            talk.TraceLog();
            throw;
        }

    }
}
