using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{
    public Image image;
    public Text nameText, costText, descriptionText, buttonBuyText;

    public Button buttonBuy;

    int id;

    public void SetItem(int p_id, string p_name, string p_description, int p_cost, Sprite p_image, bool p_bought)
    {
        id = p_id;

        nameText.text = p_name;
        descriptionText.text = p_description;

        costText.text = "R$" + p_cost;

        image.sprite = p_image;

        if (p_bought)
        {
            buttonBuyText.text = p_bought ? "Comprado!" : "Comprar";

            buttonBuy.interactable = !p_bought;
        }
        else
        {
            buttonBuyText.text = ShopInstance.Instance.playerShopManager.Money >= p_cost ? "Comprar" : "Sem grana";

            buttonBuy.interactable = ShopInstance.Instance.playerShopManager.Money >= p_cost ? true : false;
        }
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            if (!ShopInstance.Instance.playerShopManager.PlayerItems[id].bought)
            {
                if (buttonBuy.interactable && ShopInstance.Instance.playerShopManager.Money < ShopInstance.Instance.playerShopManager.PlayerItems[id].cost)
                {
                    buttonBuy.interactable = false;
                    buttonBuyText.text = "Sem grana";
                }
                else if(!buttonBuy.interactable && ShopInstance.Instance.playerShopManager.Money >= ShopInstance.Instance.playerShopManager.PlayerItems[id].cost)
                {
                    buttonBuy.interactable = true;
                    buttonBuyText.text = "Comprar";
                }
            }
        }
    }

    public void BuyItem()
    {
        ShopInstance.Instance.playerShopManager.BuyItem(id);
    }
}
