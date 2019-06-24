using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    #region Weapons

	public static Weapon shotgun = new Weapon	(2, 6, 0.95f, 7.5f);
	public static Weapon uzi = new Weapon	(1, 19, 0.15f, 12f);
	public static Weapon machineGun = new Weapon	(1, 24, 0.2f, 20f);

    #endregion   
    
    public static Weapon CurrentWeapon = new Weapon(2, 6, 1.5f, 15f);

    private float timer;
    private bool canShoot = true;

    public void Start()
    {
        string info = string.Format("Ammo: {0} / Fire Rate: {1}", CurrentWeapon.CurrentAmmo, CurrentWeapon.FireRate);

        Debug.Log(info);
    }

    public void ShootingBehavior()
    {
        timer += Time.deltaTime;

        //Se o jogador aperta o botão definido como Fire1, realizar o disparo
        if (Input.GetButton("Fire1") && timer >= CurrentWeapon.FireRate && canShoot) 
        {
            Shoot();
        }

		if(Input.GetKeyDown(KeyCode.R))
		{
			Reload(2.33f);
		}
    }

    private void Shoot()
    {
        if (CurrentWeapon.CurrentAmmo <= 0)
		{
			Debug.Log("No ammo.");

			if(CurrentWeapon.CarryingAmmo >= CurrentWeapon.MaximumAmmo)
				Reload(2.33f);

			return;
		}
        else
        {
            Debug.Log("Shot");
            
            Player.Instance.m_PlayerShooting.Shooting();

            CurrentWeapon.CurrentAmmo -= 1;            

            Player.Instance.TriggerEvent();
        }

        if(timer >= CurrentWeapon.FireRate * Player.Instance.m_PlayerShooting.effectsDisplaytime)
        {
            Player.Instance.m_PlayerShooting.DisableEffects();
        }
    }

    void Reload(float time)
	{
		StartCoroutine("StartReload", time);
	}

	private IEnumerator StartReload(float time)
	{
		yield return new WaitForSeconds(time);

		ReloadWeapon(ref CurrentWeapon);
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
}
