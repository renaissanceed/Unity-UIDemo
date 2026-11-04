using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class LoginPanel : BasePanel
{
    //确定按钮
    public Button btnSure;
    //登陆注册按钮
    public Button btnRe;

    //账号输入
    public InputField inputUN;
    //密码输入
    public InputField inputPW;

    //记住密码
    public Toggle togPW;
    //自动登录
    public Toggle togAuto;
    public override void Init()
    {
        //确定按钮逻辑
        btnSure.onClick.AddListener(() =>
        {
            //判断输入的账号密码是否合理
            if (inputPW.text.Length < 6 || inputUN.text.Length < 6)
            {
                //不合理 显示提示面板
                TipPanel tip = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示内容
                tip.ChangeInfo("账号或者密码长度不合理!");
                return;
            }

            //先切换数据
            LoginMgr.Instance.SwitchAccount(inputUN.text);

            //判断账号密码 是否通过
            if (LoginMgr.Instance.CheckInfo(inputPW.text, inputUN.text))
            {
                //登陆成功
                //记录数据
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.password = inputPW.text;
                LoginMgr.Instance.LoginData.rememberPW = togPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin = togAuto.isOn;
                LoginMgr.Instance.SaveLoginData();
                LoginMgr.Instance.SaveSingleUser(inputUN.text, LoginMgr.Instance.LoginData);

                //如果从来没有选择过服务器就 打开选服面板
                if (LoginMgr.Instance.AccountServer.accountServer[inputUN.text].frontServerID <= 0)
                {
                    //打开选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //打开服务器相关面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //删除自己
                UIManager.Instance.HidePanel<LoginPanel>();
            }
            else
            {
                //登陆失败
                //不合理 显示提示面板
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或者密码错误!");
            }
            
        });

        //注册按钮逻辑
        btnRe.onClick.AddListener(() =>
        {
            //删除自己
            UIManager.Instance.HidePanel<LoginPanel>();
            //加载注册面板
            UIManager.Instance.ShowPanel<RegisterPanel>();
        });

        //记住密码逻辑
        togPW.onValueChanged.AddListener((isOn) =>
        {
            //如果记住密码没选中 自动登陆也该取消选中
            if (!isOn)
            {
                togAuto.isOn = false; 
            }
        });

        //自动登陆逻辑
        togAuto.onValueChanged.AddListener((isOn) =>
        {
            //自动登陆选中 应该把记住密码也选中
            if (isOn)
            {
                togPW.isOn = true;
            }
        });
    }

    //数据更新内容
    public void UpdataLogin()
    {
        //得到数据
        LoginData loginData = LoginMgr.Instance.GetLoginData();

        //初始化
        //更新两个多选框
        togPW.isOn = loginData.rememberPW;
        togAuto.isOn = loginData.autoLogin;

        //更新账号密码
        inputPW.text = loginData.password;
        //根据你是否上一次勾选了记住密码 来决定更新密码
        if (togPW.isOn)
        {
            inputUN.text = loginData.userName;
        }

        //自动登陆做什么
        if (togAuto.isOn)
        {
            //自动去认证账号密码相关
            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPW.text))
            {
                //如果从来没有选择过服务器就 打开选服面板
                if (LoginMgr.Instance.AccountServer.accountServer[inputUN.text].frontServerID <= 0)
                {
                    //打开选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //打开服务器相关面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //删除自己
                UIManager.Instance.HidePanel<LoginPanel>(false);
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或者密码错误!");
            }
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdataLogin();
    }

    //给外部更新设置用户名和密码
    public void SetInfo(string userName, string passWord)
    {
        inputUN.text = userName;
        inputPW.text = passWord;
    }
}
