using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
	public event System.Action OnTriggerDetection;

	private ItemType m_Type;
	private int m_Amount;	

	private Collider m_Collider;

	public int Amount { get => m_Amount; set => m_Amount = value; }

	public ItemType Gettype()
	{
		return m_Type;
	}

	public void Settype(ItemType value)
	{
		m_Type = value;
	}

	public abstract void CollectItem();

	public virtual void Awake()
	{
		m_Collider = GetComponent<Collider>();
		m_Collider.isTrigger = true;

		OnTriggerDetection += CollectItem;
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		var player = Player.Instance.gameObject;

		if (other.gameObject == player)
		{
			if (OnTriggerDetection != null)
				OnTriggerDetection();

			m_Collider.enabled = false;
		}
	}

	public virtual void OnDisable()
	{
		OnTriggerDetection -= CollectItem;		
	}
}
