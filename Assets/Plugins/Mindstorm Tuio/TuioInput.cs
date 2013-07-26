/*
Author - Sascha Graeff

This class is supposed to bring Tuio Touches
into the Unity world the way the Unity Input class works.
*/
namespace Tuio
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using Tuio;
	using System.Linq;

	public static class TuioInput
	{
        //TODO: Als Preprocessor-Anweisung
        private const bool DEBUG = true;
    
		private static TuioTracking tracking = null;

		private static List<int> touchOrder = new List<int>();
		private static Dictionary<int, Tuio.Touch> touches =  new Dictionary<int, Tuio.Touch>();
		
		public static int touchCount
		{
			get
			{
				CreateUpdaterIfNeeded();
				return touchOrder.Count;
			}
		}
		
		public static Tuio.Touch GetTouch(int index)
		{
			CreateUpdaterIfNeeded();
			return touches[touchOrder[index]];
		}
		
		private static void CreateUpdaterIfNeeded()
		{
			TuioInputUpdater.EnsureInstance();
		}

		internal static void Init()
		{
			TuioConfiguration config = new TuioConfiguration();
			if (tracking == null) tracking = new TuioTracking();
			tracking.ConfigureFramework(config);
			tracking.Start();
		}
		
		// Ensure that the instance is destroyed when the game is stopped in the editor.
		internal static void Stop() 
		{
			tracking.Stop();
		}
		
		
		private static List<Tuio.Touch> getNewTouches () {
			return touches.Values.Where(t => t.phase == TouchPhase.Began).ToList();	
		}
		
		internal static void Update()
		{
			BuildTouchDictionary();
		}
		
		private static void BuildTouchDictionary()
		{
			deleteNonCurrentTouches();
			
			updateAllTouchesAsTemp();
			
			updateTouches();
			
			updateEndedTouches();
            
            if(DEBUG)
            {
                UpdateMouseTouch();
            }
		}
		
		/// <summary>
		/// Deletes all old non-current touches from the last frame 
		/// </summary>
		private static void deleteNonCurrentTouches()
		{
			int[] deadTouches = (from Tuio.Touch t in touches.Values
					where !t.IsCurrent
					select t.touchId).ToArray();
			foreach (int touchId in deadTouches)
			{
				touches.Remove(touchId);
				touchOrder.Remove(touchId);
			}
		}
		
		/// <summary>
		/// Update all remaining touches as temp (setting new points will reset this) 
		/// </summary>
		private static void updateAllTouchesAsTemp()
		{
			foreach (Tuio.Touch t in touches.Values) t.SetTemp();
		}
		
		/// <summary>
		/// Updates all touches with the latest TUIO received data 
		/// </summary>
		private static void updateTouches()
		{
			Tuio2DCursor[] cursors = tracking.GetTouchArray();
			
			// Update touches in current collection
			foreach (Tuio2DCursor cursor in cursors)
			{
				if (cursor == null)
				{
					Debug.LogError("CURSOR NULL");
					continue;
				}
				
				// Get the touch relating to the key
				Tuio.Touch t = null;
				if (touches.ContainsKey(cursor.SessionID))
				{
					// It's not a new one
					t = touches[cursor.SessionID];
					// Update it's position
					t.SetNewTouchPoint(getScreenPoint(cursor)/*, getRawPoint(cursor)*/);
				}
				else
				{
					// It's a new one
					t = buildTouch(cursor);
					touches.Add(cursor.SessionID, t);
					touchOrder.Add(cursor.SessionID);
				}
			}
		}
		
		private static Tuio.Touch buildTouch(Tuio2DCursor cursor)
		{
			TouchProperties prop;
			prop.Acceleration = cursor.Acceleration;
			prop.VelocityX = cursor.VelocityX;
			prop.VelocityY = cursor.VelocityY;

			Vector2 p = getScreenPoint(cursor);
			//Vector2 raw = getRawPoint(cursor);

			Tuio.Touch t = new Tuio.Touch(cursor.SessionID, p/*, raw*/);		
			t.Properties = prop;

			return t;
		}
		
		/// <summary>
		/// Update non-current touches as ended 
		/// </summary>
		private static void updateEndedTouches()
		{
			var nonCurrent = from Tuio.Touch t in touches.Values
					where !t.IsCurrent
					select t;
			foreach (Tuio.Touch t in nonCurrent) t.phase = TouchPhase.Ended;
		}
		
		public static Vector2 getRawPoint(Tuio2DCursor data)
		{
			Vector2 position = new Vector2(data.PositionX, data.PositionY);
			return position;
		}
		
		private static Vector2 getScreenPoint(Tuio2DCursor data)
		{
			Vector2 position = new Vector2(data.PositionX, data.PositionY);
			
			float x1 = getScreenPoint(position.x,
				Screen.width, false);
			float y1 = getScreenPoint(position.y,
				Screen.height, true);

			Vector2 t = new Vector2(x1, y1);
			return t;
		}
		
		private static float getScreenPoint(float xOrY, double screenDimension, bool flip)
		{
			// Flip it the get in screen space
			if (flip) xOrY = 0.5f + (0.5f - xOrY);
			xOrY *= (float)screenDimension;
			return xOrY;
		}
        
        const int mouseID = -1;
        private static void UpdateMouseTouch()
        {
            if(Input.GetMouseButtonDown(0))
            {
                touches.Add(mouseID, new Tuio.Touch(mouseID, Input.mousePosition));
                touchOrder.Add(mouseID);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                touches[mouseID].phase = TouchPhase.Ended;
            }
            else
            {
                if(touches.ContainsKey(mouseID))
                {
                    Tuio.Touch t = touches[mouseID];
                    if(t != null)
                    {
                        t.SetNewTouchPoint(Input.mousePosition);
                    }
                }
            }
        }
	}
}