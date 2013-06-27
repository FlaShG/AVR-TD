/*
Unity3d-TUIO connects touch tracking from a TUIO to objects in Unity3d.

Copyright 2011 - Mindstorm Limited (reg. 05071596)

Author - Simon Lerpiniere
Modifier by Sascha Graeff

This file is part of Unity3d-TUIO.

Unity3d-TUIO is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Unity3d-TUIO is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser Public License for more details.

You should have received a copy of the GNU Lesser Public License
along with Unity3d-TUIO.  If not, see <http://www.gnu.org/licenses/>.

If you have any questions regarding this library, or would like to purchase 
a commercial licence, please contact Mindstorm via www.mindstorm.com.
*/

using System;
using System.Diagnostics;
using UnityEngine;

namespace Tuio
{
    /// <summary>
    /// Handles simple data about a touch including status, properties and ID
    /// </summary>
    public class Touch
    {
        internal readonly int touchId;
		
		long dTime = 0;
		
		Stopwatch sw;
		
        /// <summary>
        /// Holds detailed information about this touch (e.g. Velocity).
        /// </summary>
        internal TouchProperties Properties;

        protected internal Touch(int id, Vector2 point)
        {
            touchId = id;
            position = point;
			//RawPoint = point;
			IsCurrent = true;
			sw = Stopwatch.StartNew();
			timeAdded = Time.time;
        }
		
		/*internal Touch(int Id, Vector2 point, Vector2 rawPoint)
        {
            TouchId = Id;
            position = point;
			RawPoint = rawPoint;
			IsCurrent = true;
			sw = Stopwatch.StartNew();
			TimeAdded = Time.time;
        }*/
		
		public Vector2 deltaPosition
		{
			get;
			private set;
		}
		
		public float deltaTime
		{
			get{ return dTime / Stopwatch.Frequency; }
		}
		
		public TouchPhase phase
		{
			get;
			internal set;
		}

        public Vector2 position
        {
            get;
            private set;
        }
		
		/*public Vector2 RawPoint
        {
            get;
            private set;
        }*/
		
		internal bool IsCurrent
		{
			get;
			private set;
		}
		
		public float timeAdded
		{
			get;
			private set;
		}

        internal void SetMoving()
        {
            phase = TouchPhase.Moved;
        }
        internal void SetHeld()
        {
            phase = TouchPhase.Stationary;
        }
		
		/// <summary>
		/// Use this function before updating all touches to know which ones have not been updated 
		/// </summary>
		internal void SetTemp()
		{
			IsCurrent = false;
		}
        
        /// <summary>
        /// Updates the touch point to a new one without messing up other internal variables.
        /// Use this function when a touch point has moved.
        /// </summary>
        /// <param name="p">New point of the touch location.</param>
        internal void SetNewTouchPoint(Vector2 p)
        {
			// We're being updated so must be current
			IsCurrent = true;
			
			dTime = sw.ElapsedTicks;
			sw.Reset();
			sw.Start();
			
			// If we've not moved then we're Held
			if (p != position) 
			{
				SetMoving();
				deltaPosition = p - position;
				position = p;
				//RawPoint = p;
			}
			else SetHeld();
        }
		
		/*public void SetNewTouchPoint(Vector2 p, Vector2 rawPoint)
        {
			// We're being updated so must be current
			IsCurrent = true;
			
			dTime = sw.ElapsedTicks;
			sw.Reset();
			sw.Start();
			
			// If we've not moved then we're Held
			if (p != position) 
			{
				SetMoving();
				deltaPosition = p - position;
				position = p;
				//RawPoint = rawPoint;
			}
			else SetHeld();
        }*/
		
		
    }
}
