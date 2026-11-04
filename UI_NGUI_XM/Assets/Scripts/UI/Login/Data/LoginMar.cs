using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

/// <summary>
/// 登陆管理器
/// </summary>
public class LoginMar
{
    private static LoginMar instance = new LoginMar();
    public static LoginMar Instance => instance;
    //登录面板记录的玩家数据
    private LoginData loginData;
    //为了安全 保护loginData数据 只能得 不能改
    public LoginData LoginData => loginData;
    //注册数据
    public RegisterData registerData;
    //为了安全 保护RegisterData数据 只能得 不能改
    public RegisterData RegisterData => registerData;
    //服务器数据
    private ServerInfo serverInfo;
    //方便外部获取 为了安全 只能去得到
    public ServerInfo ServerInfo => serverInfo;
    //单个用户的数据
    private UserData userData;
    //方便外部获取 为了安全 只能去得到
    public UserData UserData => userData;
    private LoginMar()
    {
        //获取注册面板数据 从Xml中
        registerData = XmlDataMar.Instacne.LoadData(typeof(RegisterData),"RegisterData") as RegisterData;
        //获取服务器数据
        serverInfo = XmlDataMar.Instacne.LoadData(typeof(ServerInfo), "ServerInfo") as ServerInfo;
        //获取登录面板数据 从Xml中
        loginData = XmlDataMar.Instacne.LoadData(typeof(LoginData),"LoginData") as LoginData;
    }

    //提供给外部进行获取个人服务器数据
    public void GetUserData(string name)
    {
        //获取单个用户的服务器数据 从Xml中
        userData = XmlDataMar.Instacne.LoadData(typeof(UserData), name) as UserData;
    }
    //提供给外部进行保存用户数据
    public void SaveUserData(string name)
    {
        XmlDataMar.Instacne.SavaData(userData, name);
    }

    //提供给外部存储数据的方法
    public void SaveLoginData()
    {
        XmlDataMar.Instacne.SavaData(loginData, "LoginData");
    }
    //提供给外部清除数据
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
    }

    //提供给外部存储 注册数据的方法
    public void SaveRegisterData()
    {
        XmlDataMar.Instacne.SavaData(registerData,"RegisterData");
    }

    //提供给外部注册的方法
    public bool RegisterUser(string user,string passWord)
    {
        //判断是否有重复的用户名
        if(registerData.registerInfo.ContainsKey(user))
        {
            return false;
        }
        //没有就保存信息
        registerData.registerInfo.Add(user, passWord);
        SaveRegisterData();

        return true;
    }

    //检查用户名和密码是否合法
    public bool CheckInfo(string name,string passWord)
    {
        if (registerData.registerInfo.ContainsKey(name))
        {
            if (registerData.registerInfo[name] == passWord)
            {
                return true;
            }
        }
        return false;
    }
}
