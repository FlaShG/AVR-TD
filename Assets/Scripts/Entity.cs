using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private float health = 100;
	
    /// <summary>
    /// Apply damage to the entity.
    /// </summary>
    /// <param name="amount">Amount of damage to apply</param>
    public void ApplyDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }
    
    /// <summary>
    /// Death of the entity.
    /// </summary>
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
