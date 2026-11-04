using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NO5 : MonoBehaviour
{
    public string[] str;

    public Rect scPos, showPos;

    private Vector2 v;
    private void OnGUI()
    {
        showPos.height = str.Length * 30;
        v = GUI.BeginScrollView(scPos, v, showPos);
        for (int i = 0; i < str.Length; i++)
        {
            GUI.Label(new Rect(0, i*30 ,100 ,30), str[i]);
        }
        GUI.EndScrollView();
    }
}
