using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using KiriUtility;
using System.Collections.Generic;

public class UtilityTest
{
    [Test]
    public void StringIsString_DectiveDoubleQuotation()
    {
        string s1 = @"""aaaa""";
        string s2 = @"aaaa";
        string s3 = @"""aaaa";
        string s4 = @"aaaa""";

        Assert.IsTrue(s1.IsString());
        Assert.IsFalse(s2.IsString());
        Assert.IsFalse(s3.IsString());
        Assert.IsFalse(s4.IsString());
    }

    [Test]
    public void StringToStringNormallize_InputWrogString_ThrowArgumentException()
    {
        string s2 = @"aaaa";
        string s3 = @"""aaaa";
        string s4 = @"aaaa""";

        Assert.Throws<ArgumentException>(() => s4.ToStringNormalize());
        Assert.Throws<ArgumentException>(() => s2.ToStringNormalize());
        Assert.Throws<ArgumentException>(() => s3.ToStringNormalize());
    }

    [Test]
    public void StringToStringNormallize_InputCurrectString_RemoveDoubleQuotation()
    {
        string s1 = @"""aaaa""";

        Assert.AreEqual("aaaa", s1.ToStringNormalize());
    }

    [Test]
    public void StringIsInt_CanDetectivIntString()
    {
        Assert.AreEqual(5, int.Parse("+5"));
        Assert.AreEqual(-5, int.Parse("-5"));
        Assert.AreEqual(false, "aa99a".IsInt());
        Assert.AreEqual(false, "aa99".IsInt());
        Assert.AreEqual(false, Utility.IsInt("a-99"));
        Assert.AreEqual(true, Utility.IsInt("99"));
        Assert.AreEqual(true, Utility.IsInt("+99"));
        Assert.AreEqual(true, Utility.IsInt("-0599"));
        Assert.AreEqual(false, Utility.IsInt("-0599.000"));
        Assert.AreEqual(false, Utility.IsInt("-0599.002"));
    }

