using UnityEngine;
using System.Collections.Generic;
using Tuio;

public class TowerBuilder : MonoBehaviour
{
    private List<Tuio.Touch> touches = new List<Tuio.Touch>();
    private float progress = 0;
    public Tower tower;

    /// <summary>
    /// Register a new touch on a TowerBuilder.
    /// </summary>
    /// <param name="touch">The touch object that should be registered.</param>
	public void RegisterTouch(Tuio.Touch touch)
    {
        touches.Add(touch); // Add the new touch to the list of touches on this TowerBuilder
    }
    
    /// <summary>
    /// Update the state of the builder (by calling helper functions)
    /// </summary>
    /// <remarks>
    /// Called once per frame.
    /// </remarks>
    void Update()
    {
        UpdateTouches();

		UpdatePosition();
 
        UpdateProgress();

    }
    
    /// <summary>
    /// Update the touches on the TowerBuilder.
    /// </summary>
    void UpdateTouches()
    {
        List<Tuio.Touch> delete = new List<Tuio.Touch>();
        
        // TODO: Refactor to Linq to make this easier and avoid creation of useless lists like "delete"
        foreach(var touch in touches) // Iterate through the active touches on this builder
        {
            if(touch.phase == TouchPhase.Ended) // If a touch as ended...
            {
                delete.Add(touch); // ...add it to the list of touches we need to delete.
            }
        }
        
        foreach(var touch in delete) // delete all touches that should be deleted.
        {
            touches.Remove(touch);
        }
    }
	
	void UpdatePosition()
	{
		if(touches.Count > 0)
		{
            RaycastHit hit;
            if(TowerPhysics.Raycast(touches[touches.Count-1], out hit, ~gameObject.layer & Physics.kDefaultRaycastLayers))
            {
                if(hit.collider.CompareTag("TowerGround"))
                {
                    transform.position = hit.point;
                }
            }
		}
	}
    
    /// <summary>
    /// Update the Progress of the tower, depending on if any touches are still on this TowerBuilder or not.
    /// </summary>
    void UpdateProgress()
    {
        progress += (touches.Count > 0 ? 1 : -1) * Time.deltaTime; // Update current progress:
            // If any touches are still on this Builder: Increase depending on elapsed time
            // If no more touches are on the builder: Decrease depending on elapsed time
        
        if(progress <= -.5f) // If progress falls below the threshold of -0.5...
        {
            Destroy(gameObject); // cancel build by destroying the Builder.
        }
        else if(progress >= 1) // If progress increases beyond the threshold of 1
        {
            Instantiate(tower, transform.position, Quaternion.identity); // Create "real" Tower...
            Destroy(gameObject); // ...and delete TowerBuilder
        }
    }
}
