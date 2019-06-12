using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInstance : MonoBehaviour
{
    public static ShopInstance instance;

    public event System.Action onStartGameClicked;

    public PlayerShopManager playerShopManager;
    public ShopGUI shopGUI;

    private void Awake()
    {
        if (instance) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void StartGame()
    {
        shopGUI.Close();

        onStartGameClicked?.Invoke();
    }
}
