 using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
/// <summary>
/// 对齐方式枚举
/// </summary>
public enum E_Alignment_Type
{
    Up, 
    Down, 
    Left, 
    Right,
    Center,
    Left_Up,
    Right_Up,
    Left_Down,
    Right_Down,
}

[System.Serializable]
public class CustomGUIPos
{
    //处理空间位置相关内容
    //完成分辨率自适应的相关计算
    private Rect rPos = new Rect(0 , 0 , 100 , 100); 
    //屏幕九宫格对齐方式
    public E_Alignment_Type screen_Alignment_Type = E_Alignment_Type.Center;
    //控件中心对齐方式
    public E_Alignment_Type control_Center_Alignment_Type = E_Alignment_Type.Center;
    //偏移位置
    public Vector2 pos;
    //宽高
    public float width = 100 , height = 50 ;
    //用于计算 中心点 成员变量
    private Vector2 centerPos;
    /// <summary>
    /// 得到最终绘制的位置 和 宽高
    /// </summary>
    public Rect Pos
    {
        get 
        {
            //进行计算中心点偏移位置
            CalCenterPos();
            //计算相对屏幕坐标
            CalPos();

            //宽高直接赋值
            rPos.width = width; 
            rPos.height = height;
            return rPos; 
        }
    }
    //计算中心点偏移的方法
    private void CalCenterPos()
    {
        switch (control_Center_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                centerPos.x = -width / 2;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Down:
                centerPos.x = width / 2;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Left:
                centerPos.x = 0;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Right:
                centerPos.x = -width;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Center:
                centerPos.x = -width / 2;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Left_Up:
                centerPos.x = 0;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Right_Up:
                centerPos.x = -width;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Left_Down:
                centerPos.x = 0;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Right_Down:
                centerPos.x = -width;
                centerPos.y = -height;
                break;
        }
    }
    //计算最终相对位置坐标的方法
    private void CalPos()
    {
        switch (screen_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                rPos.x = Screen.width/2 + centerPos.x + pos.x;
                rPos.y = centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Down:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_Alignment_Type.Left:
                rPos.x = centerPos.x + pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Center:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left_Up:
                rPos.x = centerPos.x + pos.x;
                rPos.y = centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right_Up:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left_Down:
                rPos.x = centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_Alignment_Type.Right_Down:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
        }
    }
}
