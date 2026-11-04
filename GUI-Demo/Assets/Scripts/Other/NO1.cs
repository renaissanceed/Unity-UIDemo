using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NO1 : MonoBehaviour
{
    public Rect rect1, rect2, rect, rect3, rect4;
    public GUIStyle style;

    public GUIStyle style2;

    public Texture tex;

    public bool index = false;
    // Start is called before the first frame update

    private void OnGUI()
    {
        GUI.DrawTexture(rect4, tex);
        GUI.Label(rect3, "逃离塔科夫",style);
        if(GUI.Button(rect, "开始游戏", style2))
        {
            SceneManager.LoadScene("002");
        }
        if (GUI.Button(rect1, "退出游戏", style2))
        {
            index = true;
            enabled = false;
        }
        if (GUI.Button(rect2, "设置", style2))
        {
            SceneManager.LoadScene("003");
        }
    }
}
