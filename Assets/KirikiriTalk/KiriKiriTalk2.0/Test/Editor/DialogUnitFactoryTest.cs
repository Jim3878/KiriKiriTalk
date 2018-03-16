using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DialogUnitFactoryTest
{
    //public class BaseDialogUnitFactoryTest
    //{
    //    [Test]
    //    public void CanBuild_OnCurrectValue_ReturnTrue()
    //    {
    //        var factory = GetDummyFactory();
    //        string s1 = "[dummy]";
    //        string s2 = "[dummy,a,b,c]";
    //        string s3 = "[dummy,a=0,b=0,c=0,s=\"\",v2=(2,3,2),v3=(2,2,2)]";
    //        string s4 = "[dummy,a=0,b=5,s=\"aaa\",v2=(2,2),v3=(2,2,2)]";


    //        Assert.IsFalse(factory.CanBuild(s1));
    //        Assert.IsFalse(factory.CanBuild(s2));
    //        Assert.IsFalse(factory.CanBuild(s3));
    //        Assert.IsTrue(factory.CanBuild(s4));
    //    }

    //    [Test]
    //    public void BuildDialogUnit_OnWrongValue_Throws()
    //    {
    //        var factory = GetDummyFactory();
    //        string s1 = "[dummy]";
    //        string s2 = "[dummy,a,b,c]";
    //        string s3 = "[dummy,a=0,b=0,c=0,s=\"aaa\",v2=(2,3,2),v3=(2,2,2)]";
    //        string s4 = "[dummy,a=0,b=5,s=\"\",v2=(2,2),v3=(2,2,2)]";
    //        new DummyFactory.DummyDialogUnit(0, 5, new Vector2(2, 2), new Vector3(2, 2, 2), "");

    //        Assert.Throws<KeyNotFoundException>(()=>factory.BuildDialogUnit(1,s1));
    //        Assert.Throws<KeyNotFoundException>(() => factory.BuildDialogUnit(1, s2));
    //        Assert.Throws<KeyNotFoundException>(() => factory.BuildDialogUnit(1, s3));
    //    }

    //    [Test]
    //    public void BuildDialogUnit_ReturnCurrectDialog()
    //    {
    //        var factory = GetDummyFactory();
    //        string s4 = "[dummy,a=0,b=5,s=\"aaa\",v2=(2,2),v3=(2,2,2)]";
    //        //string s4 = "[dummy,a=0,b=5,s=\"aaa\",v2=(2,2),v3=(2,2,2)]";
    //        var result= new DummyFactory.DummyDialogUnit(0, 5, new Vector2(2, 2), new Vector3(2, 2, 2), "aaa");

    //        var ans = factory.BuildDialogUnit(0,s4) as DummyFactory.DummyDialogUnit;           
    //        Assert.AreEqual(result.a, ans.a);
    //        Assert.AreEqual(result.b, ans.b);
    //        Assert.AreEqual(result.s, ans.s);
    //        Assert.AreEqual(result.v2, ans.v2);
    //        Assert.AreEqual(result.v3, ans.v3);

    //    }

    //    IDialogUnitFactory GetDummyFactory()
    //    {
    //        return new DummyFactory();
    //    }
    //}

    public class CharOutputFactoryTest
    {
        [Test]
        public void CanBuilt_OnInputValue_ReturnTrue()
        {
            var text = GetText();
            CharOutputFactory factory = new CharOutputFactory(text);

            CanBuild_InputCurrectValue_ReturnTrue(factory, GetValues("char","qqq"));
        }

        [Test]
        public void CanBuilt_OnInputWrongValue_ReturnFalse()
        {
            var text = GetText();
            CharOutputFactory factory = new CharOutputFactory(text);
            
            CanBuild_InputWrongValue_ReturnFalse(factory, GetValues("[char", "qqq"));
        }

        [Test]
        public void BuildDialogUnit_BuildCurrectUnit()
        {
            var text = GetText();
            CharOutputFactory factory = new CharOutputFactory(text);
            string value = "qqq";

            var unit = BuildDialogUnit(factory, GetValues("char", value));

            Assert.IsTrue(unit is CharOutput);
            Assert.AreEqual(value, (unit as CharOutput).dialog);
        }
    }

    public static Dictionary<string, string> GetValues(params string[] str)
    {
        Dictionary<string, string> d = new Dictionary<string, string>();
        try
        {
            for (int i = 0; i < str.Length - 1; i += 2)
            {
                d.Add(str[i], str[i + 1]);
            }
            return d;
        }
        catch (IndexOutOfRangeException)
        {
            string r = "";
            for (int i = 0; i < str.Length; i++)
            {
                r += str[i];
                r += " ";
            }
            Debug.Log(string.Format("參數錯了\nleng = {0}\narray = {1}", str.Length, r));
            throw;
        }

    }

    public static void CanBuild_InputCurrectValue_ReturnTrue(IDialogUnitFactory factory, Dictionary<string, string> value)
    {
        Assert.IsTrue(factory.CanBuild(value));
    }

    public static void CanBuild_InputWrongValue_ReturnFalse(IDialogUnitFactory factory, Dictionary<string, string> value)
    {
        Assert.IsFalse(factory.CanBuild(value));
    }

    public static IDialogUnit BuildDialogUnit(IDialogUnitFactory factory, Dictionary<string, string> value, int ID = -1)
    {
        return factory.BuildDialogUnit(ID, value);
    }
    
    public static Text GetText()
    {
        GameObject go = new GameObject();
        return go.AddComponent<Text>();
    }

}
