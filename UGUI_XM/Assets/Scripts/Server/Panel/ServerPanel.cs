using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    //返回按钮
    public Button btnBack;
    //服务器选择按钮
    public Button btnChange;
    //进入游戏按钮
    public Button btnEnter;
    //选服名
    public Text txtName; 
    public override void Init()
    {
        //返回按钮逻辑
        btnBack.onClick.AddListener(() =>
        {
            //避免自动登陆时 返回登陆界面 出现问题
            if (LoginMgr.Instance.LoginData.autoLogin)
            {
                LoginMgr.Instance.LoginData.autoLogin = false;
            }
            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
        });

        //服务器选择按钮逻辑
        btnChange.onClick.AddListener(() =>
        {
            //打开服务器选择面板
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>(); 
        });

        //进入游戏按钮逻辑
        btnEnter.onClick.AddListener(() =>
        {
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
            //隐藏登陆背景图
            UIManager.Instance.HidePanel<BKPanel>();
            //存储一次数据 存储它 选择的服务器ID
            LoginMgr.Instance.SaveLoginData();
            //打开游戏界面
            SceneManager.LoadScene("GameScene"); 
        });
    }

    public override void ShowMe()
    {
        base.ShowMe(); 
        //显示自己的时候 更新当前选择的服务器名字
        //通过用户名得到 上一次服务器ID来更新内容
        LoginData data = LoginMgr.Instance.GetLoginData();
        if (data != null)
        {
            int id = data.frontServerID;
            print(data.frontServerID);
            if (id <= 0)
            {
                txtName.text = "无";
            }
            else
            {
                ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
                txtName.text = info.name;
            }
        } 
    }
}
