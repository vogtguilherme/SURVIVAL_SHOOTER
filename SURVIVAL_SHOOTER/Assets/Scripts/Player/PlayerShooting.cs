/*
Codigo baseado no modelo da Unity - Survival Shooter encontrado no link:
https://unity3d.com/pt/learn/tutorials/projects/survival-shooter/harming-enemies?playlist=17144
*/

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(AudioSource))]
public class PlayerShooting : MonoBehaviour
{
	public static float CurrentWeaponFireRate {get; set;}

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    //ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource m_AudioSource;
    public float effectsDisplaytime = 0.15f;

	bool canShoot = true;    

    #region Unity Methods
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunLine = GetComponent<LineRenderer>();
        m_AudioSource = GetComponent<AudioSource>();		
    }

	void Start()
	{
		Debug.Log("Fire Rate: " + PlayerWeaponManager.CurrentWeapon.FireRate);
	}
    
    #endregion

    #region Class Methods	

    public void Shooting()
    {
		Player player = Player.Instance;		

        player.Shoot();

		timer = 0f;
        //Toca o clipe de disparo.  #Fazer tocar audios aleatórios
        m_AudioSource.Play();

        //Começa os efeitos de partícula, para se estavam habilitados antes.
        //gunParticles.Stop();
        //gunParticles.Play();

        //Habilita o Line Renderer e seta a primeira posição para o final do cano.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        //Determina que o raio de disparo saia do cano e aponte para frente
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //Lança o raycast para gameobjects que estão no layer "Shootable"
        if (Physics.Raycast(shootRay, out shootHit, PlayerWeaponManager.CurrentWeapon.FireDistance, shootableMask))
        {
			//Descobrir o que foi atingido
			Entity n_Entity = shootHit.collider.GetComponentInParent<Entity>();
			//Se foi um objeto com um componenete Entity
			if(n_Entity != null)
			{
				//Descontar dano do HP
				n_Entity.TakeHit(PlayerWeaponManager.CurrentWeapon.BulletDamage/* , shootHit*/);
			}
			//Posicionar o fim do Line Renderer no ponto onde o raio atingiu
			gunLine.SetPosition(1, shootHit.point);
            
        }
        //Se não atingir nada
        else
        {
            //Atribui o segundo ponto do raio para a distância máxima do disparo
            gunLine.SetPosition(1, shootRay.origin + (shootRay.direction * PlayerWeaponManager.CurrentWeapon.FireDistance));
        }		
	}

	

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    #endregion
}
