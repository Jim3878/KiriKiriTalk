using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using DG.Tweening;

public class ColorHexText {

	[Test]
	public void Color_ToHexString() {
        
        Color white = Color.white;
        Assert.AreEqual(Color.white.ToHex(), "#ffffff");
        // Use the Assert class to test conditions.

    }
}
