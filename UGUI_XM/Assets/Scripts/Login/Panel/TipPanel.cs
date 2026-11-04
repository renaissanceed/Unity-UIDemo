using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    //提示文本
    public Text txtInfo;
    //确定按钮
    public Button btnSure;
    public override void Init()
    {
        //初始化 按钮事件监听
        btnSure.onClick.AddListener(() => 
        {
            //隐藏自己
            UIManager.Instance.HidePanel<TipPanel>();
        });
    } 

    /// <summary>
    /// 提示内容改变
    /// </summary>
    /// <param name="str">提示内容</param>
    public void ChangeInfo(string str)
    {
        txtInfo.text = str;
    }
}
