using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义的选服控件 不需要继承
/// </summary>
public class ServerItem : MonoBehaviour
{
    public UIButton btn;
    public UILabel labInfo;
    private int beginIndex, endIndex;
    void Start()
    {
        btn.onClick.Add(new EventDelegate(() =>
        {
            //点击按钮后 通知服务器 更新右侧按钮
            ChooseServerPanel.Instance.UpdataPanel(beginIndex, endIndex);
            SelectRightServerID.begin = beginIndex;
            SelectRightServerID.end = endIndex;

            //用于重置每次打开的高度 防止数据自动往下掉落
            ChooseServerPanel.Instance.svLeft.ResetPosition();
            ChooseServerPanel.Instance.svRight.ResetPosition();
        }));
    }
    //外部动态创建按钮时 用于初始化 告诉 多少区到多少区
    public void InitInfo(int beginIndex,int endIndex)
    {
        //记录按钮代表区间 用于动态创建 单个服务器内容
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;   

        //改变显示区间内容
        labInfo.text = beginIndex + " - " +endIndex + "区";
    }
}
