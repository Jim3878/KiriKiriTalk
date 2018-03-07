using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region Variable
public class BtnData
{
    public string txt;
    public Action btnAction;
    public BtnData(string txt, Action btnAction)
    {
        this.txt = txt;
        this.btnAction = btnAction;
    }
}
#endregion

public class UIDebug : MonoBehaviour
{

    static Dictionary<int, string> txtList;
    static Dictionary<int, BtnData> btnList;

    GUIStyle style = new GUIStyle();

    public static void Log(int index, string txt)
    {
        if (txtList == null)
        {
            txtList = new Dictionary<int, string>();
        }
        if (!txtList.ContainsKey(index))
        {
            txtList.Add(index, txt);
        }
        else
        {
            txtList.Remove(index);
            txtList.Add(index, txt);
        }
    }

    public static void AddButton(int index, string txt, Action btnAction)
    {
        if (btnList == null)
        {
            btnList = new Dictionary<int, BtnData>();
        }
        if (btnList.ContainsKey(index))
        {
            btnList.Remove(index);
        }
        btnList.Add(index, new BtnData(txt, btnAction));
    }

    public static void RemoveLog(int index)
    {
        if (txtList != null && txtList.ContainsKey(index))
        {
            txtList.Remove(index);
        }
    }

    private void OnGUI()
    {

        style.alignment = TextAnchor.UpperLeft;
        style.normal.textColor = Color.green;
        style.normal.background = MakeTex(1, 1, new Color(0, 0, 0, 0.3f));
        string context = "";
        //debug log
        if (txtList != null)
        {
            foreach (KeyValuePair<int, string> kvp in txtList)
            {
                context += kvp.Key + ".\t";
                context += kvp.Value;
                context += "\n";
            }
            GUI.Box(new Rect(0, 0, 400, 20 * txtList.Count), context, style);
        }

        //debug button
        if (btnList != null)
        {
            int n = 0;
            foreach (KeyValuePair<int, BtnData> kvp in btnList)
            {
                if (GUI.Button(new Rect(500, 30 * n, 100, 20), kvp.Value.txt))
                {
                    kvp.Value.btnAction();
                }
                n++;
            }
        }
    }



    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

}
