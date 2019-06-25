using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplayController : MonoBehaviour
{
	public static HeadsUpDisplayController instance;

	public GameObject playerStatsHolder;

	private Text health_hud;	
	private Text weapon_hud;
	private Text ammunition_hud;

	private string healthLabel = "Vida: ";	
	private string weaponLabel = "Arma: ";
	private string ammunitionLabel = "Munição: ";

	[SerializeField] private Text[] hudTexts;

	private void Awake()
	{
		if (instance == null && instance != this)
			instance = this;
		else
			this.enabled = false;

		hudTexts = playerStatsHolder.GetComponentsInChildren<Text>();

		health_hud = hudTexts[0];		
		weapon_hud = hudTexts[1];
		ammunition_hud = hudTexts[2];		
	}

	private void Start()
	{
		playerStatsHolder.SetActive(false);
	}

	public void SetupHUD()
	{
		string currentHealth = Player.Instance.PlayerHealth.CurrentHealth.ToString();		
		string maxHealth = Player.Instance.PlayerHealth.DefaultHealth.ToString();

		health_hud.text = healthLabel + currentHealth + "/" + maxHealth;
		weapon_hud.text = weaponLabel + Player.Instance.m_PlayerShooting.CurrentWeapon.Name;

		string currentAmmo = Player.Instance.m_PlayerShooting.CurrentWeapon.CurrentAmmo.ToString();
		string maxAmmo = Player.Instance.m_PlayerShooting.CurrentWeapon.CarryingAmmo.ToString();

		ammunition_hud.text = currentAmmo + "/" + maxAmmo;
	}

	void OnDisable()
	{
		if(Player.Instance != null)
		{
			Player.Instance.OnVariableChanged -= SetupHUD;
		}
	}
}
