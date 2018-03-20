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
    public class BaseDialogUnitTest
    {
        [Test]
        public void Excute_WontAddToRunningDialogUnit()
        {
            var writer = IStateTest.GetDummyTypewriter();
            var immDialogUnit0 = new DummyImmediateDialogUnit(0);
            var immDialogUnit1 = new DummyImmediateDialogUnit(1);
            var immDialogUnit2 = new DummyImmediateDialogUnit(2);

            immDialogUnit0.Excute(writer);
            immDialogUnit1.Excute(writer);
            immDialogUnit2.Excute(writer);

            Assert.AreEqual(0, writer.runningDialogUnitManager.count);
        }
    }

    public class BaseCompletableDialogUnitTest
    {
        [Test]
        public void Excute_WillAutoAddToRunningDialogUnit()
        {
            var writer = IStateTest.GetDummyTypewriter();
            var immDialogUnit0 = new DummyRunningDialogUnit(0);
            var immDialogUnit1 = new DummyRunningDialogUnit(1);
            var immDialogUnit2 = new DummyRunningDialogUnit(2);

            immDialogUnit0.Excute(writer);
            immDialogUnit1.Excute(writer);
            immDialogUnit2.Excute(writer);

            Assert.AreEqual(3, writer.runningDialogUnitManager.count);
        }

        [Test]
        public void Complete_WillAutoDeleteFromRunningDialogUnit()
        {
            var writer = IStateTest.GetDummyTypewriter();
            var immDialogUnit0 = new DummyRunningDialogUnit(0);
            var immDialogUnit1 = new DummyRunningDialogUnit(1);
            var immDialogUnit2 = new DummyRunningDialogUnit(2);

            immDialogUnit0.Excute(writer);
            immDialogUnit1.Excute(writer);
            immDialogUnit2.Excute(writer);
            immDialogUnit0.Complete(writer);
            immDialogUnit1.Complete(writer);

            Assert.AreEqual(2, writer.runningDialogUnitManager.GetTopDialogUnit().ID);
            Assert.AreEqual(1, writer.runningDialogUnitManager.count);
        }

    }

}
