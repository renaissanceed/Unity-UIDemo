using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 记录玩家在登录面板 需要记录的信息
/// </summary>
public class LoginData
{
    public string userName;
    public string passWord;
    //记录上一次登陆的服务器ID
    public int frontServerID;
    public bool rememberPW;
    public bool autoLogin;
}
