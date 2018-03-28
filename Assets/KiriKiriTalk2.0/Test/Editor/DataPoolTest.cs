using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using Krkr;

public class DataPoolTest
{
    public class RunningDialogManagerTest
    {
        [Test]
        public void RemoveDialog_OnDialognDoesntExist_DoNothing()
        {
            var manager = GetManager();

            manager.RemoveDialogUnit(0);
            manager.RemoveDialogUnit(1);
            manager.RemoveDialogUnit(5);
        }

        [Test]
        public void AddDialog_OnNullAndSameID_Throw()
        {
            var manager = GetManager();

            manager.AddDialogUnit(new DummyDialogUnit(0));

            Assert.Throws<ArgumentNullException>(() => manager.AddDialogUnit(null));
            Assert.Throws<ArgumentException>(() => manager.AddDialogUnit(new DummyDialogUnit(0)));
        }

        [Test]
        public void Count_AddAndRemove_CurrectCount()
        {
            var manager = GetManager();

            manager.AddDialogUnit(new DummyDialogUnit(0));
            manager.AddDialogUnit(new DummyDialogUnit(1));
            manager.AddDialogUnit(new DummyDialogUnit(2));
            manager.RemoveDialogUnit(1);
            manager.RemoveDialogUnit(5);

            Assert.AreEqual(2, manager.GetAllDialogUnit().Length);
        }

        [Test]
        public void GetDialogByID_AfterAddDialog_ReturnCurrectDialog()
        {
            var manager = GetManager();
            int id = 0;
            var dummy = new DummyDialogUnit(id);

            manager.AddDialogUnit(dummy);

            Assert.AreEqual(dummy, manager.GetDialogUnit(id));
        }

        [Test]
        public void GetDialogByID_InputWrongID_Throws()
        {
            var manager = GetManager();
            int id = 0;
            var dummy = new DummyDialogUnit(id);

            manager.AddDialogUnit(dummy);
           
            Assert.Throws<KeyNotFoundException>(() => manager.GetDialogUnit(5));
        }

        [Test]
        public void IsEmpty_AfterAddAndRemove_ReturnTrue()
        {
            var manager = GetManager();

            manager.AddDialogUnit(new DummyDialogUnit(0));
            manager.AddDialogUnit(new DummyDialogUnit(1));
            manager.AddDialogUnit(new DummyDialogUnit(2));
            manager.RemoveDialogUnit(1);
            manager.RemoveDialogUnit(2);
            manager.RemoveDialogUnit(0);

            Assert.IsTrue(manager.IsEmpty);
        }
        
        [Test]
        public void GetTopDialog_AfterAddDIalog_ReturnCurrectDialog()
        {
            var manager = GetManager();
            var dummy1 = new DummyDialogUnit(1);
            var dummy2 = new DummyDialogUnit(2);
            var dummy3 = new DummyDialogUnit(3);

            manager.AddDialogUnit(dummy1);
            manager.AddDialogUnit(dummy2);
            manager.AddDialogUnit(dummy3);
            
            Assert.AreEqual(dummy3, manager.GetTopDialogUnit());
        }
        
        IRunningDialogUnitManager GetManager()
        {
            return new RunningDialogUnitManager();
        }
    }

    public class TextStyleManagerTest
    {

        public class TextStyelTest
        {
            [Test]
            public void TextStyle_Build_CanGetCorrectHeader()
            {
                TextStyle style = new TextStyle("AAA");

                string header = style.header;

                StringAssert.IsMatch("AAA", header);
            }
            [Test]
            public void TextStyle_BuildWithOutValue_CanGetCorrectRightLeftCase()
            {
                TextStyle style = new TextStyle("AAA");

                string left = style.leftStyle;
                string right = style.rightStyle;

                StringAssert.IsMatch("<AAA>", left);
                StringAssert.IsMatch("</AAA>", right);
            }
            [Test]
            public void TextStyle_BuildWithValue_CanGetCorrectRightLeftCase()
            {
                TextStyle style = new TextStyle("AAA", "ccc");

                string left = style.leftStyle;
                string right = style.rightStyle;

                StringAssert.IsMatch("<AAA=ccc>", left);
                StringAssert.IsMatch("</AAA>", right);
            }
        }

        [Test]
        public void count_AfterAddStyle_CountWillIncreass()
        {
            IDialogStyleController manager = new DialogStyleController();
            string header1 = "ABCDD";
            string header2 = "KKKKK";
            string value2 = "5555";
            TextStyle style1 = new TextStyle(header1);
            TextStyle style2 = new TextStyle(header2, value2);

            manager.AddStyle(style1);
            manager.AddStyle(style2);

            Assert.AreEqual(2, manager.count);
        }

        [Test]
        public void count_AfterAddDuplicatedStyle_CountWontIncreass()
        {
            IDialogStyleController manager = new DialogStyleController();
            string header1 = "ABCDD";
            string header2 = "KKKKK";
            string value2 = "5555";
            TextStyle style1 = new TextStyle(header1);
            TextStyle style2 = new TextStyle(header2, value2);
            TextStyle style3 = new TextStyle(header1, value2);

            manager.AddStyle(style1);
            manager.AddStyle(style2);
            manager.AddStyle(style3);

            Assert.AreEqual(2, manager.count);
        }

