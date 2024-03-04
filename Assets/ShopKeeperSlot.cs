using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperSlot : ItemSlot
{
    public Shop shop;

    public override void Start()
    {
        base.Start();
        shop = GameObject.Find("ShopUI").GetComponent<Shop>();

    }
    public override void OnLeftCLick()
    {
        //Instead of equipping item, place it in the buy slot
        if (isSelected() && !isEmpty)
        {
            shop.PlaceItemInBuy(this);
        }
        inventoryManager.ChangeSelectedSlot(this);

    }
}
