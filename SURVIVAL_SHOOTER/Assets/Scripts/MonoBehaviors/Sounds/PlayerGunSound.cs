using UnityEngine;
using UnityEngine.Audio;

public class PlayerGunSound : MonoBehaviour
{
    private PlayerShooting playerShooting;
    private AudioSource audioSource;

    public AudioClip[] shotSounds;
    public AudioClip[] reloadSounds;
    public AudioClip[] machineGunSounds;

    void Awake()
    {
        playerShooting = GetComponent<PlayerShooting>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShotSound(Weapon weapon)
    {
        string weaponName = weapon.Name;

        switch(weaponName)
        {
            case "Shotgun":
                audioSource.clip = shotSounds[0];
            break;
            case "Uzi":
                audioSource.clip = shotSounds[1];
            break;
            case "MachineGun":
                audioSource.clip = shotSounds[2];
            break;

        }
        
        RandomizeSound();        
    }

    public void PlayReloadSound()
    {

    }

    private void RandomizeSound()
    {
        float shotPitch = Random.Range(0.85f, 1.05f);

        audioSource.pitch = shotPitch;        

        audioSource.Play();
    }
}