    [Test]
    public void StringIsFloat_CanDetectivFloatString()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, Utility.IsFloat("aa99a"));
        Assert.AreEqual(false, Utility.IsFloat("aa99"));
        Assert.AreEqual(false, Utility.IsFloat("a-99f"));
        Assert.AreEqual(true, Utility.IsFloat("9"));
        Assert.AreEqual(true, Utility.IsFloat("99"));
        Assert.AreEqual(true, Utility.IsFloat("+99"));
        Assert.AreEqual(true, Utility.IsFloat("-0599"));
        Assert.AreEqual(true, Utility.IsFloat("-0599f"));
        Assert.AreEqual(true, Utility.IsFloat("-0599.000"));
        Assert.AreEqual(true, Utility.IsFloat("-0599.002"));
    }

    [Test]
    public void StringIsVector2_CanDetectivVector2String()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, Utility.IsVector2("aa99a"));
        Assert.AreEqual(false, Utility.IsVector2("(5,a)"));
        Assert.AreEqual(false, Utility.IsVector2("()"));
        Assert.AreEqual(false, Utility.IsVector2("(5.3,5.2,5)"));
        Assert.AreEqual(false, Utility.IsVector2("( 5,5)"));
        Assert.AreEqual(true, Utility.IsVector2("(5,5)"));
        Assert.AreEqual(true, Utility.IsVector2("(5.0,5.2)"));
    }

    [Test]
    public void StringIsVector3_CanDetectivVector3String()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, Utility.IsVector3("aa99a"));
        Assert.AreEqual(false, Utility.IsVector3("(5,a)"));
        Assert.AreEqual(false, Utility.IsVector3("()"));
        Assert.AreEqual(false, Utility.IsVector3("(5,5)"));
        Assert.AreEqual(false, Utility.IsVector3("(5.0,5.2)"));
        Assert.AreEqual(false, Utility.IsVector3("(5.0,5,5.2"));
        Assert.AreEqual(false, Utility.IsVector3("5.0,6.0,5.2)"));
        Assert.AreEqual(false, Utility.IsVector3("(5.3 ,5.2,5)"));
        Assert.AreEqual(true, Utility.IsVector3("(5.3,5.2,5)"));
        Assert.AreEqual(true, Utility.IsVector3("(5.0,5,5.2)"));
        Assert.AreEqual(true, Utility.IsVector3("(5.0,6.0,5.2)"));
    }

    [Test]
    public void ToVector2_CanParseStringToVector2()
    {
        Assert.That(() => Utility.ToVector2("(5,5,6)"), Throws.Exception);
        Assert.AreEqual(new Vector2(5, 5), Utility.ToVector2("(5,5)"));
        Assert.AreEqual(new Vector2(5.0f, 5.2f), Utility.ToVector2("(5.0,5.2)"));
    }

    [Test]
    public void ToVector3_CanParseStringToVector3()
    {
        Assert.That(() => Utility.ToVector3("(5,6)"), Throws.Exception);
        Assert.AreEqual(new Vector3(6, 5, 5), Utility.ToVector3("(6,5,5)"));
        Assert.AreEqual(new Vector3(8, 5.0f, 5.2f), Utility.ToVector3("(8,5.0,5.2)"));
    }

    [Test]
    public void SpilitToField_Spilit()
    {
        string[] s1 = { "dummy", "a=0", "b=5.6", "v2=(2,2)", "v3=(3,9,8)" };
        string s = string.Format("{0},{1},{2},{3},{4}", s1[0], s1[1], s1[2], s1[3], s1[4]);
        var field = s.SpiltToField();

        for (int i = 0; i < s1.Length; i++)
        {
            Assert.AreEqual(s1[i], field[i]);
        }
    }

    [Test]
    public void RemovBrackets_InputTextWithWrongBrackets_ThrowArgumentException()
    {
        string s2 = "eeeee]";
        string s3 = "[eeeee";
        int len;

        Assert.Throws<ArgumentException>(() => s3.RemoveBrackets(out len));
        Assert.Throws<ArgumentException>(() => s2.RemoveBrackets(out len));
    }

    [Test]
    public void RemovBrackets_InputTextWithSpace_RemoveSpace()
    {
        string s1 = "[ee  e]";
        string s2 = "[e e ee]";

        string r1 = s1.RemoveBrackets();
        string r2 = s2.RemoveBrackets();

        Assert.AreEqual(3, r1.Length);
        Assert.AreEqual(4, r2.Length);
    }

    [Test]
    public void RemovBrackets_InputTextWithBrackets_CalculateCurrectLength()
    {
        string s1 = "[eeeee]";
        string s2 = "[eee]";
        int len1;
        int len2;

        s1.RemoveBrackets(out len1);
        s2.RemoveBrackets(out len2);

        Assert.AreEqual(7, len1);
        Assert.AreEqual(5, len2);
    }

    [Test]
    public void RemovBrackets_InputTextWithBrackets_OutputCurrectString()
    {
        string s1 = "[eeeee]";
        string s2 = "[eee]";
        int len1;
        int len2;

        string o1 = s1.RemoveBrackets(out len1);
        string o2 = s2.RemoveBrackets(out len2);

        Assert.AreEqual("eeeee", o1);
        Assert.AreEqual("eee", o2);
    }

    [Test]
    public void FindRightBrackets_InputCharArr_OutputCurrectLength()
    {
        string s = "0123[567]";

        int index = s.ToCharArray().FindRightBrackets(4);

        Assert.AreEqual(8, index);
    }

    [Test]
    public void FindRightBrackets_InputWhitOutOneBrackets_ThrowArgumentException()
    {
        string s1 = "0123[567";
        string s2 = "01234567]";

        Assert.Throws<ArgumentException>(() => s1.ToCharArray().FindRightBrackets(4));
        Assert.Throws<ArgumentException>(() => s2.ToCharArray().FindRightBrackets(4));
    }

    [Test]
    public void IsColorHex_InputHex_ReturnTrue()
    {
        string hex = "#00ff00";
        Assert.IsTrue(hex.IsColorHex());
    }

    [Test]
    public void IsColorHex_InputRunHex_ReturnFalse()
    {
        string hex = "#e196g0";

        Assert.IsFalse(hex.IsColorHex());
    }

    [Test]
    public void IsColorHex_InputWithoutSherp_ReturnFalse()
    {
        string hex = "e196f0";

        Assert.IsFalse(hex.IsColorHex());
    }

    [Test]
    public void IsColorHex_InputToManyWord_ReturnFalse()
    {
        string hex = "#ae196f0";

        Assert.IsFalse(hex.IsColorHex());
    }

    public class UnitInputParserTest
    {
        [Test]
        public void ToUnitInput_InputMultiEquals_ThrowArgumetException()
        {
            var parser = new FactoryInputCompiler();
            string s1 = "pppp,a=b,a===b,a=====q";
            string s2 = "pppp,pdpp,ppp,p=pp";

            Assert.Throws<ArgumentException>(() => parser.ToUnitInput(s1));
            parser.ToUnitInput(s2);
        }

        [Test]
        public void ToUnitInput_SameKeyInTwoField_ThrowArgumetException()
        {
            var parser = new FactoryInputCompiler();
            string s1 = "pppp,ppp,ppp,p=pp";

            Assert.Throws<ArgumentException>(() => parser.ToUnitInput(s1));
        }

        [Test]
        public void ToUnitInput_InputNoEquals_OutCurrectValue()
        {
            var parser = new FactoryInputCompiler();
            string s1  = @"setchara,name=""ユーカリ"",file=""Image / CharacterDialog / chara_pic_1_3"",from = ""LeftLeft"",to = ""Left"",duration = 0.5f";

            //Assert.Throws<ArgumentException>(() => parser.ToUnitInput(s1));
            var o = parser.ToUnitInput(s1.Replace(" ",""));
            
            Assert.AreEqual(6, o.Count);
            Assert.IsTrue(o["setchara"] ==null);
            Assert.AreEqual("ユーカリ", o["name"].ToStringNormalize());
            Assert.AreEqual("Image/CharacterDialog/chara_pic_1_3",o["file"].ToStringNormalize());
            Assert.AreEqual("LeftLeft",o["from"].ToStringNormalize());
            Assert.AreEqual("Left",o["to"].ToStringNormalize());
            Assert.AreEqual("0.5f",o["duration"]);
        }
    }

    [Test]
    public void DelayableCatcherTest_CatchUntilDelayable()
    {
        DummyTypewriter typewriter = new DummyTypewriter();
        var d1 = new DummyDialogUnit(-1);
        var d2 = new DummyDialogUnit(-1);
        var d3 = new DummyDialogUnit(-1);
        var dd = new DummyDelayableUnit(2);
        var d5 = new DummyDialogUnit(-1);
        List<IDialogUnit> unitList = new List<IDialogUnit>();
        unitList.Add(d1);
        unitList.Add(d2);
        unitList.Add(d3);
        unitList.Add(dd);
        unitList.Add(d5);
        typewriter.AddDialogUnitList(unitList.ToArray());

        IDialogUnit[] arr;
        arr = typewriter.CatchNoDelayUnit();

        Assert.IsFalse(typewriter.unreadDialogUnitManager.isEmpty);
        Assert.AreEqual(2, typewriter.unreadDialogUnitManager.Count);
        Assert.AreEqual(dd, typewriter.unreadDialogUnitManager.PopDialogUnit());
        Assert.AreEqual(3, arr.Length);
    }
}
