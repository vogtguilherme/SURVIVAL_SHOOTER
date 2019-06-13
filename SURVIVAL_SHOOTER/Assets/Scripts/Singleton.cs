using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	private static T instance;

	public static T Instance
	{
		get
		{
			instance = FindObjectOfType<T>();

			if (instance == null)
			{
				var obj = new GameObject();

				instance = obj.AddComponent<T>();
			}

			return instance;
		}
		set
		{
			instance = value;
		}
	}

	private void Awake()
	{
		if (instance != null && instance != this)
			Destroy(this);
		else
			instance = this as T;		
	}
}
