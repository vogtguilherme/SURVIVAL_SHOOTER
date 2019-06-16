using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPlayer
{
    public string itemName;
    public string description;
    public int cost;
    public bool bought;
    public Sprite image;

    public ItemPlayer(Item p_item)
    {
        itemName = p_item.itemName;
        description = p_item.description;
        cost = p_item.cost;
        bought = p_item.bought;
        image = p_item.image;
    }
}

public class PlayerShopManager : MonoBehaviour
{
    [SerializeField]
    int money = 100;
    public int Money { get { return money; } }

    public List<Item> itemsResources = new List<Item>();

    List<ItemPlayer> playerItems = new List<ItemPlayer>();
    public List<ItemPlayer> PlayerItems { get { return playerItems; } }

    private void Awake()
    {
        for(int i = 0; i < itemsResources.Count; i++)
        {
            playerItems.Add(new ItemPlayer(itemsResources[i]));
        }
    }

    public void BuyItem(int p_id)
    {
        money -= playerItems[p_id].cost;
        playerItems[p_id].bought = true;

        ShopInstance.instance.shopGUI.FillShop();
    }
}
