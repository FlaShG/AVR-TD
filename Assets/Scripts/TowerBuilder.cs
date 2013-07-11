using UnityEngine;
using System.Collections.Generic;
using Tuio;

public class TowerBuilder : MonoBehaviour
{
    private List<Tuio.Touch> touches = new List<Tuio.Touch>();
    private float process = 0;
    public Tower tower;

    
	public void RegisterTouch(Tuio.Touch touch)
    {
        touches.Add(touch);
    }
    
    void Update()
    {
        UpdateTouches();
        UpdateProcess();
    }
    
    void UpdateTouches()
    {
        List<Tuio.Touch> delete = new List<Tuio.Touch>();
        
        foreach(var touch in touches)
        {
            if(touch.phase == TouchPhase.Ended)
            {
                delete.Add(touch);
            }
        }
        
        foreach(var touch in delete)
        {
            touches.Remove(touch);
        }
    }
    
    void UpdateProcess()
    {
        process += (touches.Count > 0 ? 1 : -1) * Time.deltaTime;
        
        if(process <= -.5f)
        {
            Destroy(gameObject);
        }
        else if(process >= 1)
        {
            Instantiate(tower, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
