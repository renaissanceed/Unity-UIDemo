using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutomGUI_Toggle : CustomGUIControl
{
    public bool isSel;

    public event UnityAction<bool> changeValue;

    private bool isOldSel;

    protected override void StyleOffDraw()
    {
        isSel =GUI.Toggle(guiPos.Pos , isSel ,content);
        //只有变化时才告诉 外部执行函数 否则没有必要一直告诉别人 同一个值 
        if (isOldSel != isSel)
        {
            changeValue?.Invoke(isSel);
            isOldSel = isSel;
        }
    }

    protected override void StyleOnDraw()
    {
        isSel = GUI.Toggle(guiPos.Pos, isSel, content ,style);
        if (isOldSel != isSel)
        {
            changeValue?.Invoke(isSel);
            isOldSel = isSel;
        }
    }
}
