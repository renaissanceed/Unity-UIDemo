using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllNP : MonoBehaviour
{
    void Start()
    {
        SerializerDictionary<string, string> registerInfo = LoginMar.Instance.registerData.registerInfo;
        foreach (KeyValuePair<string, string> item in registerInfo)
        {
            print("账号:" + item.Key +"密码:"+  item.Value);
        }
    }
    private void Update()
    {
        print(LoginMar.Instance.LoginData.frontServerID);
    }
}
