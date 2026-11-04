using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPanel : BasePanel<RegisterPanel>
{
    //确认 和 关闭 按钮
    public UIButton btnSure;
    public UIButton btnClose;
    //用户名 和 密码 输入框
    public UIInput inputUN;
    public UIInput inputPW;
    public override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(() =>
        {
            //判断输入的账号密码是否合法
            if (inputUN.value.Length < 6 || inputPW.value.Length < 6)
            {
                TipPanel.Instance.ShowMe();
                TipPanel.Instance.ChangeInfo("账号和密码必须大于6位");
                return;
            }

            //用户注册成功
            if (LoginMar.Instance.RegisterUser(inputUN.value,inputPW.value))
            {
                LoginPanel.Instance.ShowMe();
                LoginPanel.Instance.SetInfo(inputUN.value, inputPW.value);  
                //注册成功后 我们应该把登陆数据 重置
                LoginMar.Instance.ClearLoginData();
                HideMe();
            }
            else
            {
                TipPanel.Instance.ShowMe();
                TipPanel.Instance.ChangeInfo("账号已经被注册");
            }
        }));

        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            LoginPanel.Instance.ShowMe();
        }));
        HideMe();
    }

    //重写ShowMe 把上次写的用户名和密码重置 方便玩家操作
    public override void ShowMe()
    {
        base.ShowMe();
        inputUN.value = "";
        inputPW.value = "";
    }
}
