using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private float health = 100;
	
    public void ApplyDamage(float amount)
    {
        health -= amount;
    }
}
