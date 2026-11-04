using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutomGUI_Input : CustomGUIControl
{
    public event UnityAction<string> textChange;

    private string oldString = "";

    protected override void StyleOffDraw()
    {
        GUI.TextField(guiPos.Pos, content.text);
        if (oldString != content.text)
        {
            textChange?.Invoke(oldString);
            oldString = content.text;
        }
    }

    protected override void StyleOnDraw()
    {
        GUI.TextField(guiPos.Pos, content.text ,style);
        if (oldString != content.text)
        {
            textChange?.Invoke(oldString);
            oldString = content.text;
        }
    }
}
