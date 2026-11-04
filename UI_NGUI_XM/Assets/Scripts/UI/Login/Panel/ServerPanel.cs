using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerPanel : BasePanel<ServerPanel>
{
    //开始游戏按钮
    public UIButton btnBegin;
    //换区按钮
    public UIButton btnSelect;
    //服务器信息
    public UILabel labInfo;
    //返回上一级按钮
    public UIButton btnBack;
    public override void Init()
    {
        btnBegin.onClick.Add(new EventDelegate(() =>
        {
            //点击进入游戏了 存储一次登陆的数据
            LoginMar.Instance.SaveLoginData();
            LoginMar.Instance.SaveUserData(LoginMar.Instance.LoginData.userName
                + LoginMar.Instance.LoginData.passWord);
            SceneManager.LoadScene("001");
        }));

        btnSelect.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            ChooseServerPanel.Instance.ShowMe();
        }));

        btnBack.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            LoginPanel.Instance.ShowMe();
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        string userKey = LoginMar.Instance.LoginData.userName + LoginMar.Instance.LoginData.passWord;
        if (LoginMar.Instance.UserData.UserServerDic.ContainsKey(userKey))
        {
            int id = LoginMar.Instance.UserData.UserServerDic[userKey].frontServerID;
            Server info = LoginMar.Instance.ServerInfo.ServerDic[id];
            labInfo.text = info.name;
        }
        else
        {
            labInfo.text = "未选择服务器";
        }
    }
}
