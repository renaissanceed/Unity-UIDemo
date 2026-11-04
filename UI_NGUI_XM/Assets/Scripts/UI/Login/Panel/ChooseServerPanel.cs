using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseServerPanel : BasePanel<ChooseServerPanel>
{
    //左右两个滚动视图位置信息
    public UIScrollView svLeft, svRight;
    //上次登录的服务器名
    public UILabel labName;
    //登陆的状态
    public UISprite sprState;
    //当前选择的服务器区间文字
    public UILabel labNowServer;
    //关闭按钮
    public UIButton btnBack;
    //记录创建出来的单个服务器对象
    private List<GameObject> itemList = new List<GameObject>(); 

    public override void Init()
    {
        //左侧服务器数据 进行游戏的时候 是不会改变的 所有我们可以 一进去就初始化它
        //动态创建左侧按钮
        ServerInfo info = LoginMar.Instance.ServerInfo;
        //得到循环几次 按12区一间隔区分
        int num = info.ServerDic.Count / 12 + 1;
        for (int i = 0; i < num; i++)
        {
            //创建按钮
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/btnServer"));
            //设置父对象 设置位置
            obj.transform.SetParent(svLeft.transform, false);
            obj.transform.localPosition = new Vector3(-73,71 + -i * 60,0);
            //得到脚本  进行按钮信息初始化
            ServerItem serverItem = obj.GetComponent<ServerItem>();
            //得到当前 按钮的代表的服务器区间
            int beginIndex = 12 * i + 1;
            int endIndex = 12 * (i + 1) ;
            //判断最大是否 超过了服务器总数
            if(endIndex > info.ServerDic.Count)
            {
                endIndex = info.ServerDic.Count;
            }
            serverItem.InitInfo(beginIndex,endIndex);
        }
        //退出键 按下逻辑
        btnBack.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            if(LoginMar.Instance.LoginData.frontServerID == 0)
            {
                LoginPanel.Instance.ShowMe();
            }
            else
            {
                ServerPanel.Instance.ShowMe();
            }
        }));
        HideMe();
    }

    //提供一个方法给外部 用于更新面板右侧显示内容
    public void UpdataPanel(int beginIndex,int endIndex)
    {
        //更新当前选择的服务器显示
        labNowServer.text = "服务器 " + beginIndex + "—" + endIndex + "区";

        //创建新按钮之前 应该删除老的
        for (int i = 0; i < itemList.Count; i++)
        {
            //删除依附的对象
            Destroy(itemList[i].gameObject);
        }
        //删除后清空列表 需要手动
        itemList.Clear();

        //动态创建 新按钮
        //自我优化逻辑:先创建出来12个对象到List中 每次需要更改 就改变它的信息
        //如果需要增删 就进行判断数量 进行增删改查
        Server nowInfo;
        for (int i = beginIndex; i <= endIndex; i++)
        {
            //获取单个服务器数据
            nowInfo = LoginMar.Instance.ServerInfo.ServerDic[i];
            //动态创建按钮
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/btnChooseServer"));
            obj.transform.SetParent(svRight.transform,false);
            obj.transform.localPosition = new Vector3(101 , 13, 0 ) + new Vector3((i - 1) %12 %2 * 298,  (i - 1) % 12 / 2 * -76, 0);
            //得到按钮上的脚本 根据服务器信息 更新内容
            ServerChooseItem chooseItem = obj.GetComponent<ServerChooseItem>();
            chooseItem.InitInfo(nowInfo);
            //创建成功后 记录到列表中 下次删除
            itemList.Add(obj);
        }
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //初始化面板显示内容
        //初始化上一次选择的内容
        if (LoginMar.Instance.LoginData.frontServerID == 0)
        {
            labName.text = "无";
            sprState.gameObject.SetActive(false);
        }
        else
        {   
            //得到单个服务器信息 然后去显示
            Server info = LoginMar.Instance.ServerInfo.ServerDic[LoginMar.Instance.LoginData.frontServerID];
            //显示上一次服务器名字
            //labName.text = info.id + "区  "+info.name;
            labName.text = ServerPanel.Instance.labInfo.text;
            //根据状态改变服务器显示状态
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

        //更新右侧按钮显示
        //最大索引 需要判断
        UpdataPanel(SelectRightServerID.begin, SelectRightServerID.end > LoginMar.Instance.ServerInfo.ServerDic.Count ? LoginMar.Instance.ServerInfo.ServerDic.Count : SelectRightServerID.end);
    }
}
