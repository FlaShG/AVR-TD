using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tuio;

public class TowerPlacement : MonoBehaviour
{
	public TowerBuilder towerBuilder;
	public Tower towerTemplate;
	public LayerMask layerMask;
	
	void Update()
	{
		var count = TuioInput.touchCount;
		for(int i = 0; i < count; ++i)
		{
			ProcessTouch(TuioInput.GetTouch(i));
        }
    }
    
    void ProcessTouch(Tuio.Touch touch)
    {
       	 RaycastHit hit;
		
		if(TowerPhysics.Raycast(touch, out hit))
        {
            TowerBuilder tb;
            
            if(hit.collider.CompareTag("TowerGround"))
            {
                tb = Instantiate(towerBuilder, hit.point, Quaternion.identity) as TowerBuilder;
                //tb = tbgo.GetComponent<TowerBuilder>();
                tb.tower = towerTemplate;
            }
            else
            {
                tb = hit.collider.GetComponent<TowerBuilder>();
            }
            
            if(tb)
            {
                tb.RegisterTouch(touch);
            }
        }
    }
}