using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private Transform me;
    private Waypoint nextWaypoint;
    
    public float speed = 3;
    public float steer = 360;
    
    void Awake()
    {
        me = transform;
    }

	void Start()
    {
        Waypoint start = Waypoint.GetRandomStart();
        
        me.position = start.transform.position;
        
        nextWaypoint = start.next;
        
        me.rotation = GetTargetRotation();
	}
	
    void Update()
    {
        me.position = Vector3.MoveTowards(me.position, me.position + me.forward, speed * Time.deltaTime);
        
        me.rotation = Quaternion.RotateTowards(me.rotation, GetTargetRotation(), steer * Time.deltaTime);
        
        if(Vector3.Distance(me.position, nextWaypoint.transform.position) < 0.1f)
        {
            var next = nextWaypoint.next;
            if(next)
            {
                nextWaypoint = next;
            }
            else
            {
                //lost a life!
                Destroy(gameObject);
            }
        }
	}
    
    Quaternion GetTargetRotation()
    {
        return Quaternion.LookRotation(nextWaypoint.transform.position - me.position);
    }
}
