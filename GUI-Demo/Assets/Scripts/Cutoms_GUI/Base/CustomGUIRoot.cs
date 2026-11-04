using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    //���ڴ洢�Ӷ������е�GUI����
    private CustomGUIControl[] allControls;

    void Start()
    {
        allControls = this.GetComponentsInChildren<CustomGUIControl>();
    }

    //�����Ӷ���ؼ�������
    private void OnGUI()
    {
        ////�༭״̬�� �Ż�һֱ����
        //if(!Application.isPlaying)
        //{ 
            //ÿ�λ���֮ǰ �õ������Ӷ���ؼ��� ����ű�
            allControls = this.GetComponentsInChildren<CustomGUIControl>();
        //}

        for (int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI(); 
        }
    }
}
