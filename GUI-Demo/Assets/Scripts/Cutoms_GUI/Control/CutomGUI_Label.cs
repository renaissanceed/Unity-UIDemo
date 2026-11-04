using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutomGUI_Label : CustomGUIControl
{
    protected override void StyleOffDraw()
    {
        GUI.Label(guiPos.Pos, content); 
    }

    protected override void StyleOnDraw()
    {
        GUI.Label(guiPos.Pos, content ,style);
    }
}
