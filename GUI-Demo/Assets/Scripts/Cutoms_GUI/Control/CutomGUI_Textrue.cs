using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutomGUI_Textrue : CustomGUIControl
{
    //图片绘制的缩放模式
    public ScaleMode scalMode =  ScaleMode.StretchToFill;

    protected override void StyleOffDraw()
    {
        GUI.DrawTexture(guiPos.Pos, content.image, scalMode);
    }

    protected override void StyleOnDraw()
    {
        GUI.DrawTexture(guiPos.Pos, content.image, scalMode);
    }
}
