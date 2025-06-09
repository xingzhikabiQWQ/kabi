using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthControl : MonoBehaviour
{
    public static PlayerHealthControl instance;//即controller
    
    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    public GameObject deathEffect;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;//无敌时间的递减
            if (invincibleCounter <= 0)
            {
                theSR.color=new Color(theSR.color.r,theSR.color.g,theSR.color.b,1f);
            }
            
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //meObject.SetActive(false);
                Instantiate(deathEffect,transform.position,transform.rotation);
                levelmanager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color=new Color(theSR.color.r,theSR.color.g,theSR.color.b,0.5f);
                
                PlayerController.instance.KnockBack();
                Audiomanager.instance.PlaySFX(9);
            }

            UIcontroller.instance.UpdateHealthDisplay(); //获取组件，然后调用函数

        }
    }


    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        UIcontroller.instance.UpdateHealthDisplay();
    }
    
    
}
