using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class Tower : MonoBehaviour
{
    private List<Entity> enemiesInRange = new List<Entity>();
    
    public float damagePerShot = 10;
    public float shootDelay = 1;
    private float shootDelayLeft = 0;
    [SerializeField]
    private ShotAnimation anim;
    

	void Update()
    {
        if(shootDelayLeft > 0)
        {
            shootDelayLeft -= Time.deltaTime;
        }
        else
        {
            Shoot();
        }
	}
    
    void Shoot()
    {
        Entity target = null;
        float distance = Mathf.Infinity;
        
        foreach(var e in enemiesInRange)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if(dist < distance)
            {
                target = e;
                distance = dist;
            }
        }
        
        if(target)
        {
            ShotAnimation sa = Instantiate(anim,
                                           transform.position,
                                           Quaternion.LookRotation(target.transform.position - transform.position)) as ShotAnimation;
                                           
            sa.Init(distance);
            
            target.ApplyDamage(damagePerShot);
            shootDelayLeft = shootDelay;
        }
    }

	void OnTriggerEnter(Collider other)
    {
        print("da isser");
        if(other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.GetComponent<Entity>());
        }
	}
    
	void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.GetComponent<Entity>());
        }
	}
}
