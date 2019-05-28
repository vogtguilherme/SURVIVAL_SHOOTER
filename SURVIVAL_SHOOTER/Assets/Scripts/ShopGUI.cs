using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGUI : MonoBehaviour
{
    public static ShopGUI instance;

    public Text moneyText;

    public Transform contentItems;

    ItemGUI _currentItem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FillShop();
    }

    public void FillShop()
    {
        for (int i = 0; i < contentItems.childCount; i++)
        {
            contentItems.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < PlayerManager.instance.PlayerItems.Count; i++)
        {
            _currentItem = contentItems.GetChild(i).GetComponent<ItemGUI>();
            _currentItem.gameObject.SetActive(true);

            _currentItem.SetItem(p_id: i,
                                    p_name: PlayerManager.instance.PlayerItems[i].itemName,
                                    p_description: PlayerManager.instance.PlayerItems[i].description,
                                    p_cost: PlayerManager.instance.PlayerItems[i].cost,
                                    p_bought: PlayerManager.instance.PlayerItems[i].bought,
                                    p_image: PlayerManager.instance.PlayerItems[i].image);
        }
    }

    void Update()
    {
        moneyText.text = "$" + PlayerManager.instance.Money;
    }
}
