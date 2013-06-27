using UnityEngine;
using System.Collections;


public class ShotAnimationLaser : ShotAnimation
{
    [SerializeField]
    private LineRenderer line;

	public override void Init(float distance)
    {
        line.SetPosition(1, Vector3.forward * distance);
        
        Destroy(gameObject, 0.1f);
    }
}
