using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using DG.Tweening;
using System;

public class DialogUnitTest
{

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [Test]
    public void PauseUnit_Excute_WillPause()
    {

    }

    [UnityTest]
    public IEnumerator Tweener_PlayAndCompleteOnSameFrame_AreCompletable()
    {
        yield return null;
        GameObject go = new GameObject();
        go.transform.position = new Vector3(0, 0, 0);

        Tween goMove = go.transform.DOMoveX(10, 5);
        goMove.Complete();

        Assert.AreEqual(10, go.transform.position.x);

    }

   

    class DummyKiriTalk : KiriTalk
    {
        public string prevDialog = "";
        public override void AddDialog(string s)
        {
            prevDialog += s;
        }
    }
}
