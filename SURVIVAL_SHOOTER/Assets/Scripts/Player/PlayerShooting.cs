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
    #region Weapons

	private Weapon shotgun = new Weapon	(2, 6, 0.95f, 7.5f);
	private Weapon uzi = new Weapon	(1, 19, 0.15f, 12f);
	private Weapon machineGun = new Weapon	(1, 24, 0.2f, 20f);

    #endregion

    #region Variables    

    public static Weapon CurrentWeapon = new Weapon(2, 6, 1.5f, 15f);

	public static float CurrentWeaponFireRate
    {
        get => CurrentWeapon.FireRate;
    }

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

    public void ShootingBehavior()
    {
        timer += Time.deltaTime;

        //Se o jogador aperta o botão definido como Fire1, realizar o disparo
        if (Input.GetButton("Fire1") && timer >= CurrentWeapon.FireRate && canShoot) Shoot();
        
        //Desabilitar o efeito da arma se o timer excedeu o tempo de efeito
        if (timer >= CurrentWeapon.FireRate * effectsDisplaytime) DisableEffects();

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

		ReloadWeapon(ref CurrentWeapon);
	}

    void Shoot()
    {
		Player player = Player.Instance;

		if (CurrentWeapon.CurrentAmmo <= 0)
		{
			Debug.Log("No ammo.");

			if(CurrentWeapon.CarryingAmmo >= CurrentWeapon.MaximumAmmo)
				Reload(2.33f);

			return;
		}

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
        if (Physics.Raycast(shootRay, out shootHit, CurrentWeapon.FireDistance, shootableMask))
        {
			//Descobrir o que foi atingido
			Entity n_Entity = shootHit.collider.GetComponentInParent<Entity>();
			//Se foi um objeto com um componenete Entity
			if(n_Entity != null)
			{
				//Descontar dano do HP
				n_Entity.TakeHit(CurrentWeapon.BulletDamage, shootHit);
			}
			//Posicionar o fim do Line Renderer no ponto onde o raio atingiu
			gunLine.SetPosition(1, shootHit.point);
            
        }
        //Se não atingir nada
        else
        {
            //Atribui o segundo ponto do raio para a distância máxima do disparo
            gunLine.SetPosition(1, shootRay.origin + (shootRay.direction * CurrentWeapon.FireDistance));
        }

		CurrentWeapon.CurrentAmmo -= 1;

		Debug.Log("Ammo: " + CurrentWeapon.CurrentAmmo + "/" + CurrentWeapon.CarryingAmmo);

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
		Debug.Log("Ammo: " + CurrentWeapon.CurrentAmmo + "/" + CurrentWeapon.CarryingAmmo);
	}

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    #endregion
}
