using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class debug : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] item;
    public int[] quantity;
    public void AddHair()
    {
        inventoryManager.AddItem(item[0], quantity[0]);
    }
    public void AddOutfit1()
    {
        inventoryManager.AddItem(item[1], quantity[1]);
    }
    public void AddOutfit2()
    {
        inventoryManager.AddItem(item[2], quantity[2]);
    }
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
