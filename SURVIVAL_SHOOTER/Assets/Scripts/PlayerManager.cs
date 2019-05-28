using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPlayer
{
    public string itemName;
    public string description;
    public int cost;
    public bool bought;
    public Sprite image;

    public itemPlayer(Item p_item)
    {
        itemName = p_item.itemName;
        description = p_item.description;
        cost = p_item.cost;
        bought = p_item.bought;
        image = p_item.image;
    }
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField]
    int money = 100;
    public int Money { get { return money; } }

    public List<Item> itemsResources = new List<Item>();

    List<itemPlayer> playerItems = new List<itemPlayer>();
    public List<itemPlayer> PlayerItems { get { return playerItems; } }

    private void Awake()
    {
        instance = this;

        for(int i = 0; i < itemsResources.Count; i++)
        {
            playerItems.Add(new itemPlayer(itemsResources[i]));
        }
    }


    public void BuyItem(int p_id)
    {
        money -= playerItems[p_id].cost;
        playerItems[p_id].bought = true;

        ShopGUI.instance.FillShop();
    }
}
