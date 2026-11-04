using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanel : BasePanel<LoginPanel>
{
    public UIButton btnRe;
    public UIButton btnSure;

    public UIToggle togPW;
    public UIToggle togAuto;

    public UIInput inputUN;
    public UIInput inputPW;
    public override void Init()
    {
        #region 处理控件事件监听  
        btnRe.onClick.Add(new EventDelegate(() =>
        {
            //隐藏自己
            //点击注册
            HideMe();
            RegisterPanel.Instance.ShowMe();
        }));

        btnSure.onClick.Add(new EventDelegate(()=>
        {
            //判断 账号密码是否合法 
            if(LoginMar.Instance.CheckInfo(inputUN.value, inputPW.value))
            {
                LoginMar.Instance.LoginData.userName = inputUN.value;
                LoginMar.Instance.LoginData.passWord = inputPW.value;
                LoginMar.Instance.LoginData.rememberPW = togPW.value;
                LoginMar.Instance.LoginData.autoLogin = togAuto.value;
                LoginMar.Instance.SaveLoginData();
                LoginMar.Instance.GetUserData(LoginMar.Instance.LoginData.userName
        + LoginMar.Instance.LoginData.passWord);
                //如果玩家 之前选择过服务器 面板 
                //直接打开 对应选择的服务器面板
                if (LoginMar.Instance.LoginData.frontServerID == 0)
                {
                    ChooseServerPanel.Instance.ShowMe();
                }
                else
                {
                    //如果没有选择过 那就打开 对应的服务器选择面板
                    ServerPanel.Instance.ShowMe();
                }
                HideMe(); 
            }
            else
            {
                TipPanel.Instance.ShowMe();
                TipPanel.Instance.ChangeInfo("账号或者密码错误");
            }
        }));

        togAuto.onChange.Add(new EventDelegate(()=>
        {
            //记录数据
            //如果自动登陆选中了 那么记住密码 也应该强制选中
            if (togAuto.value)
            {
                //强制让自动密码选中
                togPW.value = true;
            }
        }));

        togPW.onChange.Add(new EventDelegate(()=>
        {
            //记录数据
            //如果记住密码 没有选中
            if (!togPW.value)
            {
                //强制让自动登陆也没有选中
                togAuto.value = false;
            }
        }));
        #endregion
        #region 初始化面板
        //更新数据面板
        //获取登录数据 更新面板上的显示
        LoginData data = LoginMar.Instance.LoginData;
        //更新内容
        //tog更新
        togPW.value = data.rememberPW;
        togAuto.value = data.autoLogin;

        //账号密码更新
        if (data.userName != "")
        {
            inputUN.value = data.userName;
        }
        if (data.rememberPW)
        {
            inputPW.value = data.passWord;
        }

        //自动登录更新
        if (data.autoLogin)
        {
            //判断 账号密码是否合法 
            if (LoginMar.Instance.CheckInfo(inputUN.value, inputPW.value))
            {
                //如果玩家 之前选择过服务器 面板 
                //直接打开 对应选择的服务器面板
                if (LoginMar.Instance.LoginData.frontServerID == 0)
                {
                    ChooseServerPanel.Instance.ShowMe();
                }
                else
                {
                    //如果没有选择过 那就打开 对应的服务器选择面板
                    ServerPanel.Instance.ShowMe();
                }
                HideMe(); 
            }
            else
            {
                TipPanel.Instance.ShowMe();
                TipPanel.Instance.ChangeInfo("账号或者密码错误");
            }
        }
        #endregion
    }

    //提供给外部使用 快捷设置 登陆面板 账号和密码 主要提供给注册面板
    public void SetInfo(string name,string passWord)
    {
        inputPW.value = passWord;
        inputUN.value = name;
    }
}
