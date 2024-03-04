using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Sprite noItem;
    [SerializeField]
    private SellSlot sellSlot;
    [SerializeField]
    private BuySlot buySlot;
    [SerializeField]
    public ItemSlot[] shopKeeperSlots;

    //managers
    [SerializeField]
    private MoneyManagment playerMoneyManagment;
    [SerializeField]
    private InventoryManager inventoryManager;

    //shopKeeper
    [SerializeField]
    private ShopKeeper shopKeeper;
    [SerializeField]
    private Image shopKeeperSprite;
    [SerializeField]
    private TMP_Text shopKeeperText;
    [SerializeField]
    private TMP_Text currentMoneyText;
    [SerializeField]
    private MoneyManagment shopKeeperManagment;



    public void Awake()
    {
        playerMoneyManagment = GameObject.Find("Player").GetComponent<MoneyManagment>();
    }
    public void OpenShop(ShopKeeper newShopKeeper)
    {
        //Set up UI
        shopKeeper = newShopKeeper;
        PopulateShop();

        shopKeeperSprite.sprite = newShopKeeper.iconShopKeeper;
        shopKeeperText.text = newShopKeeper.shopKeeperText;
        shopKeeperManagment = newShopKeeper.moneyManagment;
        shopKeeperManagment.currentMoneyText = currentMoneyText;

    }

    public void BuyItemButton()
    {
        //If player has enough space for the coins and seller has enough gold
        if (playerMoneyManagment.HasEnoughMoneyForPurchase(buySlot.totalBuyPrice) && shopKeeperManagment.SellItem(buySlot.totalBuyPrice))
        {
            if (inventoryManager.AddItem(buySlot.item, buySlot.quantity) && playerMoneyManagment.BuyItem(buySlot.totalBuyPrice))
            {
                buySlot.ClearSlot(noItem);
            }

        }
    }
    public void SellItemButton()
    {

        //If player has enough space for the coins and seller has enough gold
        if (shopKeeperManagment.HasEnoughMoneyForPurchase(sellSlot.totalSellPrice) && playerMoneyManagment.SellItem(sellSlot.totalSellPrice))
        {
            if (PlaceItemInShopKeeperInventory(sellSlot.item, sellSlot.quantity) && shopKeeperManagment.BuyItem(sellSlot.totalSellPrice))
            {
                sellSlot.ClearSlot(noItem);
            }

        }
    }

    public bool PlaceItemInShopKeeperInventory(Item item, int quantity)
    {
        //Tries to find instance of same item
        for (int i = 0; i < shopKeeperSlots.Length; i++)
        {

            //If the same item is found in the inventory try to add the quantity to the existing one
            if (!shopKeeperSlots[i].isEmpty && shopKeeperSlots[i].item.itemName == item.itemName)
            {
                //if it's possible, return true
                if (shopKeeperSlots[i].AddQuantity(quantity))
                    return true;
            }
        }
        //item is not in inventory or no slots with space
        for (int i = 0; i < shopKeeperSlots.Length; i++)
        {
            if (shopKeeperSlots[i].isEmpty)
            {
                shopKeeperSlots[i].AddItem(item, quantity);
                return true;
            }
        }
        return false;
    }

    public void PopulateShop()
    {
        // Check if the number of items and quantities match
        if (shopKeeper.itemsToSell.Length != shopKeeper.quantities.Length)
        {
            Debug.LogError("Number of items and quantities don't match!");
            return;
        }

        // Iterate over each item and quantity pair
        for (int i = 0; i < shopKeeper.itemsToSell.Length; i++)
        {
            // Call the PlaceItemInShopKeeperInventory function for each item and quantity pair
            bool placed = PlaceItemInShopKeeperInventory(shopKeeper.itemsToSell[i], shopKeeper.quantities[i]);

            //For now no debug;
        }
    }

    public void RetrieveShopItems()
    {
        List<Item> itemList = new List<Item>();
        List<int> quantityList = new List<int>();

        // Iterate over each shopKeeperSlot
        foreach (ItemSlot slot in shopKeeperSlots)
        {
            // If the slot is not empty, add its item and quantity to the lists
            if (!slot.isEmpty)
            {
                itemList.Add(slot.item);
                quantityList.Add(slot.quantity);

                // Clear the slot after retrieving its data
                slot.ClearSlot(noItem);
            }
        }

        // Update Shopkeeper details
        shopKeeper.itemsToSell = itemList.ToArray();
        shopKeeper.quantities = quantityList.ToArray();
    }

    //handle double clicking of item in shop
    public void PlaceItemInBuy(ShopKeeperSlot slot)
    {
        inventoryManager.SwitchSlots(slot, buySlot);
    }
}
