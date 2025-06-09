using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{   public static PlayerController instance;
    
    public float movespeed;
    public float jumpforce;
    public Rigidbody2D theRB;
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer theSR;

    public float knockBacklength, knockBackForce;
    private float knockBackCounter;
    public float bounceForce;
    public bool stopInput;
    

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pausemanu.instance.isPaused&&!stopInput)
        {
            if (knockBackCounter <= 0)
            {

                theRB.velocity = new Vector2(movespeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
               

                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpforce);
                        Audiomanager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpforce);
                            canDoubleJump = false;
                            Audiomanager.instance.PlaySFX(10);

                        }
                    }

                }

                if (theRB.velocity.x < 0)
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (!theSR.flipX)
                {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }
                else
                {
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }
            }
        }

    

        anim.SetFloat("moveSpeed",Mathf.Abs(theRB.velocity.x));//确保正负向播放动画正常
        anim.SetBool("isGrounded",isGrounded);
        
        
        
    }

    public void KnockBack()
    {
        knockBackCounter=knockBacklength;//碰撞时间
        theRB.velocity = new Vector2(0f, knockBackForce);
        
        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        Audiomanager.instance.PlaySFX(10);

    }
    
    
}
