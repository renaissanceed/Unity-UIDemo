using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem : MonoBehaviour
{
    //按钮自己
    public Button btnSelf;
    //是否为新服
    public Image imgNew;
    //单个服务器状态
    public Image imgState;
    //服务器名
    public Text txtName;

    //当前按钮代表哪个服务器
    public ServerInfo nowServerInfo;

    void Start()
    {
        //按钮逻辑
        btnSelf.onClick.AddListener(() =>
        {
            //服务器在维护中的 不可以选择
            if (nowServerInfo.state == 4)
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("服务器维护中！");
                return;
            } 
            //记录当前选择的服务器
            LoginMgr.Instance.LoginData.frontServerID = nowServerInfo.id;
            //同步保存到账号字典中，防止切换账号后数据丢失
            LoginMgr.Instance.SaveSingleUser(LoginMgr.Instance.LoginData.userName, LoginMgr.Instance.LoginData);
            //隐藏面板
            UIManager.Instance.HidePanel<ChooseServerPanel>();
            //显示服务器面板
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
    }

    //初始化方法 用于更新按钮显示相关
    public void InitInfo(ServerInfo info)
    {
        //记录数据
        nowServerInfo = info;
        //更新数据
        txtName.text = info.name;
        //是否为新服
        imgNew.gameObject.SetActive(info.isNew);
        //一开让状态图显示
        imgState.gameObject.SetActive(true);
        //状态
        //加载图集
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Login");
        switch (info.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1:
                //流畅状态
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_liuchang_01");
                break;
            case 2:
                //火爆状态
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_huobao_01");
                break;
            case 3:
                //繁忙状态
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_fanhua_01");
                break;
            case 4:
                //维护状态
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_weihu_01");
                break;
        }
    }
}
