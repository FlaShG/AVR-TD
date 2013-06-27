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
			Vector2 pos = TuioInput.GetTouch(i).position;
			var x = pos.x - icon.width / 2;
			var y = Screen.height - pos.y - icon.height / 2;
			GUI.DrawTexture(new Rect(x, y, icon.width, icon.height), icon);
			switch(touch.phase)
			{
				case TouchPhase.Ended:
					//if(!currentRadius){
						var newTower = Instantiate(towerTemplate, GetPosition(touch), Quaternion.identity) as GameObject;
						towers.Add(newTower);
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
	
	private Vector3 GetPosition(Tuio.Touch touch)
	{
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		//RaycastHit hit;
		float length = 0;
		if(new Plane(Vector3.up, Vector3.up * 20).Raycast(ray, out length))
		{
			//return hit.point;
			return ray.origin + ray.direction * length;
		}
		return Vector3.zero;
	}	
}