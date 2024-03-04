using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    Rigidbody2D rb;
    Animator animator;
    [SerializeField]
    Animator bodyAnimator;
    [SerializeField]
    Animator headAnimator;

    Vector2 movement;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        bodyAnimator.SetInteger("Outfit", 0);
        headAnimator.SetInteger("Hair", 0);
    }

    void Update()
    {
        //Handle input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        bodyAnimator.SetFloat("Horizontal", movement.x);
        bodyAnimator.SetFloat("Vertical", movement.y);
        bodyAnimator.SetFloat("Speed", movement.sqrMagnitude);

        headAnimator.SetFloat("Horizontal", movement.x);
        headAnimator.SetFloat("Vertical", movement.y);
        headAnimator.SetFloat("Speed", movement.sqrMagnitude);

        //So the idle sprite is correctly set  
        if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
        {
            animator.SetFloat("LastMovedX", movement.x);
            animator.SetFloat("LastMovedY", movement.y);

            bodyAnimator.SetFloat("LastMovedX", movement.x);
            bodyAnimator.SetFloat("LastMovedY", movement.y);

            headAnimator.SetFloat("LastMovedX", movement.x);
            headAnimator.SetFloat("LastMovedY", movement.y);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void ChangeHead(int hair)
    {
        headAnimator.SetInteger("Hair", hair);
    }
    public void ChangeBody(int outfit)
    {
        bodyAnimator.SetInteger("Outfit", outfit);
    }
}
