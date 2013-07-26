using UnityEngine;
using System.Collections;

public class GUICircles
{
	public static void Progress(Rect rect, float progress, Texture2D empty, Texture2D full)
    {
        float angle = progress * 360f;
    
        GUI.BeginGroup(rect);
            GUI.DrawTexture(new Rect(0,0,rect.width,rect.height), empty);
            GUI.BeginGroup(new Rect(rect.width/2, 0, rect.width/2, rect.height));
                
                GUIUtility.RotateAroundPivot(angle, new Vector2(rect.width, rect.height));
                GUI.BeginGroup(rect);
                
                GUI.DrawTexture(rect, full);
                GUI.EndGroup();
                
            GUI.EndGroup();
        GUI.EndGroup();
	}
}
