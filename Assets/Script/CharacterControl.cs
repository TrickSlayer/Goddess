using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private string[] parameters = { "isCrounch", "isHurt", "isClimb", "isRun", "isJump", "isIdle" };
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        MoveControl();
        ActionControl();
    }

    public void MoveControl()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");
        gameObject.transform.Translate(horizontalMove * Time.deltaTime, verticalMove * Time.deltaTime, 0);
    }

    public void ActionControl()
    {
        var input = Input.inputString;
        switch (input)
        {
            case " ":
                SetAction("isJump");
                Debug.Log("Space");
                break;
            default:
                if (input != "")
                {
                    print("\'" + input + "\' was pressed");
                }
                break;
        }
    }

    private void SetAction(string parameter)
    {
        foreach (string param in parameters)
        {
            if (param == parameter)
            {
                animator.SetBool(param, true);
            }
            else
            {
                animator.SetBool(param, false);
            }
        }
    }
}
