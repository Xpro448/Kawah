
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
    float doubleTapTime;
    KeyCode lastKeyCode;



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

        // Dash

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                StartCoroutine(Dash(-1f));
                
                
                
            }

            else
            {
                doubleTapTime = Time.time + 0.5f;
            }

            lastKeyCode = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                StartCoroutine(Dash(1f));
                
            }

            else
            {
                doubleTapTime = Time.time + 0.5f;
            }

            lastKeyCode = KeyCode.D;


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.LeftArrow)
                {
                    StartCoroutine(Dash(1f));



                }

                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }

                lastKeyCode = KeyCode.LeftArrow;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.RightArrow)
                {
                    StartCoroutine(Dash(-1f));



                }

                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }

                lastKeyCode = KeyCode.RightArrow;
            }
        }

     void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsGround);
       
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        RB2D.velocity = new Vector2(RB2D.velocity.x, 0f);
        RB2D.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        RB2D.gravityScale = 0f;
        yield return new WaitForSeconds(0.1f);
        isDashing = false;
        RB2D.gravityScale = 3;
        RB2D.velocity = new Vector2 (0,0);
    }


    }
}
