using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tuio;

public class TowerPlacement : MonoBehaviour
{
	public Texture2D icon;
	public List<GameObject> towers = new List<GameObject>();
	public GameObject towerTemplate;
	public LayerMask layerMask;
	public Stopwatch watch = new Stopwatch();
	public TimeSpan requiredDownTime = TimeSpan.FromSeconds(1);
	float percentageSure = 0f;
	
	void OnGUI()
	{
		for(int i = 0; i < TuioInput.touchCount; ++i)
		{
			var touch = TuioInput.GetTouch(i);
			Vector3 position;
			percentageSure = (float)(watch.Elapsed.TotalMilliseconds / requiredDownTime.TotalMilliseconds);
			print(touch.phase + " " + watch.Elapsed + " -> " + percentageSure + "%");
			showTouchCircle(touch.position, percentageSure);
			
			switch(touch.phase)
			{
				case TouchPhase.Ended:
					var timeIsUp = watch.Elapsed > requiredDownTime;
					watch.Reset();				
					if(timeIsUp)
					{
						if(GetPosition(touch, out position)) {
							var newTower = Instantiate(towerTemplate, position, Quaternion.identity) as GameObject;
							towers.Add(newTower);
						}
					}
					break;
				case TouchPhase.Moved:
				case TouchPhase.Began:
					if(!watch.IsRunning) { watch.Reset(); watch.Start(); }
					break;
			}
		}
	}
	
	void showTouchCircle(Vector2 pos, float percentageSure) {
		percentageSure = Mathf.Clamp(0.1f, percentageSure, 1.5f);
		var w = icon.width * percentageSure;
		var h = icon.height * percentageSure;
		var x = pos.x - w / 2f;
		var y = Screen.height - pos.y - h / 2f;
		GUI.DrawTexture(new Rect(x, y, w, h), icon);
	}
	
	bool GetPosition(Tuio.Touch touch, out Vector3 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
		{
			position = hit.point;
			return true;
		}
		position = Vector2.zero;
		return false;
	}	
}