using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class DialogCompilerTest
{

    [Test]
    public void DialogCompiler_InputDialog_ReturnCurrectCharOutput()
    {
        var compiler = new DialogCompiler(new CharOutputFactory(DialogUnitFactoryTest.GetText()), new FactoryInputCompiler());
        string dialog = "1234567890";
        var units = compiler.Build(dialog);

        Assert.AreEqual(dialog.Length, units.Length);
        for (int i = 0; i < dialog.Length; i++)
        {
            Assert.IsTrue(units[i] is CharOutput);
        }
    }
    [Test]
    public void DialogCompiler_InputDialogWithWrongHead_ThrowArgumentException()
    {
        var compiler = new DialogCompiler(new CharOutputFactory(DialogUnitFactoryTest.GetText()), new FactoryInputCompiler());
        string dialog = "1234567890[a]";
        
        Assert.Throws<ArgumentException>(() => compiler.Build(dialog));
    }

    [Test]
    public void DialogCompiler_InputDialog_ThrowArgumentException()
    {
        var compiler = new DialogCompiler(new CharOutputFactory(DialogUnitFactoryTest.GetText()), new FactoryInputCompiler());
        string dialog = "1234567890[a]";

        Assert.Throws<ArgumentException>(() => compiler.Build(dialog));
    }

    [Test]
    public void DialogCompiler_InputUpcaseAndLowcase_OutputLowCase()
    {
        var compiler = new DialogCompiler(new CharOutputFactory(DialogUnitFactoryTest.GetText()), new FactoryInputCompiler());
        compiler.AddDialogUnitFactory(new DummyFactoryForCompiler());
        string dialog = "123[q,dDq][A]";

        var units = compiler.Build(dialog);
        try
        {
            Assert.AreEqual(5, units.Length);
        }
        catch (Exception)
        {
            string s = "";
            for (int i = 0; i < units.Length; i++)
            {
                if (units[i] is CharOutput)
                {
                    s += (units[i] as CharOutput).dialog;
                }
                if (units[i] is DummyFactoryForCompiler.DummyUnit)
                {
                    s += (units[i] as DummyFactoryForCompiler.DummyUnit).contain;
                }
                s += "\n";
            }
            Debug.Log(s);
            throw;
        }
        Assert.IsTrue(units[3] is DummyFactoryForCompiler.DummyUnit);
        DummyFactoryForCompiler.DummyUnit temp = units[3] as DummyFactoryForCompiler.DummyUnit;
        Assert.AreEqual("[q,ddq]", temp.contain);
        temp = units[4] as DummyFactoryForCompiler.DummyUnit;
        Assert.AreEqual("[a]", temp.contain);
    }

    [Test]
    public void DialogCompiler_InputDialogWithUnitWithSpace_ReturnDummyUnitWithCurrentKeyValuPairWithOutSpace()
    {
        var compiler = new DialogCompiler(new CharOutputFactory(DialogUnitFactoryTest.GetText()), new FactoryInputCompiler());
        compiler.AddDialogUnitFactory(new DummyFactoryForCompiler());
        string dialog = "123[q,d  dq][a]";

        var units = compiler.Build(dialog);
        try
        {
            Assert.AreEqual(5, units.Length);
        }
        catch (Exception)
        {
            string s = "";
            for (int i = 0; i < units.Length; i++)
            {
                if (units[i] is CharOutput)
                {
                    s += (units[i] as CharOutput).dialog;
                }
                if (units[i] is DummyFactoryForCompiler.DummyUnit)
                {
                    s += (units[i] as DummyFactoryForCompiler.DummyUnit).contain;
                }
                s += "\n";
            }
            Debug.Log(s);
            throw;
        }
        Assert.IsTrue(units[3] is DummyFactoryForCompiler.DummyUnit);
        DummyFactoryForCompiler.DummyUnit temp = units[3] as DummyFactoryForCompiler.DummyUnit;
        Assert.AreEqual("[q,ddq]", temp.contain);
        temp = units[4] as DummyFactoryForCompiler.DummyUnit;
        Assert.AreEqual("[a]", temp.contain);
    }

    [Test]
    public void DialogCompiler_InputDialogWithUnit_ReturnDummyUnitWithCurrentKeyValuPair()
    {
        var compiler = new DialogCompiler(new CharOutputFactory(DialogUnitFactoryTest.GetText()), new FactoryInputCompiler());
        compiler.AddDialogUnitFactory(new DummyFactoryForCompiler());
        string dialog = "123[q,ddq][a]";

        var units = compiler.Build(dialog);
        try
        {
            Assert.AreEqual(5, units.Length);
        }
        catch (Exception)
        {
            string s="";
            for(int i = 0; i < units.Length; i++)
            {
                if(units[i] is CharOutput)
                {
                     s+=(units[i] as CharOutput).dialog;
                }
                if (units[i] is DummyFactoryForCompiler.DummyUnit)
                {
                    s += (units[i] as DummyFactoryForCompiler.DummyUnit).contain;
                }
                s += "\n";
            }
            Debug.Log(s);
            throw;
        }
        Assert.IsTrue(units[3] is DummyFactoryForCompiler.DummyUnit);
        DummyFactoryForCompiler.DummyUnit temp = units[3] as DummyFactoryForCompiler.DummyUnit;
        Assert.AreEqual("[q,ddq]", temp.contain);
        temp = units[4] as DummyFactoryForCompiler.DummyUnit;
        Assert.AreEqual("[a]", temp.contain);
    }
}
