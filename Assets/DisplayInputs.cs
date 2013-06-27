using UnityEngine;
using System.Collections;
using Tuio;

public class DisplayInputs : MonoBehaviour
{
	public Texture2D icon;
	
	void OnGUI()
	{
		for(int i = 0; i < TuioInput.touchCount; ++i)
		{
			Vector2 pos = TuioInput.GetTouch(i).position;
			GUI.DrawTexture(new Rect(pos.x-icon.width/2, Screen.height-pos.y-icon.height/2, icon.width, icon.height), icon);
		}
	}
}
