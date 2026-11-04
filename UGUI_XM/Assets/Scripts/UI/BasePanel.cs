using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //淡入淡出的组件
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed = 3f;
    //是否开始显示
    private bool isShow;
    //淡出成功时 执行的委托函数
    private UnityAction hideCallBack;
    protected virtual void Awake()
    {
        //一开始获取面板上的挂载组件 如果没有给它添加一个
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 主要用于 初始化 按钮事件监听等内容
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 显示自己 做的事情
    /// </summary>
    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// 隐藏自己 做的事情
    /// </summary>
    public virtual void HideMe(UnityAction callBack)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = false; 
        canvasGroup.blocksRaycasts = false;
        //记录当淡出成功时 会执行的函数
        hideCallBack = callBack;
    }

    private void Update()
    {
        //淡入
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出
        else if (!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //应该让管理器 删除自己
                hideCallBack?.Invoke();
            }
        }
    }
}
