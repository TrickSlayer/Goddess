using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            Debug.Log(true);
            crouch = true;
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            crouch = false;
        }

    }

    public void OnLanding()
    {
        jump = false;

        animator.SetBool("isJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        Debug.Log("Call");
        animator.SetBool("isCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);


    }

}
