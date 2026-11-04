using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipPanel : BasePanel<TipPanel>
{
    public UIButton btnSure;
    public UILabel labSure;
    public override void Init()
    {
        //初始化逻辑
        btnSure.onClick.Add(new EventDelegate(() =>
        {
            //隐藏自己
            HideMe();
        }));
        //一开始就把自己隐藏掉
        HideMe();
    }
    /// <summary>
    /// 改变提示面板的提示内容 
    /// </summary>
    /// <param name="info"></param>
    public void ChangeInfo(string info)
    {
        labSure.text = info;
    }
}
