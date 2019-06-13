using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ItemType { NONE, VIDA, ESTAMINA, MUNICAO, CHAVE };

[System.Serializable]
public class Object
{
    public Transform postion;
    public GameObject thisObject;
    public ItemType type;
}

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField, Header("Prefabs dos Objetos")]
    GameObject[] objetos;

    [SerializeField, Header("Lista dos Objetos Instanciados e suas posições")]
    List<Object> objectsLocations = new List<Object>();

	[SerializeField] Transform itemPositionHolder;

	[SerializeField] private Transform[] itemPosition;

	private void Awake()
	{
		itemPosition = new Transform[itemPositionHolder.childCount];

		for (int i = 0; i < itemPosition.Length; i++)
		{
			itemPosition[i] = itemPositionHolder.GetChild(i).transform;
		}
	}

	private void Start()
    {
		for (int i = 0; i < itemPosition.Length; i++)
		{
			itemPosition[i] = itemPositionHolder.GetChild(i).transform;

			int __rand = Mathf.FloorToInt(Random.Range(0, objetos.Length - 0.001f));			

			var item = Instantiate(objetos[__rand], itemPosition[i].position, itemPosition[i].rotation) as GameObject;

			item.transform.SetParent(itemPosition[i]);
		}		
    }
}
