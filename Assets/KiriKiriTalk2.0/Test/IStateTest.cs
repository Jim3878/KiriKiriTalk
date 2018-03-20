using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class IStateTest
{
    static float typeSpeed = 10;
    static float typeDelay
    {
        get
        {
            return 1 / typeSpeed;
        }
    }
    public class NormalStateTest
    {
        [UnityTest]
        public IEnumerator StateUpdate_ChangeSpeedWhenUpdate_ChangeSpeedImmdieatly()
        {
            int count = 20;
            var writer = GetDummyTypewriter() as DummyTypewriter;
            writer.typeSpeed = 10;
            AddDelayableDialogUnit(count, writer);
            var state = GetNormalState(writer);
            state.StateBegin();
            float time = Time.time;

            yield return new WaitUntil(() =>
            {
                state.StateUpdate();
                return Time.time > time + 1;
            });
            writer.typeSpeed = 20;
            time = Time.time;
            yield return new WaitUntil(() =>
            {
                state.StateUpdate();
                return Time.time > time + 0.5f;
            });

            Assert.AreEqual(0, writer.unreadDialogUnitManager.Count);
        }

        [UnityTest]
        public IEnumerator StateUpdate_AfterTime_ExcuteAllDialogUnit()
        {
            typeSpeed = 20;
            yield return ExcuteAllDialogUnitThread(10);
            yield return ExcuteAllDialogUnitThread(20);
            yield return ExcuteAllDialogUnitThread(25);
        }

        IEnumerator ExcuteAllDialogUnitThread(int count)
        {
            var writer = GetDummyTypewriter();
            AddDelayableDialogUnit(count, writer);
            Assert.AreEqual(count, writer.unreadDialogUnitManager.Count);
            var state = GetNormalState(writer);
            state.StateBegin();
            float time = Time.time;

            yield return new WaitUntil(() =>
            {
                state.StateUpdate();
                return Time.time >= time + count / typeSpeed;
            });

            Assert.AreEqual(0, writer.unreadDialogUnitManager.Count);
        }



        [UnityTest]
        public IEnumerator StateUpdate_AfterTerminate_ExcuteNoDialogUnit()
        {
            var writer = GetDummyTypewriter();
            writer.Terminate();
            AddDelayableDialogUnit(10, writer);
            var state = GetNormalState(writer);
            state.StateBegin();
            float time = Time.time;

            yield return new WaitUntil(() =>
            {
                state.StateUpdate();
                return Time.time >= time + 1;
            });

            Assert.AreEqual(10, writer.unreadDialogUnitManager.Count);

        }

        [UnityTest]
        public IEnumerator StateUpdate_InputDelayAndBaseUnit_OnlyDelayInDelayUnit()
        {
            var writer = GetDummyTypewriter();
            writer.typeSpeed = 5;
            int typeCount = 10;
            AddDelayableDialogUnit(typeCount, writer);
            AddDialogUnit(5, writer);
            var state = GetNormalState(writer);
            state.StateBegin();
            float time = Time.time;

            yield return new WaitUntil(() =>
            {
                state.StateUpdate();
                return Time.time >= time + typeCount / writer.typeSpeed;
            });
            state.StateUpdate();

            Assert.AreEqual(0, writer.unreadDialogUnitManager.Count);


        }

    }

    public class SkipStateTest
    {
        [UnityTest]
        public IEnumerator Update_EveryFrame_PerframExcuteOneDialogUnit()
        {
            var dummy = GetDummyTypewriter();
            int count = 10;
            AddDelayableDialogUnit(count, dummy);
            var skip = new SkipState(dummy);
            skip.StateBegin();

            for (int i = 0; i < 10; i++)
            {
                skip.StateUpdate();
                yield return null;

                Assert.AreEqual(count - 1 - i, dummy.unreadDialogUnitManager.Count);
            }
        }

        [UnityTest]
        public IEnumerator Update_AfterTermitateEveryFrame_ExcuteNoDialogUnit()
        {
            var dummy = GetDummyTypewriter();
            int count = 10;
            dummy.Terminate();
            AddDelayableDialogUnit(count, dummy);
            var skip = new SkipState(dummy);
            skip.StateBegin();

            for (int i = 0; i < 10; i++)
            {
                skip.StateUpdate();
                yield return null;

                Assert.AreEqual(10, dummy.unreadDialogUnitManager.Count);
            }
        }

        [Test]
        public void Begin_IfRunningManagerIsNotEmpty_ClearRunningManager()
        {
            var writer = GetDummyTypewriter();
            int count = 10;
            writer.Terminate();
            DummyDialogUnit[] dummyArr;
            AddRunningDialogUnit(count, writer, out dummyArr);
            var skip = new SkipState(writer);

            skip.StateBegin();

            Assert.AreEqual(0, writer.runningDialogUnitManager.count);
        }
    }

    public class PauseStateTest
    {

    }

    public static ITypewriter GetDummyTypewriter()
    {
        var dummy = new DummyTypewriter();
        dummy.typeSpeed = typeSpeed;

        return dummy;
    }

    public static void AddDelayableDialogUnit(int count, ITypewriter writer)
    {
        for (int i = 0; i < count; i++)
        {
            writer.unreadDialogUnitManager.PushDialogs(new DummyDelayableUnit(i));
        }
    }

    public static void AddDialogUnit(int count, ITypewriter writer)
    {
        for (int i = 0; i < count; i++)
        {
            writer.unreadDialogUnitManager.PushDialogs(new DummyDialogUnit(i));
        }
    }

    public static void AddRunningDialogUnit(int count, ITypewriter writer, out DummyDialogUnit[] dummyArr)
    {
        dummyArr = new DummyDialogUnit[count];
        for (int i = 0; i < count; i++)
        {
            var dummy = new DummyDialogUnit(i);
            dummyArr[i] = dummy;
            dummy.Excute(writer);
            writer.runningDialogUnitManager.AddDialogUnit(dummy);
        }

    }

    public static NormalState GetNormalState(ITypewriter typewriter)
    {
        var n = new NormalState(typewriter);
        return n;
    }
}