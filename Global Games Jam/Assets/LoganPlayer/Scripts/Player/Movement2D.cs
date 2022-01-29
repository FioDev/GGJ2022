using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Movement2D : MonoBehaviour
{

	public CharacterController2D controller;

	public float runSpeed = 40f;

	private bool moving = false;
	private float horizontalMove = 0f;
	private float horizontalStore;
	private bool jump = false;
	public bool nonStop = false;
	public int playerNumber = 1;

	// Update is called once per frame
	void Update()
	{
		//Gets the players horizontal direction
		horizontalMove = Input.GetAxisRaw("Horizontal"+ playerNumber) * runSpeed;

		if(horizontalMove != 0f)
		{
			horizontalStore = horizontalMove;
			moving = true;
		}
		else
		{
			moving = false;
		}

		if(nonStop)
		{
			horizontalMove = horizontalStore;
		}

		/*
		if(Input.GetButton("Left") && Input.GetButton("Right"))
		{
			moving = false;
		}
		*/

        if(controller.m_Grounded)
        {
            jump = false;
            //Gets if the player should jumping
            if (Input.GetButton("Jump"+ playerNumber))
            {
                jump = true;
            }

        }
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

	void FixedUpdate()
	{
		//Passes all inputs to CharacterController2D
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, runSpeed, moving);
	}
}