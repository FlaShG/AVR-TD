using UnityEngine;
using System.Collections;
using Tuio;

public class TowerPhysics : MonoBehaviour
{
	public static bool Raycast(Tuio.Touch touch, out RaycastHit hit, LayerMask raycastLayers)
    {
        return Raycast(touch.position, out hit, raycastLayers);
	}
    
	public static bool Raycast(Tuio.Touch touch, out RaycastHit hit)
    {
        return Raycast(touch.position, out hit, Physics.kDefaultRaycastLayers);
	}
    
	public static bool Raycast(Vector2 position, out RaycastHit hit, LayerMask raycastLayers)
    {
        Ray ray = getRay(position);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayers);
	}
	
	static Ray getRay(Vector2 position)
    {
		return Camera.main.ScreenPointToRay(position);
	}
}
