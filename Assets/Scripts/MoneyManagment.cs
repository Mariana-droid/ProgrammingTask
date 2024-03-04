using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MoneyManagment : MonoBehaviour
{
    [SerializeField]
    int currentMoney;
    [SerializeField]
    int maxMoney;
    public TMP_Text currentMoneyText;
    private void Start()
    {
        currentMoneyText.text = currentMoney + " $";
    }
    public bool SellItem(int value)
    {
        if(currentMoney+value > maxMoney)
        {
            currentMoney = maxMoney;
            return true;
        }
        else
        {
            currentMoney += value;
            currentMoneyText.text = currentMoney + " $";
            return true;
        }
    }

    public bool BuyItem(int value)
    {
        if (currentMoney - value < 0)
        {
            return false;
        }
        else
        {
            currentMoney -= value;
            currentMoneyText.text = currentMoney + " $";
            return true;
        }
    }

    public bool HasEnoughMoneyForPurchase(int value)
    {
        return (currentMoney - value >= 0);
    }
}
