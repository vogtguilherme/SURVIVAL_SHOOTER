using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	[SerializeField]private float m_Smoothing = 20f;
	private Vector3 velocity = Vector3.zero;

	public Transform m_Target { get; set; }
	public Vector3 m_Offset;

	void Start()
	{
		m_Target = Player.Instance.gameObject.transform;		

		m_Offset = transform.position - m_Target.position;
	}

	void FixedUpdate()
	{
		Vector3 targetCameraPos = m_Target.position + m_Offset;

		transform.position = Vector3.SmoothDamp (transform.position, targetCameraPos, ref velocity, m_Smoothing * Time.deltaTime);
	}
}
