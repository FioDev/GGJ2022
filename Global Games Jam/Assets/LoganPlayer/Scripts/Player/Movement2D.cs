using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class Movement2D : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40.0f;

    private bool moving = false;
    private float horizontalMove = 0.0f;
    private bool jump = false;
    //private bool crouch = false;
    //private bool fire = false;

    public float playerNumber = 1;

	public Animator animator;

    // Update is called once per frame
    void Update()
    {
        //Gets the players horizontal direction
        horizontalMove = Input.GetAxisRaw("Horizontal" + playerNumber) * runSpeed;
		animator.SetFloat("Speed",Mathf.Abs(horizontalMove));
        if (horizontalMove != 0.0f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        /*
		if(Input.GetButton("Left") && Input.GetButton("Right"))
		{
			moving = false;
		}
		*/

        if (controller.m_Grounded)
        {
            jump = false;
			animator.SetBool("IsJumping", false);
            //Gets if the player should jumping
            if (Input.GetButton("Jump" + playerNumber))
            {
                jump = true;
				animator.SetBool("IsJumping",true);
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, runSpeed, moving);
    }
}