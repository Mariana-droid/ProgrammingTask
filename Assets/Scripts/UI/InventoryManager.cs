using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public bool isShopOpen = false;
    private bool menuActivated = false;
    public ItemSlot[] itemSlots;

    private ItemSlot selectedSlot = null;

    //Item description tab
    [SerializeField]
    private Image itemDescriptionImage;
    [SerializeField]
    private Sprite noItem;
    [SerializeField]
    private TMP_Text itemDescriptionDescriptionText;
    [SerializeField]
    private TMP_Text itemDescriptionNameText;


    //Equip slots
    [SerializeField]
    private EquippedSlot clothesEquipSlot;
    [SerializeField]
    private EquippedSlot headEquipSlot;

    [SerializeField]
    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        //Only be able to interact with inventory if shop is not open 
        if (Input.GetButtonDown("Inventory") && menuActivated && !isShopOpen)
        {
            CloseInventory();
        }
        else if(Input.GetButtonDown("Inventory") && !menuActivated && !isShopOpen)
        {
            OpenInventory();
        }

    }

    public void OpenInventory()
    {
        Time.timeScale = 0;
        InventoryMenu.SetActive(true);
        menuActivated = true;
    }
    public void CloseInventory()
    {
        ChangeSelectedSlot(null);
        InventoryMenu.SetActive(false);
        menuActivated = false;
        Time.timeScale = 1;
    }
   
    public void ChangeIsShopOpen(bool value)
    {
        isShopOpen = value;
    }

    public bool AddItem(Item item, int quantity)
    {
        //Tries to find instance of same item
        for(int i = 0; i < itemSlots.Length; i++)
        {

            //If the same item is found in the inventory try to add the quantity to the existing one
            if(!itemSlots[i].isEmpty && itemSlots[i].item.itemName == item.itemName)
            {
                //if it's possible, return true
                if (itemSlots[i].AddQuantity(quantity))
                    return true;
            }
        }
        //item is not in inventory or no slots with space
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].isEmpty)
            {
                itemSlots[i].AddItem(item, quantity);
                return true;
            }
        }
        return false;
    }

    public void ChangeSelectedSlot(ItemSlot newSelected)
    {
        if(selectedSlot != null)
            selectedSlot.Deselect();

        if(newSelected != null)
        {
            //if the slot is not empty, display item on the left
            if (!newSelected.isEmpty)
            {
                itemDescriptionImage.sprite = newSelected.item.icon;
                itemDescriptionDescriptionText.text = newSelected.item.description;
                itemDescriptionNameText.text = newSelected.item.itemName;
            }
            //no item, turn it off
            if (newSelected.isEmpty)
            {
                itemDescriptionImage.sprite = noItem;
                itemDescriptionDescriptionText.text = "";
                itemDescriptionNameText.text = "";
            }

            newSelected.Select();
        }
        //If its null 
        else if(newSelected == null)
        {
            itemDescriptionImage.sprite = noItem;
            itemDescriptionDescriptionText.text = "";
            itemDescriptionNameText.text = "";
        }

        selectedSlot = newSelected;
    }
    
    public void EquipGear(ItemSlot slot)
    {
        //Put it on the correct slot
        if (slot.item.itemType == ItemType.Clothes)
        {
            //Unequip current clothes
            if (!clothesEquipSlot.isEmpty)
            {
                UnEquipGear(clothesEquipSlot);
            }
            clothesEquipSlot.EquipGear(slot.item);
            playerMovement.ChangeBody(slot.item.numberAnimation);
        }
        else if(slot.item.itemType == ItemType.Hair)
        {
            //Unequip current hair
            if (!headEquipSlot.isEmpty)
            {
                UnEquipGear(headEquipSlot);
            }
            headEquipSlot.EquipGear(slot.item);
            playerMovement.ChangeHead(slot.item.numberAnimation);
        }
        //Use one instance of this item
        slot.UseItem(1, noItem);
    }

    public void DropGearInSlot(ItemSlot slot, EquippedSlot finalSlot)
    {
        //Put it on the correct slot
        if (slot.item.itemType == finalSlot.itemType)
        {
            EquipGear(slot);
        }

    }

    public void UnEquipGear(EquippedSlot slot)
    {

        if (slot.item.itemType == ItemType.Clothes)
        {
            playerMovement.ChangeBody(0);
        }
        else if (slot.item.itemType == ItemType.Hair)
        {
            playerMovement.ChangeHead(0);
        }
        //add item to normal inventory
        AddItem(slot.item, 1);
        //clear equip Slot
        slot.ClearSlot(noItem);
    }

    public void SwitchSlots(ItemSlot originSlot, ItemSlot finalSlot)
    {
        //LastMinutedebugging
        if (originSlot.item == null || originSlot == finalSlot)
            return;
        //------------------------------------------------------------------
        //shopkeeper slot items can only be placed in buy slots
        if (originSlot.GetType() == typeof(ShopKeeperSlot) && finalSlot.GetType() != typeof(BuySlot))
        {
            return;
        }
        //in the sell slot, only things from itemslot or equipped slots allowed
        if (finalSlot.GetType() == typeof(SellSlot) && (originSlot.GetType() != typeof(ItemSlot) && originSlot.GetType() != typeof(EquippedSlot)))
        {
            return;
        }
        //ShopKeeperSlots only allowed in the buy slot (can't move around shopkeeper inventory)
        if (originSlot.GetType() != typeof(ShopKeeperSlot) && finalSlot.GetType() == typeof(BuySlot))
        {
            return;
        }
        //Can't put things in shop keeper inventory (unless regretting buying
        if (finalSlot.GetType() == typeof(ShopKeeperSlot) && originSlot.GetType() != typeof(BuySlot))
        {
            return;
        }
        //------------------------------------------------------------------

        //If slot is empty, add dragged item to it
        if (finalSlot.isEmpty)
        {
            finalSlot.AddItem(originSlot.item, originSlot.quantity);
            originSlot.ClearSlot(noItem);
        }
        //If not check if its the same item
        else if(originSlot.item.itemName == finalSlot.item.itemName)
        {
            //If possible clear slot
            if (finalSlot.AddQuantity(originSlot.quantity))
            {
                originSlot.ClearSlot(noItem);
            }
        }
    }
}
