using UnityEngine;
using System.Collections;
using Tuio;

public class TowerPhysics : MonoBehaviour {

	// Use this for initialization
	void Start () {


		
		
		
	}
	public static bool Raycast(Tuio.Touch touch, out RaycastHit hit){
			
			Ray ray = getRay(touch);
			return Physics.Raycast(ray, out hit, Mathf.Infinity);
	}
	
	static Ray getRay(Tuio.Touch touch){
		return Camera.main.ScreenPointToRay(touch.position);
	
	}
	// Update is called once per frame
	void Update () {
	
	}
}
