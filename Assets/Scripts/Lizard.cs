using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public Animator anim;
    public Vector2 force = new Vector2(0f, 5f);
    public Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float hangTime = 2f;
    private float hangCounter = 0f;

    public bool isGrounded = false;
    public bool facingRight = true;
    
    void Update()
    {
        // Checking the side player is going, left or right
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); // so its either -1 or 1
        transform.position += movement * Time.deltaTime * moveSpeed; // so the position of our player is -1 or 1 * time has passed and ms 
        
        // ***=== Jumping section start ===*** \\
        HangManager(); // After player leaves platform timer

        if (Input.GetButtonDown("Jump") && hangCounter > 0)
        {
            Jump();
        } // Jump on spacebar
        // ***=== Jumping section end ===*** \\

        // Attack on LeftShift but can be changed. In work
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // Set animation to walking if x axis has difference >0 or its not walking, then turn off walking animation
        if (movement.x > 0 || movement.x < 0)
        {
            anim.SetBool("isRunning", true);
        } 
        else 
        {
            anim.SetBool("isRunning", false);
        }
        
        // Flip if changed direction
        if ( (movement.x < 0 && facingRight) || (movement.x > 0 && !facingRight))
        {
            Flip();
        }

    }

    void HangManager()
    {
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
    }

    void Attack()
    {
        anim.SetTrigger("attack");
        //anim.ResetTrigger("attack");
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .3f);
        } else if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = force;
        //gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
