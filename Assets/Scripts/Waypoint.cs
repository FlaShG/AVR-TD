using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
    public Waypoint next;
    
	void Start()
    {
	
	}

    #if UNITY_EDITOR
	void OnDrawGizmos()
    {
        if(next)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
	}
    #endif
}
