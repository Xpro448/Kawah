using System.Collections;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float MoveSpeed, JumpForce;

    public bool Jumping;

    public Rigidbody2D RB2D;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask WhatIsGround;

    private int extraJump;
    public int extraJumpValue;

    public float dashDistance = 15f;
    bool isDashing;
    bool PressShift;
    



    void Start()
    {
        extraJump = extraJumpValue;

        RB2D = GetComponent<Rigidbody2D>();

        MoveSpeed = 10f;
        JumpForce = 13f;

        Jumping = true;




    }
    // Update is called once per frame
    void Update()
    {
        //movement   
        var mouvement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(mouvement, 0, 0) * Time.deltaTime * MoveSpeed;


        // Jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(RB2D.velocity.y) < 0.001f)
        {
            RB2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        // double jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0)
        {
            RB2D.velocity = Vector2.up * JumpForce;
            extraJump--;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump == 0 && isGrounded == true)
        {
            RB2D.velocity = Vector2.up * JumpForce;
        }


        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }

        // appelle de la fonction Shift

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PressShift = true;

        }

        else
        {
            PressShift = false;

        }

       
        //dash
        if(PressShift == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(-dashDistance, 0, 0);
                

            }

        }

        if (PressShift == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += new Vector3(dashDistance, 0, 0);


            }

        }

        if (PressShift == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-dashDistance, 0, 0);


            }

        }

        if (PressShift == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(dashDistance, 0, 0);


            }

        }

        void FixedUpdate()
        {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsGround);
       
        }

  
    }


} 
