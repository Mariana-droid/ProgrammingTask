using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquippedSlot : ItemSlot
{
    [SerializeField]
    private Image playerDisplayImage;
    [SerializeField]
    private TMP_Text slotName;
    public ItemType itemType = new ItemType();
    

    public void EquipGear(Item newItem)
    {
        itemImage.sprite = newItem.icon;
        quantity = 1;
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
        quantity = 0;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        inventoryManager.DropGearInSlot(dropped.GetComponent<DraggableItem>().parentItemSlot, this);

    }
}
