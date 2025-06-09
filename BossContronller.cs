using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContronller : MonoBehaviour
{
    public enum bossStates
    {
        shooting,
        hurt,
        moving,
        ended
    };
    public bossStates currentState;
    public Transform theBoss;

    public Animator anim;
    [Header("movement")]//添加小标签,不能在两个变量的上方加小标题
    public float movespeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;
    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    private float shotCounter;
    public float timeBetweenShots;
    [Header("hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")] 
    public int health = 5;

    public GameObject explosion,winPlatform;

    private bool isDefeated;

    public float shotSpeedUp, mineSpeedUp;
    // Start is called before the first frame update
    void Start()
    {
        currentState = bossStates.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:
                shotCounter-= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    var newBullet=Instantiate(bullet,firePoint.position, firePoint.rotation);//在正确的位置创建子弹
                    newBullet.transform.localScale = theBoss.localScale;
                }
                break;
            case bossStates.hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;
                    if (hurtCounter <= 0)
                    {
                        currentState=bossStates.moving;
                        mineCounter=0;
                        if (isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            winPlatform.SetActive(true);
                            Audiomanager.instance.StopBossMusic();
                            currentState = bossStates.ended;
                        }
                    }
                }
                break;
            case bossStates.moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(movespeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one;
                        moveRight=false;
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(movespeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale=new Vector3(-1f,1f,1f);//scale的反转不用，反转函数是为了保证开火点的跟随
                        moveRight = true;
                        EndMovement();
                    }
                }
                mineCounter-= Time.deltaTime;
                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine,minePoint.position,minePoint.rotation);
                }
                break;
        }
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
        #endif
    }

    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;
        anim.SetTrigger("Hit");
        Audiomanager.instance.PlaySFX(0);
        BossMine[]mines=FindObjectsOfType<BossMine>();
        if (mines.Length > 0)
        {
            foreach (BossMine foundMine in mines )
            {
                foundMine.Explode();
            }
        }

        health--;
        if (health <= 0)
        {
            isDefeated=true;
        }
        else
        {
            timeBetweenShots/=shotSpeedUp;
            timeBetweenMines/=mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentState=bossStates.shooting;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }
}

