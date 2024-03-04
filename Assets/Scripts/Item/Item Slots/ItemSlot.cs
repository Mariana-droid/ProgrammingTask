using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    //Item data
    public Item item;
    public int quantity;
    public bool isEmpty = true;
    public int maxQuantity;

    //Item Slot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    public Image itemImage;
    [SerializeField]
    public GameObject imageGameObject;
    [SerializeField]
    private GameObject selectedShader;

    public InventoryManager inventoryManager;
    Transform parentAfterDrag;

    public virtual void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public void OnAwake()
    {
        Deselect();
    }
    public  virtual void AddItem(Item newItem, int quantity)
    {

        this.item = newItem;
        this.quantity = quantity;
        this.isEmpty = false;

       //Only display quantities if it's bigger than 0
       if(quantity > 0)
            quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = newItem.icon;
    }

    public bool AddQuantity(int value)
    {
        //Checks to see if slot can handle the new quantity
        if (quantity + value > maxQuantity)
        {
            return false;
        }
        quantity += value;
        quantityText.text = quantity.ToString();

        return true;
    }

    public bool UseItem(int quantityToRemove, Sprite noItem)
    {
        //If there are not enough items cant use items
        if (quantity - quantityToRemove < 0)
            return false;
        //If there are just enough clear slot
        else if(quantity - quantityToRemove == 0)
        {
            ClearSlot(noItem);
            return true;
        }
        //Else just take quantity off
        quantity = quantity - quantityToRemove;
        quantityText.text = quantity.ToString();

        return true;
        
    }
     public virtual void ClearSlot(Sprite noItem)
    { 
        itemImage.sprite = noItem;
        isEmpty = true;
        item = null;
        quantity = 0;
        quantityText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftCLick();
        }
    }

    public virtual void OnLeftCLick()
    {
        if(isSelected() && !isEmpty && (item.itemType == ItemType.Clothes || item.itemType == ItemType.Hair))
        {
            inventoryManager.EquipGear(this);
        }
        inventoryManager.ChangeSelectedSlot(this);
    }

    public bool isSelected()
    {
        return selectedShader.activeSelf;
    }
    public void Select()
    {
        selectedShader.SetActive(true);
    }
    public void Deselect()
    {
        selectedShader.SetActive(false);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        inventoryManager.SwitchSlots(dropped.GetComponent<DraggableItem>().parentItemSlot, this);
    }
}
