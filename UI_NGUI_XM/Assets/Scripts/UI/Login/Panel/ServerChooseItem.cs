using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerChooseItem : MonoBehaviour
{
    public UIButton btn;
    public UISprite sprNew;
    public UISprite sprState;
    public UILabel labName;

    //当前这个按钮 代表 单个服务器的信息 之后用于传给服务器面板
    private Server nowInfo;
    void Start()
    {
        btn.onClick.Add(new EventDelegate(() =>
        {
            //记录当前玩家 选择的服务器id
            LoginMar.Instance.LoginData.frontServerID = nowInfo.id;
            string userKey = LoginMar.Instance.LoginData.userName + LoginMar.Instance.LoginData.passWord;
            if (!LoginMar.Instance.UserData.UserServerDic.ContainsKey(userKey))
            {
                LoginMar.Instance.UserData.UserServerDic.Add(userKey, new LoginData());
            }
            LoginMar.Instance.UserData.UserServerDic[userKey].frontServerID = nowInfo.id;
            //记录当前玩家 选择的服务器名字
            ChooseServerPanel.Instance.HideMe();
            ServerPanel.Instance.ShowMe();
        }));
    }
    
    public void InitInfo(Server info)
    {
        //记录传入的信息
        nowInfo = info;

        //根据传入的单个服务器数据 进行更新

        //名字
        labName.text = info.id + "区  " + info.name;
        //是否为新服
        sprNew.gameObject.SetActive(info.isNew);
        //状态
        sprState.gameObject.SetActive(true);
        switch (info.state)
        {
            case 0:
                sprState.gameObject.SetActive(false);
                break;
            case 1:
                sprState.spriteName = "ui_DL_liuchang_01";
                break;
            case 2:
                sprState.spriteName = "ui_DL_fanhua_01";
                break;
            case 3:
                sprState.spriteName = "ui_DL_huobao_01";
                break;
            case 4:
                sprState.spriteName = "ui_DL_weihu_01";
                break;
        }

    }
}
