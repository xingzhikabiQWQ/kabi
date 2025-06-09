using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public bool isGem,isHeal;
    private bool isCollected;
    public GameObject pickupEffect;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&!isCollected)
        {
            if (isGem)
            {
                levelmanager.instance.gemsCollected++;
                isCollected=true;
                Destroy(gameObject);
                
                Instantiate(pickupEffect,transform.position,transform.rotation);
                
                UIcontroller.instance.UpdateGemCount();
                Audiomanager.instance.PlaySFX(6);
            }

            if (isHeal)
            {
                if (PlayerHealthControl.instance.currentHealth != PlayerHealthControl.instance.maxHealth)
                {
                    PlayerHealthControl.instance.HealPlayer();
                    isCollected=true;
                    Destroy(gameObject);
                    Instantiate(pickupEffect,transform.position,transform.rotation);
                    Audiomanager.instance.PlaySFX(7);
                    
                }
            }
			
        }
    }
    
    
}
