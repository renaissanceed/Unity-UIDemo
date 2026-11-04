using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个用户存储的服务器数据类
/// </summary>
public class UserData
{
    public SerializerDictionary<string, LoginData> UserServerDic= new SerializerDictionary<string, LoginData>();
}
