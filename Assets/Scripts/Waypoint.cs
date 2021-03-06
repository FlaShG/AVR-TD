using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
    private static List<Waypoint> starts = new List<Waypoint>();

    public Waypoint next;
    [SerializeField]
    private bool start = false;
    
	void Awake()
    {
        if(start)
        {
            starts.Add(this);
        }
	}
    
    void OnDestroy()
    {
        if(start)
        {
            starts.Remove(this);
        }
    }
    
    public static Waypoint GetRandomStart()
    {
        return starts[Random.Range(0,starts.Count)];
    }

    #if UNITY_EDITOR
	void OnDrawGizmos()
    {
        if(next)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + Vector3.up, next.transform.position);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
        }
	}
    #endif
}
