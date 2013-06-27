namespace Tuio
{
	using UnityEngine;
	using System.Collections;

	public class TuioInputUpdater : MonoBehaviour
	{
		private static TuioInputUpdater instance;

		void Awake()
		{
			if(!instance)
				instance = this;
			else
				Destroy(this);
				
			TuioInput.Init();
			gameObject.hideFlags = HideFlags.HideAndDontSave;
		}

		void Update()
		{
			TuioInput.Update();
		}
		
		void OnApplicationQuit()
		{
			TuioInput.Stop();
			DestroyImmediate(gameObject);
		}
		
		public static void EnsureInstance()
		{
			if(instance) return;
			GameObject go = new GameObject("TuioInput Updater");
			instance = go.AddComponent<TuioInputUpdater>();
		}
	}
}