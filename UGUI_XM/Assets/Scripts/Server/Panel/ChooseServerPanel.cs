using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ChooseServerPanel : BasePanel
{
    //左边视图
    public ScrollRect svLeft;
    //右边视图
    public ScrollRect svRight;

    //上次选择的服务器
    public Text txtFrontServer;
    //上次选择服务器的状态
    public Image imgState;

    //服务器范围
    public Text txtRange;
    //由于存储右侧按钮
    private List<GameObject> itemList = new List<GameObject>();

    //返回按钮
    public Button btnBack;
    public override void Init()
    {
        //动态创建左侧的 区间按钮
        //获取服务器列表数据
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;

        //得到一共循环创建多少个区间按钮
        //由于向下取整 我们+1 就代表平均分成num 个 按钮
        int num = infoList.Count / 15 + 1;

        //循环创建一个一个按钮
        for (int i = 0; i < num; i++)
        {
            //动态创建按钮
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/ServerLeftItem"));
            item.transform.SetParent(svLeft.content,false);
            //初始化 判断最大是不是超过了 服务器的总数
            item.GetComponent<ServerLeftItem>().InitInfo(i * 15 + 1, 15 * (i + 1) > infoList.Count ? infoList.Count : 15 * (i + 1));  
        }

        //返回按钮逻辑
        btnBack.onClick.AddListener(() =>
        {
            //如果选择过服务器就 打开登录面板
            if (LoginMgr.Instance.GetLoginData().frontServerID > 0)
            {
                //打开服务器相关面板
                UIManager.Instance.ShowPanel<ServerPanel>();
            }
            else
            {
                //打开登录面板
                UIManager.Instance.ShowPanel<LoginPanel>();
            }

            //隐藏自己
            UIManager.Instance.HidePanel<ChooseServerPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();

        //显示自己的时候 应该初始化上一次选择的服务器 更新当前选择的
        int id = LoginMgr.Instance.GetLoginData().frontServerID;
        if ( id <= 0)
        {
            txtFrontServer.text = "无"; 
            imgState.gameObject.SetActive(false);
        }
        else
        {
            //根据上一次登录的id 获取到 服务器信息 用于界面数据更新
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            //显示上一次服务器名
            txtFrontServer.text =  info.name;
            //改变状态
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

        //更新当前区间
        UpdatePanel(1,15 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count :15);
    }

    //提供给外部 用于更新当前选择区间的右侧按钮方法
    public void UpdatePanel(int begin,int end)
    {
        //更新服务器选择区间
        txtRange.text = "服务器 "+ begin + "—" + end + "区";

        //先删除之前的对象
        for (int i = 0; i < itemList.Count; i++)
        {
            Destroy(itemList[i]);
        }

        //删除完成后 一定要清空它
        itemList.Clear();

        //创建新按钮
        for (int i = begin; i <= end; i++)
        {
            //获取服务器信息
            ServerInfo info = LoginMgr.Instance.ServerData[i - 1];

            //动态创建 
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/ServerRightItem"));
            serverItem.transform.SetParent(svRight.content,false);
            //更新信息
            serverItem.GetComponent<ServerRightItem>().InitInfo(info);

            //创建成功后 需要记录到列表中
            itemList.Add(serverItem);
        }   
    }
}
