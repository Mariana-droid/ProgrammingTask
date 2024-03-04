using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquippedSlot : ItemSlot
{
    [SerializeField]
    private Image playerDisplayImage;
    [SerializeField]
    private TMP_Text slotName;
    [SerializeField]
    private ItemType itemType = new ItemType();
    

    public void EquipGear(Item newItem)
    {
        itemImage.sprite = newItem.icon;
        isEmpty = false;
        slotName.enabled = false;
        item = newItem;

        playerDisplayImage.sprite = newItem.icon;
    }

    public override void OnLeftCLick()
    {
        if (isSelected() && !isEmpty)
        {
            inventoryManager.UnEquipGear(this);
        }
        inventoryManager.ChangeSelectedSlot(this);
    }

    public override void ClearSlot(Sprite noItem)
    {
        base.ClearSlot(noItem);
        playerDisplayImage.sprite = noItem;
        slotName.enabled = true;
    }
}
