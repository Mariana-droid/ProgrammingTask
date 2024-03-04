using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item item;
    public int quantity;
    public void AddToInventory()
    {
        inventoryManager.AddItem(item, quantity);
    }
}
