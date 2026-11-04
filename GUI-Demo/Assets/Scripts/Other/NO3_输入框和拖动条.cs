using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class NO3_输入框和拖动条 : MonoBehaviour
{
    public Rect rect2 , rect3 , rect4 , rect5 , rect6 , rect7 , rect8 , rect;

    private string name = "";

    private string password ="";

    public GUIStyle Style, TextStyle, ButtonStyle;

    public Texture tex;

    private void OnGUI()
    {
        GUI.DrawTexture(rect, tex);
        GUI.Label(rect6, "逃离塔科夫", Style);

        GUI.Label(rect7, "用户名", TextStyle);

        GUI.Label(rect8, "密码" , TextStyle);

        name = GUI.TextField(rect2, name);
        password = GUI.PasswordField(rect3, password, '*');

        if(GUI.Button(rect4, "登陆", ButtonStyle))
        { 
            if (name == "admin" && password == "8888" )
            {
                SceneManager.LoadScene("004"); 
            }
            else
            {
                name = "";
                password = "";
            }
        }
        
        if(GUI.Button(rect5, "返回", ButtonStyle))
        {
            SceneManager.LoadScene("SampleScene");
        }

    }
}

