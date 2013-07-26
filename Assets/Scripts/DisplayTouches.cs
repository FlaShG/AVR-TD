using UnityEngine;
using Tuio;

public class DisplayTouches : MonoBehaviour
{
	public Texture2D texture;
    
	void OnGUI()
    {
        Vector2 size = new Vector2(texture.width, texture.height);
        
        for(int i = 0; i < TuioInput.touchCount; ++i)
        {
            Tuio.Touch touch = TuioInput.GetTouch(i);
            
            GUI.DrawTexture(new Rect(touch.position.x - size.x/2, touch.position.y - size.y/2, size.x, size.y), texture);
        }
	}
}
