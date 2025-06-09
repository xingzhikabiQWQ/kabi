using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossbullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Audiomanager.instance.PlaySFX(2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=new Vector3(-speed*transform.localScale.x*Time.deltaTime,0,0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {   
            PlayerHealthControl.instance.DealDamage();
        }
        Audiomanager.instance.PlaySFX(1);
        Destroy(gameObject);
    }
}
