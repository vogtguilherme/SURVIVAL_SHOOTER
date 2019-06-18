using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{   
    public float MovementSpeed { get { return m_MovementSpeed; } set { m_MovementSpeed = value; } }

    private float m_MovementSpeed;

    private Rigidbody rigidBody;

	private Vector3 direction;

    private float camRayLenght = 100f;
    private int floorMask;

    float x;
    float y;

    [Space(0.5f)]
	[Header("Debug")]
	[SerializeField]private bool drawDebugRay = true;
	[SerializeField]private float rayDistance = 10f;

    public Vector3 velocity { get => new Vector2(x, y); }

    void Awake()
	{
        floorMask = LayerMask.GetMask("Floor");

        rigidBody = GetComponent<Rigidbody> ();		
	}

    /*void FixedUpdate()
	{
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		Move (x, y);

        Turning();
	}*/


    public void UpdateMovement()
	{
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");

		Move(x, y);

		Turning();
	}

	void Update()
	{	
		if(drawDebugRay)
			Debug.DrawRay (transform.position, direction * rayDistance, Color.red);
	}

	void Move(float h, float v)
	{
		direction.Set (h, 0, v);

		direction = direction.normalized * m_MovementSpeed * Time.deltaTime;

		rigidBody.MovePosition (transform.position + direction);
	}

    void Turning()
    {
        // Cria um raio da câmera para o ponto no mapa onde o cursor está apontando
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //Cria a variavel RayCastHit que armazena informação sobre o que foi atingido
        RaycastHit floorHit;

        //Verifica se o raycast atinge o layer do chão
        if(Physics.Raycast(cameraRay, out floorHit, camRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            
            //Zera o y do vetor para que este permaneca no nivel do chão
            playerToMouse.y = 0;

            //Cria uma roatação baseada no vetor do personagem para o mouse
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            rigidBody.MoveRotation(newRotation);
        }
    }

	public Vector3 MovementDirection()
	{
		float range = 15f;

		Vector3 n_movementDirection = transform.position + direction * range;

		return n_movementDirection;
	}

	public void DisableMovement()
	{
		rigidBody.velocity = Vector3.zero;
	}
}
