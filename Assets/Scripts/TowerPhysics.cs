using UnityEngine;
using System.Collections;
using Tuio;

public class TowerPhysics : MonoBehaviour
{
	public static bool Raycast(Tuio.Touch touch, out RaycastHit hit, LayerMask raycastLayers)
    {
        Ray ray = getRay(touch);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayers);
	}
    
	public static bool Raycast(Tuio.Touch touch, out RaycastHit hit)
    {
        return Raycast(touch, out hit, ~0);
	}
	
	static Ray getRay(Tuio.Touch touch)
    {
		return Camera.main.ScreenPointToRay(touch.position);
	}
}
