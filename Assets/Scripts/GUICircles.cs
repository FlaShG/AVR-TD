using UnityEngine;
using System.Collections;

public class GUICircles
{
	public static void Progress(Rect rect, float progress, Texture2D empty, Texture2D full)
    {
        float angle = progress * 360f;
        Vector2 halfsize = new Vector2(rect.width, rect.height) / 2;
    
        GUI.BeginGroup(rect);
            GUI.DrawTexture(new Rect(0,0,rect.width,rect.height), empty);
            GUI.BeginGroup(new Rect(rect.width/2, 0, rect.width/2, rect.height));
                
                var matrix = GUI.matrix;
                //GUI.BeginGroup(new Rect(0,0,rect.width,rect.height));
                    GUIUtility.RotateAroundPivot(angle, new Vector2(0, halfsize.y));
                    GUI.DrawTexture(new Rect(-halfsize.x, 0, rect.width, rect.height), full);
                 //   GUIUtility.RotateAroundPivot(-angle, new Vector2(0, halfsize.y));
                //GUI.EndGroup();
                
                GUI.matrix = matrix;
                
            GUI.EndGroup();
            GUI.matrix = matrix;
        GUI.EndGroup();
	}
}
