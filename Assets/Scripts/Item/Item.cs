using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Hat,
    Hair,
    Clothes,
    Consumable
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    [TextArea(3, 5)]
    public string description;
    public ItemType itemType;
    public int numberAnimation;
    public int buyPrice;
    public int sellPrice;
}
