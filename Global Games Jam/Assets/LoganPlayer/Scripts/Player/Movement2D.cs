using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class Movement2D : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40.0f;
    private float horizontalMove = 0.0f;
    private bool jump = false;
    private bool dive = false;
    //private bool crouch = false;
    //private bool fire = false;
    private float horizontalStore;

    public bool nonStop = false;
    public bool reverse = false;
    public float playerNumber = 1;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        //Gets the players horizontal direction
        horizontalMove = Input.GetAxis("Horizontal" + playerNumber) * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (horizontalMove != 0f)
        {
            //FindObjectOfType<AudioManager>().Play("PlayerRun");
            horizontalStore = horizontalMove;
        }
        
        //If nonstop is applied, keep player moving in last known direction
        if (nonStop)
        {
            // Set values manually to ensure full speed
            if(horizontalStore < 0)
            {
                horizontalMove = -1 * runSpeed;
            }
            else
            {
                horizontalMove = 1 * runSpeed;
            }
        }

        //If reverse is applied, reverse horizontal controls
        if(reverse)
        {
            horizontalMove *= -1;
        }

        if (controller.m_Grounded)
        {
            jump = false;
            animator.SetBool("IsJumping", false);
            //Gets if the player should jumping
            if (Input.GetButton("Jump" + playerNumber))
            {
                jump = true;
                FindObjectOfType<AudioManager>().Play("Jump");
                animator.SetBool("IsJumping", true);
            }

        }

        if (!controller.m_Grounded)
        {
            //Gets if the player should dive

            bool shouldDive = Input.GetButton("Dive" + playerNumber) ||
                Input.GetAxis("Vertical" + playerNumber) > 0;

            if (shouldDive)
            {
                dive = true;
            }
            else
            {
                dive = false;
            }
        }

        //animator.SetFloat("Speed",Mathf.Abs(horizontalMove));
        //Gets if the player should jumping
        // if (Input.GetButtonDown("Jump"))
        // {
        // 	jump = true;
        // }
        // else
        // {
        // 	jump = false;
        // }

        /*
		if(Input.GetButtonDown("Cancel"))
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
		*/

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    void FixedUpdate()
    {
        //Passes all inputs to CharacterController2D
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dive, runSpeed);
    }
}