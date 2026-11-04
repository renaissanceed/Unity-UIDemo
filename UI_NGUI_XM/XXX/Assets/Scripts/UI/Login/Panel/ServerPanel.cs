public override void ShowMe()
{
    base.ShowMe();
    //在这里更新 服务器信息
    //根据玩家上一次服务器ID 得到数据 进行更新
    Server server = LoginMar.Instance.ServerInfo.ServerDic[
        LoginMar.Instance.UserData.UserServerDic[
        LoginMar.Instance.LoginData.userName +
        LoginMar.Instance.LoginData.passWord
        ].frontServerID];
    labInfo.text = server.name;
}