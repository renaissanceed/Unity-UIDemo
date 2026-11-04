using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    LoginData data;
    private void Awake()
    {
        UIManager.Instance.ShowPanel<BKPanel>();
        UIManager.Instance.ShowPanel<LoginPanel>();
    }
    private void Start()
    {
        data = LoginMgr.Instance.GetLoginData();
    }
    private void Update()
    {
        //print(data.frontServerID);
    }
}
