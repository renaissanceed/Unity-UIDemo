using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NO2 : MonoBehaviour
{
    public Rect rect , rect2 ,rect3 ,rect4;

    private int nowSelIndex = 0;

    private string[] strings = new string[3] { "装备栏", "升级栏", "进化栏"};

    public GUIStyle style ,style1;

    public Texture tex;

    public GameObject obj1;

    private NO4 obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = obj1.GetComponent<NO4>();
    }

    private void OnGUI()
    {
        GUI.DrawTexture(rect2 ,tex);

        if(GUI.Button(rect4, "", style1))
        {
            obj.enabled = true;
            gameObject.SetActive(false);
        }

        nowSelIndex =  GUI.SelectionGrid(rect, nowSelIndex, strings, 3 , style);
        switch (nowSelIndex)
        {
            case 0:
                GUI.Label(rect3, "装备界面");
                break;
            case 1:
                GUI.Label(rect3, "升级界面");
                break;
            case 2:
                GUI.Label(rect3, "进化界面");
                break;
        }
    }
}
