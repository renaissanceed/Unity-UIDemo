using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 设置 : MonoBehaviour
{
    public GameObject obj;

    private AudioSource source;

    private float hO = 0.3f;

    public Rect rect , rect3 , rect4 , rect5 , rect2;

    public GUIStyle style , gUIStyle;

    private bool isMusic = false , isMusic2 = false;

    public Texture tex;

    // Start is called before the first frame update
    void Start()
    {
        source = obj.GetComponent<AudioSource>();
        source.volume = hO;
    }

    private void OnGUI()
    {
        GUI.DrawTexture(rect2 , tex);
        source.volume = hO;
        hO = GUI.HorizontalSlider(rect, hO, 0, 1);

        if(GUI.Button(rect3, "", style))
        {
            SceneManager.LoadScene("SampleScene");
        }

        isMusic = GUI.Toggle(rect4, isMusic,"背景音乐", gUIStyle);
        source.gameObject.SetActive(isMusic);

        isMusic2 = GUI.Toggle(rect5, isMusic2, "音效", gUIStyle);
    }
}
