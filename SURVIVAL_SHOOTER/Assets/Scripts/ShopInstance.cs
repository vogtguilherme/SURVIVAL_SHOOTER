using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInstance : Singleton<ShopInstance>
{
    public event System.Action onStartGameClicked;

    public PlayerShopManager playerShopManager;
    public ShopGUI shopGUI;

    protected override void Awake()
    {
		base.Awake();
    }

    public void StartGame()
    {
        shopGUI.Close();

        onStartGameClicked?.Invoke();
    }
}
