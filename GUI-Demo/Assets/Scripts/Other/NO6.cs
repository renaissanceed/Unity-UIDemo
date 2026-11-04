using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NO6 : MonoBehaviour
{
    public GameObject obj;

    private NO1 no;

    public Rect rect , rect2 ,rect3 , rect4;

    public Texture tex;

    public GUIStyle style ,style2 ,style3;

    // Start is called before the first frame update
    void Start()
    {
       no = obj.GetComponent<NO1>();
    }

    // Update is called once per frame
    private void OnGUI()
    {
        if (no.index)
        {
            GUI.ModalWindow(1, rect, DrawWindow, tex, style);
        }
    }   
    private void DrawWindow(int id)
    {
        GUI.Label(rect2, "是否退出？", style2);
        if (GUI.Button(rect3, "退出", style3))
        {
            Application.Quit();
            print("按下退出");
        }
        if (GUI.Button(rect4, "取消", style3))
        {
            no.index = false;
            no.enabled = true;
        }
    }
}
