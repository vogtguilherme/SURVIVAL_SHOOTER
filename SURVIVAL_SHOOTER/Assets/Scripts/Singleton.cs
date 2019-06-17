using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	private static T instance;

	public static T Instance
	{
		get
		{
			//instance = FindObjectOfType<T>();

			/*if (instance == null)
			{
				var obj = new GameObject();

				instance = obj.AddComponent<T>();
			}*/

			return instance;
		}
		set
		{
			instance = value;
		}
	}

	protected virtual void Awake()
	{
		if (instance != null && instance != this)
		{
			Debug.Log("Destroying " + this.name);
			Destroy(this);

			return;
		}
		else
		{
			Debug.Log("Setting " + this.name + " as instance.");
			instance = this as T;
		}			
	}
}
