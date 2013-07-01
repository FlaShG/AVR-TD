using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private Transform me;
    private Waypoint nextWaypoint;
    
    public float speed = 3;
    [RangeAttribute(50,200)]
    public float steer = 120;
    [RangeAttribute(1,10)]
    public int damageToBase = 1;
    
    
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
        float distance = Vector3.Distance(me.position, nextWaypoint.transform.position);
        var next = nextWaypoint.next;
    
        float travelDistance = speed * Time.deltaTime;
        if(travelDistance >= distance)
        {
            me.position = nextWaypoint.transform.position;
            if(!next)
            {
                BaseHealth.ApplyDamage(damageToBase);
                Destroy(gameObject);            
            }
        }
        else
        {
            me.position = Vector3.MoveTowards(me.position, me.position + me.forward, travelDistance);
            me.rotation = Quaternion.RotateTowards(me.rotation, GetTargetRotation(), steer * Time.deltaTime);
        }
        
        if(next)
        {
            float angle = Vector3.Angle(me.forward, next.transform.position - me.position);
            if(distance < speed / steer * angle * 0.6f)
            {
                nextWaypoint = next;
            }
        }
        

	}
    
    Quaternion GetTargetRotation()
    {
        return Quaternion.LookRotation(nextWaypoint.transform.position - me.position);
    }
}
