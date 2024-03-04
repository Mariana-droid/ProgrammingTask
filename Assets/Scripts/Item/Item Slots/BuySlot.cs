using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuySlot : ItemSlot
{
    [SerializeField]
    TMP_Text buyPrice;

    public int totalBuyPrice;

    public Shop shop;

    public override void Start()
    {
        base.Start();
        shop = GameObject.Find("ShopUI").GetComponent<Shop>();
    }

    public override void OnLeftCLick()
    {
        //Instead of equipping item, try to buy it
        if (isSelected() && !isEmpty)
        {
            shop.BuyItemButton();
        }
        inventoryManager.ChangeSelectedSlot(this);
    }

    public override void AddItem(Item newItem, int quantity)
    {
        base.AddItem(newItem, quantity);
        totalBuyPrice = newItem.buyPrice * quantity;
        //Change text once item is added
        buyPrice.text = newItem.buyPrice + "$ *" + quantity + " = -" + totalBuyPrice;
    }
    public override void ClearSlot(Sprite noItem)
    {
        base.ClearSlot(noItem);
        totalBuyPrice = 0;
        buyPrice.text = "";
    }

}
