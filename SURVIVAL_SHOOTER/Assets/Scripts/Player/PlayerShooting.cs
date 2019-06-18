/*
Codigo baseado no modelo da Unity - Survival Shooter encontrado no link:
https://unity3d.com/pt/learn/tutorials/projects/survival-shooter/harming-enemies?playlist=17144
*/

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(AudioSource))]
public class PlayerShooting : MonoBehaviour
{
	#region ExternalAcessVariables

	public float FireRate { get { return m_FireRate; } set { m_FireRate = value; } }
	public float FireDistance { get { return m_FireDistance; } set { m_FireDistance = value; } }
	public int BulletDamage { get { return m_BulletDamage; } set { m_BulletDamage = value; } }

	#endregion

	#region Variables

	private float m_FireRate = 0.2f;
	private float m_FireDistance = 25f;
	private int m_BulletDamage = 1;	

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    //ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource m_AudioSource;
    float effectsDisplaytime = 0.15f;

	bool canShoot = true;

    #endregion

    #region Unity Methods
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunLine = GetComponent<LineRenderer>();
        m_AudioSource = GetComponent<AudioSource>();		
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Se o jogador aperta o botão definido como Fire1, realizar o disparo
        if (Input.GetButton("Fire1") && timer >= m_FireRate && canShoot) Shoot();
        
        //Desabilitar o efeito da arma se o timer excedeu o tempo de efeito
        if (timer >= m_FireRate * effectsDisplaytime) DisableEffects();

		if(Input.GetKeyDown(KeyCode.R))
		{
			Reload(2.33f);
		}
    }
    #endregion

    #region Class Methods

	void Reload(float time)
	{
		StartCoroutine("StartReload", time);
	}

	private IEnumerator StartReload(float time)
	{
		yield return new WaitForSeconds(time);

		ReloadWeapon(ref Player.Instance.m_Weapon);
	}

    void Shoot()
    {
		Player player = Player.Instance;

		if (player.m_Weapon.CurrentAmmo <= 0)
		{
			Debug.Log("No ammo.");

			if(player.m_Weapon.CarryingAmmo >= player.m_Weapon.MaximumAmmo)
				Reload(2.33f);

			return;
		}

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
        if (Physics.Raycast(shootRay, out shootHit, player.m_Weapon.FireDistance, shootableMask))
        {
			//Descobrir o que foi atingido
			Entity n_Entity = shootHit.collider.GetComponentInParent<Entity>();
			//Se foi um objeto com um componenete Entity
			if(n_Entity != null)
			{
				//Descontar dano do HP
				n_Entity.TakeHit(player.m_Weapon.BulletDamage, shootHit);
			}
			//Posicionar o fim do Line Renderer no ponto onde o raio atingiu
			gunLine.SetPosition(1, shootHit.point);
            
        }
        //Se não atingir nada
        else
        {
            //Atribui o segundo ponto do raio para a distância máxima do disparo
            gunLine.SetPosition(1, shootRay.origin + (shootRay.direction * player.m_Weapon.FireDistance));
        }

		player.m_Weapon.CurrentAmmo -= 1;

		Debug.Log("Ammo: " + player.m_Weapon.CurrentAmmo + "/" + player.m_Weapon.CarryingAmmo);

		player.TriggerEvent();

	}

	private void ReloadWeapon(ref Weapon weapon)
	{	
		canShoot = false;
		//Numero de disparos realizados = Capacidade de um carregador subtraído pela quantidade de munição ainda carregada
		int emptyShots = weapon.MaximumAmmo - weapon.CurrentAmmo;
		Debug.Log(emptyShots);
		
		//Se a munição guardada é maior ou igual o número de disparos vazios
		if(weapon.CarryingAmmo >= emptyShots)
		{			
			//Recarregar
			weapon.CarryingAmmo = weapon.CarryingAmmo - emptyShots;
			weapon.CurrentAmmo = weapon.CurrentAmmo + emptyShots;

			Debug.Log("RECARREGANDO...");

			//yield return new WaitForSeconds(2.33f);

			canShoot = true;

			Debug.Log("Recarregado");
		}		
		else
		{
			Debug.Log("SEM MUNIÇÃO...");

			//yield return null;
		}

		Debug.Log("Ammo: " + weapon.CurrentAmmo + "/" + weapon.CarryingAmmo);
		Debug.Log("Ammo: " + Player.Instance.m_Weapon.CurrentAmmo + "/" + Player.Instance.m_Weapon.CarryingAmmo);
	}

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    #endregion
}
