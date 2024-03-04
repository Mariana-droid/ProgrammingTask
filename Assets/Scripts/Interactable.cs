using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    public UnityEvent interactFunctions;
    // Start is called before the first frame update
    void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    //This needs to be implemented depending on the type of interaction
    public void Interact()
    {
        interactFunctions.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TODO Turn on an interaction icon when near;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TODO Turn off an interaction icon when exiting range;
        }
    }


}
