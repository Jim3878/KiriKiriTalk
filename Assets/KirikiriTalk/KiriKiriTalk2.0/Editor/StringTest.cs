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
    public void IntIsMachedPasses()
    {
        Assert.AreEqual(5, int.Parse("+5"));
        Assert.AreEqual(-5, int.Parse("-5"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsInt("aa99a"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsInt("aa99"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsInt("a-99"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsInt("99"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsInt("+99"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsInt("-0599"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsInt("-0599.000"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsInt("-0599.002"));
    }

    [Test]
    public void FloatIsMachedPasses()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsFloat("aa99a"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsFloat("aa99"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsFloat("a-99"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsFloat("9"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsFloat("99"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsFloat("+99"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsFloat("-0599"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsFloat("-0599.000"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsFloat("-0599.002"));
    }

    [Test]
    public void Vector2IsMachedPasses()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector2("aa99a"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector2("(5,a)"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector2("()"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector2("(5.3,5.2,5)"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector2("( 5,5)"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsVector2("(5,5)"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsVector2("(5.0,5.2)"));
    }

    [Test]
    public void Vector3IsMachedPasses()
    {
        Assert.AreEqual(5f, float.Parse("+5.0"));
        Assert.AreEqual(-5.2f, float.Parse("-5.2"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector3("aa99a"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector3("(5,a)"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector3("()"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector3("(5,5)"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector3("(5.0,5.2)"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector3("(5.0,5,5.2"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector3("5.0,6.0,5.2)"));
        Assert.AreEqual(false, KiriKiriTalkUtility.StringIsVector3("(5.3 ,5.2,5)"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsVector3("(5.3,5.2,5)"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsVector3("(5.0,5,5.2)"));
        Assert.AreEqual(true, KiriKiriTalkUtility.StringIsVector3("(5.0,6.0,5.2)"));
    }

    [Test]
    public void Vector2ParserPasses()
    {
        Assert.AreEqual(new Vector2(5, 5), KiriKiriTalkUtility.ToVector2("(5,5)"));
        Assert.AreEqual(new Vector2(5.0f, 5.2f), KiriKiriTalkUtility.ToVector2("(5.0,5.2)"));
    }

    [Test]
    public void Vector3ParserPasses()
    {
        Assert.AreEqual(new Vector3(6,5, 5), KiriKiriTalkUtility.ToVector3("(6,5,5)"));
        Assert.AreEqual(new Vector3(8,5.0f, 5.2f), KiriKiriTalkUtility.ToVector3("(8,5.0,5.2)"));
    }


    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator StringTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
