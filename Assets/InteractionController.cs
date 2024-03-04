using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private KeyCode interactKey;
    [SerializeField]
    private Vector2 boxSize = new Vector2(0.2f, 0.2f);
    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            CheckInteraction();
        }
    }

    private void CheckInteraction()
    {
        //Check for interactables within range
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if(hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    //Only interact with one object so return after
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
}
