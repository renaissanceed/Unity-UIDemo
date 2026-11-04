using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static  T Instance => instance;
    protected virtual void Awake()
    {
        instance = this as T;
    }

    protected virtual void Start()
    {
        //初始化 主要用于 监听按钮 响应事件等
        Init();
    }

    /// <summary>
    /// 方便子类继承时 进行初始化操作
    /// 只要子类在其中写一些初始化逻辑 
    /// 最终都会在Start中执行
    /// 子类就不需要去执行start函数
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 显示自己
    /// </summary>
    public virtual void ShowMe()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HideMe()
    {
        gameObject.SetActive(false);
    }
}
