﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float moveSpeed;
    private Animator animator;
    private bool playerMoving;
    private Vector2 lastMove;
    private Rigidbody2D rb2d;
    private static bool thePlayer;
    private bool attack;
    public float attackDuration;
    private float adCounter;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        //Use Unitys build in feature for collision and we will use the rigid body to move the charachter instead
        //This gives teh player a box for collisions with other objects in the game
        rb2d = GetComponent<Rigidbody2D>();

        if(!thePlayer)
        {
            thePlayer = true;
            //keep the object that this script is connected to
            //will put it at the same x and y position between worlds
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Keeps unity from destroying player and camera location when going from scene to scene
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerMoving = false;

        if (!attack)
        {

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                //transform uses decimals so we use a float
                //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                //Using rigidbidy to move player, Vector2 Param are X * movespeed to control how fast the player moves and Y of player
                rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb2d.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }
            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                //transform uses decimals so we use a float
                //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                rb2d.velocity = new Vector2(rb2d.velocity.y, Input.GetAxisRaw("Vertical") * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            //improvement on player movement, fix for player gliding
            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                adCounter = attackDuration;
                attack = true;
                rb2d.velocity = Vector2.zero;
                animator.SetBool("isAttacking", true);
            }
        }
            if(adCounter > 0)
            {
                adCounter -= Time.deltaTime;
            }

            if(adCounter <= 0)
            {
                attack = false;
                animator.SetBool("isAttacking", false);
            }
        
        //Accessing animator
        animator.SetFloat("axisX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("axisY", Input.GetAxisRaw("Vertical"));
        animator.SetBool("isMoving", playerMoving);
        animator.SetFloat("facingX", lastMove.x);
        animator.SetFloat("facingY", lastMove.y);


    }
}
