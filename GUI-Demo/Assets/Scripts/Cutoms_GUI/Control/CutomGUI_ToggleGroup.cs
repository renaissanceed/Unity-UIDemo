using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutomGUI_ToggleGroup : MonoBehaviour
{
    public CutomGUI_Toggle[] toggles;

    //记录上一次为ture的toggle
    private CutomGUI_Toggle frontTog;

    private void Start()
    {
        if (toggles.Length == 0)
        {
            return;
        }

        //通过遍历 来为多个 多选框 添加 监听事件函数 
        //在函数中处理
        //当一个为true时 另外两个变成false
        for (int i = 0; i < toggles.Length; i++)
        {
            CutomGUI_Toggle toggle = toggles[i];
            toggle.changeValue += (value) =>
            {
                //当传入的Value是true时 需要把另外俩变成 false
                if (value)
                {
                    //意味着另外俩变成false
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        //注意有闭包 toggle是上一个函数的变量 我改变了它的生命周期
                        if (toggles[j] != toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    //记录上一次为true的toggle
                    frontTog = toggle;
                }
                //判断 当前变成false的toggle是不是上一次为true的
                //如果是 就不应该让它变成false
                else if (toggle == frontTog) 
                {
                    //强制改成true
                    toggle.isSel = true;
                }

            };
        }
    }
}
