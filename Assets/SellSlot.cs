using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellSlot : ItemSlot
{
    [SerializeField]
    TMP_Text sellPrice;


    public int totalSellPrice;

    public Shop shop;


    public override void Start()
    {
        base.Start();
        shop = GameObject.Find("ShopUI").GetComponent<Shop>();
    }
    public override void OnLeftCLick()
    {
        //Instead of equipping item, try to sell it
        if (isSelected() && !isEmpty)
        {
            shop.SellItemButton();
        }
        inventoryManager.ChangeSelectedSlot(this);
    }

    public override void AddItem(Item newItem, int quantity)
    {
        base.AddItem(newItem, quantity);
        totalSellPrice = newItem.sellPrice * quantity;
        //Change text once item is added
        sellPrice.text = newItem.sellPrice + "$ *" + quantity + " = +" + totalSellPrice;
    }
    public override void ClearSlot(Sprite noItem)
    {
        base.ClearSlot(noItem);
        totalSellPrice = 0;
        sellPrice.text = "";
    }

  

}
