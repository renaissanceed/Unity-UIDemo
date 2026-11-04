using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    //存储面板的容器
    private Dictionary<string,BasePanel> panelDic = new Dictionary<string,BasePanel>();
    //应该一开始 得到canvas对象
    private Transform canvasTrans;
    private UIManager()
    {
        //从场景上得到创建好的 Canvas对象
        canvasTrans = GameObject.Find("Canvas").transform;
        //让Canvs对象 过场景不移除
        //都是通过 动态创建删除面板的 不删除它影响不大
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }

    //显示面板
    public T ShowPanel<T>() where T : BasePanel
    {
        //我们只需要保证 泛型T类型 和 面板 一致即可
        string panelName = typeof(T).Name;

        //是否已经有显示着的该面板了 如果有 就不用创建 直接返回给外部即可
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        
        //显示面板 创建一个面板预设体 设置父对象
        //根据得到的类名 就是我们预设体面板名 直接动态创建它即可
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans,false);

        //得到对应的面板脚本 存储起来
        T panel = panelObj.GetComponent<T>();
        //把面板脚本 存储到对应容器中
        panelDic.Add(panelName, panel);
        //调用显示自己
        panel.ShowMe();

        return panel;
    }

    //隐藏面板
    //参数一：如果希望淡出 就默认传入ture 如果希望直接隐藏(删除) 就传入false
    public void HidePanel<T>(bool isFade = true) where T:BasePanel
    {
        //根据 泛型类型 得到面板名字
        string panName = typeof(T).Name;
        //判断当前显示的面板 有没有该名字的面板
        if (panelDic.ContainsKey(panName))
        {
            if (isFade)
            {
                panelDic[panName].HideMe(() =>
                {
                    //面板 淡出成功后 希望删除面板
                    GameObject.Destroy(panelDic[panName].gameObject);
                    //删除面板后 从字典中移除
                    panelDic.Remove(panName);
                });
            }
            else
            {
                //面板 淡出成功后 希望删除面板  
                GameObject.Destroy(panelDic[panName].gameObject);
                //删除面板后 从字典中移除
                panelDic.Remove(panName);
            }
        }
    }

    //获得面板
    public T GetPanel<T>() where T:BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        //没有面板就返回空
        return null;
    }
}
