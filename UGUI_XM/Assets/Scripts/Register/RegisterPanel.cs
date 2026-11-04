using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    //确定按钮
    public Button btnSure;
    //取消按钮
    public Button btnCancel;

    //账号输入
    public InputField inputUN;
    //密码输入
    public InputField inputPW;
    public override void Init()
    {
        //取消按钮逻辑
        btnCancel.onClick.AddListener(() =>
        {
            //隐藏自己
            UIManager.Instance.HidePanel<RegisterPanel>();
            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
        });

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

            //注册账号密码
            if (LoginMgr.Instance.RegisterUser(inputUN.text,inputPW.text))
            {
                //用于清理登陆数据 用于 新注册账号的重置 不然会残留
                LoginMgr.Instance.ClearLoginData();

                //注册成功
                //显示登录面板
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                //更新登陆面板上的 用户名和密码
                loginPanel.SetInfo(inputUN.text, inputPW.text);

                //隐藏自己
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                //注册失败
                //显示提示面板
                TipPanel tip = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示内容
                tip.ChangeInfo("账号已经被注册!");

                //清空输入的账号和密码
                inputUN.text = "";
                inputPW.text = "";
            }
        });
    }
}
