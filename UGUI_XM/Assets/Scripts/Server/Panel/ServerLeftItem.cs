using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    //按钮自己
    public Button btnSelf;
    //显示的区间内容
    public Text txtInfo;

    //记录区间
    private int beginIndex;
    private int endIndex;

    void Start()
    {
        //按钮自己点击逻辑
        btnSelf.onClick.AddListener(() =>
        {
            //通知选服面板 改变右侧区间内容
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdatePanel(beginIndex, endIndex);
        });
    }
    
    //提供给外部的区间的值
    public void InitInfo(int begin,int end)
    {
        beginIndex = begin;
        endIndex = end;

        txtInfo.text = beginIndex + " - " + endIndex + "区";
    }
}
