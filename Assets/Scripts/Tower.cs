using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class Tower : MonoBehaviour
{
    private List<Entity> enemiesInRange = new List<Entity>();
    
    public float damagePerShot = 10;
    public float shootDelay = 1;
    private float shootDelayLeft = 0;
    [SerializeField]
    private ShotAnimation anim;
    
    /// <summary>
    /// See if the tower can shoot, and shoot if any enemy is in range and the timer has elapsed.
    /// </summary>
    /// <remarks>
    /// Called on every frame.
    /// </remarks>
	void Update()
    {
        if(shootDelayLeft > 0) // If the shootDelay has not expired...
        {
            shootDelayLeft -= Time.deltaTime; // ...update it with the elapsed time
        }
        else // If the timer has elapsed...
        {
            Shoot(); // ...try to shoot the nearest enemy.
        }
	}
    
    /// <summary>
    /// Shoot the closest enemy, if any enemies are in range (if not, do nothing).
    /// </summary>
    void Shoot()
    {
        Entity target = null; // Contains the closest enemy
        float distance = Mathf.Infinity; // Distance to selected enemy
        
        bool containsNull = false; // Indicates if any enemy in the list has been killed (which would be removed later)
        foreach(var e in enemiesInRange) // Iterate through all enemies in range
        {
            if(e) // If the enemy is still alive (not null)...
            {
                float dist = Vector3.Distance(transform.position, e.transform.position); // ...get distance to enemy
                if(dist < distance) // If it is closer than the currently selected enemy...
                {
                    target = e; // ...update currently selected target and distance
                    distance = dist;
                }
            }
            else // If the enemy is null (dead)...
            {
                containsNull = true; // ...indicate that it should be removed later
            }
        }
        
        if(containsNull) // If any enemy was killed...
        {
            enemiesInRange = enemiesInRange.Where(x => x != null).ToList(); // ...remove it from the list
        }
        
        if(target) // If we have selected a target
        {
            // Play shot animation
            ShotAnimation sa = Instantiate(anim,
                                           transform.position,
                                           Quaternion.LookRotation(target.transform.position - transform.position)) as ShotAnimation;
                                           
            sa.Init(distance);
            
            // Apply damage, reset shot delay to maximum
            target.ApplyDamage(damagePerShot);
            shootDelayLeft = shootDelay;
        }
    }

    /// <summary>
    /// Updates the list of enemies in range when enemies enter the range of the tower.
    /// </summary>
    /// <param name="other">Collider that entered the range of the tower</param>
    /// <remarks>
    /// Called when a collider enters into range of the tower.
    /// </remarks>
	void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")) // If the collider is an enemy...
        {
            enemiesInRange.Add(other.GetComponent<Entity>()); // ...add it to the list of enemies in range
        }
	}

    /// <summary>
    /// Updates the list of enemies in range when enemies leave the range of the tower.
    /// </summary>
    /// <param name="other">Collider that left the range of the tower</param>
    /// <remarks>
    /// Called when a collider leaves into range of the tower.
    /// </remarks>
	void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy")) // If the collider is an enemy...
        {
            enemiesInRange.Remove(other.GetComponent<Entity>()); // ...remove it from the list of enemies in range
        }
	}
}
