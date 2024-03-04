using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject itemDescriptionUI;
    [SerializeField]
    private GameObject shopUI;

    [SerializeField]
    private SellSlot sellSlot;
    [SerializeField]
    private BuySlot buySlot;
    [SerializeField]
    private Sprite noItem;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    public MoneyManagment moneyManagment;


    public Sprite iconShopKeeper;
    [TextArea(3, 5)]
    public string shopKeeperText;
    public Item[] itemsToSell;
    public int[] quantities;

    private bool isShopOpen;

    public void OpenCloseShopUI()
    {
        if (!isShopOpen)
        {
            inventoryManager.OpenInventory();
            inventoryManager.ChangeIsShopOpen(true);
            //Turn off item description
            itemDescriptionUI.SetActive(false);
            //Turn on Shop UI instead
            shopUI.SetActive(true);
            isShopOpen = true;
            shopUI.GetComponent<Shop>().OpenShop(this);
        }
        else
        {
            shopUI.GetComponent<Shop>().RetrieveShopItems();
            //Turn On item description
            itemDescriptionUI.SetActive(true);
            //Turn Off Shop UI instead
            shopUI.SetActive(false);
            inventoryManager.CloseInventory();
            inventoryManager.ChangeIsShopOpen(false);
            isShopOpen = false;
        }
    }


}
