using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登陆界面记住的内容数据
/// </summary>
public class LoginData
{
    //用户名
    public string userName;
    //密码
    public string password;

    //是否记住密码
    public bool rememberPW;
    //是否自动登陆
    public bool autoLogin;

    //服务器相关
    //-1 代表没有选择过服务器
    public int frontServerID = 0;

    //用于给外部 拷贝自身数据
    public LoginData Clone()
    {
        return new LoginData
        {
            userName = this.userName,
            password = this.password,
            rememberPW = this.rememberPW,
            autoLogin = this.autoLogin,
            frontServerID = this.frontServerID
        };
    }
}

