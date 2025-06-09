using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class stompbox : MonoBehaviour
{
    public GameObject deathEffect;
    public GameObject collectible;
   [Range(0,100f)] public float chanceToDrop;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            PlayerController.instance.Bounce();
            float dropSelect = Random.Range(0, 100f);
            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible,other.transform.position,other.transform.rotation);
            }
            Audiomanager.instance.PlaySFX(3);
            
            
        }
    }
}
