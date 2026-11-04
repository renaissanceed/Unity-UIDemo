using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 服务器相关数据
/// </summary>
public class ServerInfo
{
    //一个服务器ID 对应一个服务器数据
    public SerializerDictionary<int, Server> ServerDic = new SerializerDictionary<int, Server>();

}
/// <summary>
/// 单个服务器信息
/// </summary>
public class Server
{
    [XmlAttribute]
    public int id; //服务器id
    [XmlAttribute]
    public string name;//服务器名字
    [XmlAttribute]
    public int state;//服务器状态 
    [XmlAttribute]
    public bool isNew;//是否为新服
}
