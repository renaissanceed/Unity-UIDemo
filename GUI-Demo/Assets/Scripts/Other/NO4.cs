using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NO4 : MonoBehaviour
{
    public Rect rect;

    public GUIStyle style;

    public GameObject obj;

    private NO4 no2;
    
    void Start()
    {
        obj.gameObject.SetActive(false);
        no2 = GetComponent<NO4>();
    }

    private void OnGUI()
    {
        if(GUI.Button(rect,"打开界面", style))
        {
            obj.gameObject.SetActive(true);
            no2.enabled = false;
        }
    }
}
