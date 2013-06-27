using UnityEngine;
using System.Collections.Generic;
using Tuio;

public class TowerPlacement : MonoBehaviour
{
	public Texture2D icon;
	public List<GameObject> towers = new List<GameObject>();
	public GameObject towerTemplate;
	
	void OnGUI()
	{
		for(int i = 0; i < TuioInput.touchCount; ++i)
		{
			var touch = TuioInput.GetTouch(i);
			showTouchCircle(touch.position);
			Vector3 position;
			switch(touch.phase)
			{
				case TouchPhase.Ended:
					//if(!currentRadius){
					if(GetPosition(touch, out position))
					{
						var newTower = Instantiate(towerTemplate, position, Quaternion.identity) as GameObject;
						towers.Add(newTower);
					}
						//currentRadius = ( as GameObject).GetComponent<Radius>();
					//}
					break;
				case TouchPhase.Began:
					/*if(currentRadius && Vector3.Distance(currentRadius.transform.position, GetPosition(touch)) < 77)
					{
						currentRadius.Done();
						currentRadius = null;
					}*/
					break;
			}
		}
	}
	
	void showTouchCircle(Vector2 pos) {
		var x = pos.x - icon.width / 2;
		var y = Screen.height - pos.y - icon.height / 2;
		GUI.DrawTexture(new Rect(x, y, icon.width, icon.height), icon);
	}
	
	bool GetPosition(Tuio.Touch touch, out Vector3 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		//RaycastHit hit;
		float length = 0;
		if(new Plane(Vector3.up, Vector3.up * 20).Raycast(ray, out length))
		{
			//return hit.point;
			position = ray.origin + ray.direction * length;
			return true;
		}
		position = Vector2.zero;
		return false;
	}	
}