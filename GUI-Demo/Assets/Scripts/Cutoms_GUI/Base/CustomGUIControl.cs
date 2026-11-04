using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Style_OnOff
{
    On,
    Off,
}

public abstract class CustomGUIControl : MonoBehaviour 
{
    //提取相同的内容信息 
    //位置信息
    public CustomGUIPos guiPos;
    //显示内容信息
    public GUIContent content;
    //自定义样式
    public GUIStyle style;
    //自定义样式是否启用开关
    public E_Style_OnOff style_OnOff = E_Style_OnOff.Off;

    //提供给外部绘制的成员方法
    public void DrawGUI()
    {
        switch (style_OnOff)
        {
            case E_Style_OnOff.On:
                StyleOnDraw();
                break;
            case E_Style_OnOff.Off:
                StyleOffDraw();
                break;
        }
    }
    /// <summary>
    /// 自定义样式 开启时的绘制方法
    /// </summary>
    protected abstract void StyleOnDraw();


    /// <summary>
    /// 自定义样式 关闭时的绘制方法
    /// </summary>
    protected abstract void StyleOffDraw();
}
