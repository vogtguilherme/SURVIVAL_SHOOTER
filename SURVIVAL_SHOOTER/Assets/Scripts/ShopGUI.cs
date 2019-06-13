using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGUI : MonoBehaviour
{
    public CanvasGroup panelShop;

    public Text moneyText;

    public Transform contentItems;

    public float closeShopDuration = 0.5f;

    ItemGUI _currentItem;

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

        for (int i = 0; i < ShopInstance.instance.playerShopManager.PlayerItems.Count; i++)
        {
            _currentItem = contentItems.GetChild(i).GetComponent<ItemGUI>();
            _currentItem.gameObject.SetActive(true);

            _currentItem.SetItem(p_id: i,
                                    p_name: ShopInstance.instance.playerShopManager.PlayerItems[i].itemName,
                                    p_description: ShopInstance.instance.playerShopManager.PlayerItems[i].description,
                                    p_cost: ShopInstance.instance.playerShopManager.PlayerItems[i].cost,
                                    p_bought: ShopInstance.instance.playerShopManager.PlayerItems[i].bought,
                                    p_image: ShopInstance.instance.playerShopManager.PlayerItems[i].image);
        }
    }

    void Update()
    {
        moneyText.text = "$" + ShopInstance.instance.playerShopManager.Money;
    }

    public void Open()
    {
        panelShop.blocksRaycasts = true;
        panelShop.interactable = true;
        panelShop.alpha = 1f;
    }

    public void Close()
    {
        StartCoroutine("CloseShop");
    }

    private IEnumerator CloseShop()
    {
        panelShop.blocksRaycasts = false;
        panelShop.interactable = false;

        float startTime = 0f;

        while (startTime <= closeShopDuration)
        {
            panelShop.alpha = Mathf.Lerp(1f, 0f, startTime / closeShopDuration);

            startTime += Time.deltaTime;

            yield return null;
        }
    }
}
