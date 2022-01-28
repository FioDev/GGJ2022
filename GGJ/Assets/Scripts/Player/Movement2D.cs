﻿using System.Collections;
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
	private bool jump = false;
	private bool crouch = false;
	private bool fire = false;

	// Update is called once per frame
	void Update()
	{
		//Gets the players horizontal direction
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if(Input.GetButton("Left") || Input.GetButton("Right"))
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


		//Gets if the player should jumping
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
		else if (Input.GetButtonUp("Jump"))
		{
			jump = false;
		}

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