using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float maxSpeed;

    public float acceleration;

    public Rigidbody2D myRb;
    public float jumpForce;
    public bool isGrounded;

    public float secondaryJumpForce;
    public float secondaryJumpTime;

    public bool secondaryJump;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>(); // look for a component called Rigidbody2D and assign it to myRb
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(myRb.velocity.x)); // sets the speed parameter in the animator to the absolute value of the player's x velocity
        
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && Mathf.Abs(myRb.velocity.x) < maxSpeed) // if the absolute value of the input is greater than 0.1, and player is not moving faster than max Speed
        {
           myRb.AddForce(new Vector2(Input.GetAxis("Horizontal")*acceleration, 0), ForceMode2D.Force); //gets Input value and multiplies it by acceleration in the x direction.
        }

        //JUMP CODE
        if (isGrounded == true && Input.GetButtonDown("Jump")) // if the player is grounded and the jump button is pressed
        {
            myRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // add a force in the y direction
            StartCoroutine(SecondaryJump());
        }

        if (isGrounded == false && Input.GetButton("Jump") && secondaryJump == true)
        {
            myRb.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force); //while the jump button is held, add a force in the y direction
        }
        //END JUMP CODE
    }

    private void OnTriggerStay2D(Collider2D other) // as long as a collider is detected inside the trigger, the player is grounded
    {
        isGrounded = true;
    }
    
    private void OnTriggerExit2D(Collider2D other) // when the collider exits the trigger, the player is no longer grounded
    {
        isGrounded = false;
    }

    IEnumerator SecondaryJump()
    {
        secondaryJump = true;
        yield return new WaitForSeconds(secondaryJumpTime); // wait for a certain amount of time
        secondaryJump = false;
        //yield return null;
    }
}
