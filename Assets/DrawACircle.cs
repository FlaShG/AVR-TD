using UnityEngine;
using System.Collections;

public class DrawACircle : MonoBehaviour
{
    public float progress;
    public Texture2D empty;
    public Texture2D full;

	void OnGUI()
    {
        GUICircles.Progress(new Rect(10,10,400,400), progress, empty, full);
	}
}
