using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

/// <summary>
/// 登陆管理器
/// </summary>
public class LoginMgr
{
    private static LoginMgr instance =  new LoginMgr();
    public static LoginMgr Instance => instance;

    //登陆数据
    private LoginData loginData;
    public LoginData LoginData => loginData;

    //注册数据
    private RegisterData registerData;
    public RegisterData RegisterData => registerData;

    //所有的服务器数据
    private List<ServerInfo> serverData = new List<ServerInfo>();
    public List<ServerInfo> ServerData => serverData;

    //单个账户数据
    private AccountServerData accountServer = new AccountServerData();
    public AccountServerData AccountServer => accountServer;

    private LoginMgr()
    {
        //直接通过json管理器 读取登陆数据
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");

        //读取注册数据
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");

        //读取服务器数据
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");

        //读取用户数据
        accountServer = JsonMgr.Instance.LoadData<AccountServerData>("AccountServerData");
    }

    #region 登陆数据相关
    //存储登陆数据相关
    public void SaveLoginData()
    {
        JsonMgr.Instance.SaveData(loginData,"LoginData");
    }

    #endregion

    #region 注册数据相关
    //储存注册数据相关
    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "RegisterData");
    }

    //注册方法
    public bool RegisterUser(string userName,string password)
    {
        //说明已经存在用户
        if(registerData.registerInfo.ContainsKey(userName))
        {
            return false;
        }

        //不存在 说明可以注册
        registerData.registerInfo.Add(userName, password);
        //存储进json
        SaveRegisterData();
        return true;
    }

    //验证用户密码是否合法
    public bool CheckInfo(string userName, string password)
    {
        //判断用户名是否存在
        if (registerData.registerInfo.ContainsKey(userName))
        {
            //相等 密码相同 登陆成功
            if (registerData.registerInfo[userName] == password)
            {
                return true;
            }
        }
        //用户名 和 密码不合法
        return false;
    }

    //注册成功后 应该清理数据
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
        loginData.autoLogin = false;
        loginData.rememberPW = false;
    }
    #endregion

    #region 用户数据相关
    //保存单个用户数据
    public void SaveAccountData()
    {
        JsonMgr.Instance.SaveData(accountServer, "AccountServerData");
    }

    //给外部提供一个保存单个用户的数据
    public void SaveSingleUser(string user, LoginData data)
    {
        //拷贝现有的账户信息
        LoginData copy = data.Clone();
        //如果用户存在 就覆盖它
        if (accountServer.accountServer.ContainsKey(user))
        {
            accountServer.accountServer[user] = copy;
            //存储进json
            SaveAccountData();
            return;
        }

        //如果不存在 就添加进字典里面
        accountServer.accountServer.Add(user, copy);
        //存储进json
        SaveAccountData();
    }

    //提供给外部 得到当前账户的 LoginData
    public LoginData GetLoginData()
    {
        LoginPanel loginPanel = UIManager.Instance.GetPanel<LoginPanel>();
        if (accountServer.accountServer.ContainsKey(LoginData.userName))
        {
            return accountServer.accountServer[LoginData.userName];
        }

        return null;
    }

    /// <summary>
    /// 切换当前登录账号，从字典中加载该账号的历史数据
    /// </summary>
    public void SwitchAccount(string userName)
    {
        if (accountServer.accountServer.ContainsKey(userName))
        {
            // 从字典中拷贝一份数据到当前 loginData
            LoginData saved = accountServer.accountServer[userName];
            loginData.userName = saved.userName;
            loginData.password = saved.password;
            loginData.rememberPW = saved.rememberPW;
            loginData.autoLogin = saved.autoLogin;
            loginData.frontServerID = saved.frontServerID;
        }
        else
        {
            // 新账号，重置所有数据，避免残留上一个账号的状态
            loginData.userName = userName;
            loginData.password = "";
            loginData.frontServerID = 0;
            loginData.rememberPW = false;
            loginData.autoLogin = false;
        }
    }
    #endregion
}
