using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Text.RegularExpressions;
using System;


public class StringTest
{

    [Test]
    public void StringIsInt_CanDetectivIntString()
    {
        Assert.AreEqual(5, int.Parse("+5"));
        Assert.AreEqual(-5, int.Parse("-5"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsInt("aa99a"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsInt("aa99"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsInt("a-99"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsInt("99"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsInt("+99"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsInt("-0599"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsInt("-0599.000"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsInt("-0599.002"));
    }

    [Test]
    public void StringIsFloat_CanDetectivFloatString()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsFloat("aa99a"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsFloat("aa99"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsFloat("a-99"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsFloat("9"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsFloat("99"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsFloat("+99"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsFloat("-0599"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsFloat("-0599.000"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsFloat("-0599.002"));
    }

    [Test]
    public void StringIsVector2_CanDetectivVector2String()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector2("aa99a"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector2("(5,a)"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector2("()"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector2("(5.3,5.2,5)"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector2("( 5,5)"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsVector2("(5,5)"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsVector2("(5.0,5.2)"));
    }

    [Test]
    public void StringIsVector3_CanDetectivVector3String()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector3("aa99a"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector3("(5,a)"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector3("()"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector3("(5,5)"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector3("(5.0,5.2)"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector3("(5.0,5,5.2"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector3("5.0,6.0,5.2)"));
        Assert.AreEqual(false, KiriTalkUtility.StringIsVector3("(5.3 ,5.2,5)"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsVector3("(5.3,5.2,5)"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsVector3("(5.0,5,5.2)"));
        Assert.AreEqual(true, KiriTalkUtility.StringIsVector3("(5.0,6.0,5.2)"));
    }

    [Test]
    public void ToVector2_CanParseStringToVector2()
    {
        Assert.AreEqual(new Vector2(5, 5), KiriTalkUtility.ToVector2("(5,5)"));
        Assert.AreEqual(new Vector2(5.0f, 5.2f), KiriTalkUtility.ToVector2("(5.0,5.2)"));
    }

    [Test]
    public void ToVector3_CanParseStringToVector3()
    {
        Assert.AreEqual(new Vector3(6,5, 5), KiriTalkUtility.ToVector3("(6,5,5)"));
        Assert.AreEqual(new Vector3(8,5.0f, 5.2f), KiriTalkUtility.ToVector3("(8,5.0,5.2)"));
    }
}