        [Test]
        public void GetStyle_InputHeader_CanFoundTextStyle()
        {
            IDialogStyleController manager = new DialogStyleController();
            string header1 = "ABCDD";
            string header2 = "KKKKK";
            string value2 = "5555";
            TextStyle style1 = new TextStyle(header1);
            TextStyle style2 = new TextStyle(header2, value2);

            manager.AddStyle(style1);
            manager.AddStyle(style2);

            Assert.IsTrue(manager.HasStyle(header1));
            Assert.IsTrue(manager.HasStyle(header2));
            Assert.IsFalse(manager.HasStyle("$%^&*"));
            Assert.AreEqual(style1, manager.GetStyle(header1));
            Assert.AreEqual(style2, manager.GetStyle(header2));
            Assert.Throws<KeyNotFoundException>(() => manager.GetStyle("&*()))"));
        }

        [Test]
        public void AddStyle_InputSameHeaderSytle_OldStyleWillBeOverwrite()
        {
            IDialogStyleController manager = new DialogStyleController();
            string header1 = "ABCDD";
            string header2 = "KKKKK";
            string value2 = "5555";
            TextStyle style1 = new TextStyle(header1);
            TextStyle style2 = new TextStyle(header2, value2);
            TextStyle style3 = new TextStyle(header1, value2);
            TextStyle style4 = new TextStyle(header2);

            manager.AddStyle(style1);
            manager.AddStyle(style2);
            manager.AddStyle(style3);
            manager.AddStyle(style4);

            Assert.IsTrue(manager.HasStyle(header1));
            Assert.IsTrue(manager.HasStyle(header2));
            Assert.IsFalse(manager.HasStyle("$%^&*"));
            Assert.AreEqual(style3, manager.GetStyle(header1));
            Assert.AreEqual(style4, manager.GetStyle(header2));
            Assert.Throws<KeyNotFoundException>(() => manager.GetStyle("&*()))"));
        }

        [Test]
        public void GetRightLeftStyle_RightStyleAndLeftStyleWillSortingReversed()
        {
            IDialogStyleController manager = new DialogStyleController();
            string header1 = "ABCDD";
            string header2 = "KKKKK";
            string value2 = "5555";
            TextStyle style1 = new TextStyle(header1);
            TextStyle style2 = new TextStyle(header2, value2);
            manager.AddStyle(style1);
            manager.AddStyle(style2);

            List<string> left = new List<string>();
            for (int i = 0; i < manager.count; i++)
            {
                left.Add(manager.GetLeftStyle(i));
            }
            List<string> right = new List<string>();
            for (int i = 0; i < manager.count; i++)
            {
                right.Add(manager.GetRightStyle(i));
            }

            Assert.AreEqual(left.Count, right.Count);
            for (int i = 0; i < manager.count; i++)
            {
                StringAssert.Contains(right[manager.count - i - 1].Replace("/", "").Replace(">", ""), left[i].Replace("/", ""));
            }
        }
        //public void GetLeft
    }

    public class UnreadDialogManagerTest
    {
        [Test]
        public void Clear_CountEqualsZero()
        {
            var manager = GetManager();
            manager.PushDialogs(new DummyDialogUnit(0), new DummyDialogUnit(1), new DummyDialogUnit(2));

            manager.Clear();

            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void PopDIalogUnit_OnEmpty_Throws()
        {
            var manager = GetManager();
            manager.PushDialogs(new DummyDialogUnit(0), new DummyDialogUnit(1), new DummyDialogUnit(2));

            Assert.AreEqual(0, manager.PopDialogUnit().ID);
            Assert.AreEqual(2, manager.Count);
            Assert.AreEqual(1, manager.PopDialogUnit().ID);
            Assert.AreEqual(1, manager.Count);
            Assert.AreEqual(2, manager.PopDialogUnit().ID);
            Assert.AreEqual(0, manager.Count);
            Assert.Throws<IndexOutOfRangeException>(() => manager.PopDialogUnit());
            Assert.AreEqual(0, manager.Count);

        }

        [Test]
        public void PopDialogUnit_AfterPushDialogUnit_WillGetFirstPushDialogUnit()
        {
            var manager = GetManager();
            manager.PushDialogs(new DummyDialogUnit(0), new DummyDialogUnit(1), new DummyDialogUnit(2));

            Assert.AreEqual(0, manager.PopDialogUnit().ID);
            Assert.AreEqual(2, manager.Count);
            Assert.AreEqual(1, manager.PopDialogUnit().ID);
            Assert.AreEqual(1, manager.Count);
            Assert.AreEqual(2, manager.PopDialogUnit().ID);
            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void PeekDialogUnit_PeekAndPeek_BothOutValueAndCountWontCahnge()
        {
            var manager = GetManager();
            manager.PushDialogs(new DummyDialogUnit(0), new DummyDialogUnit(1), new DummyDialogUnit(2));

            Assert.AreEqual(0, manager.PeekDialogUnit().ID);
            Assert.AreEqual(3, manager.Count);
            Assert.AreEqual(0, manager.PeekDialogUnit().ID);
            Assert.AreEqual(3, manager.Count);
            Assert.AreEqual(0, manager.PeekDialogUnit().ID);
            Assert.AreEqual(3, manager.Count);

        }

        public IUnreadDialogUnitManager GetManager()
        {
            return new UnreadDialogUnitManager();
        }

    }

}
