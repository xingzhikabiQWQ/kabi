using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite cpOn, cpOff;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//如果与玩家碰撞，改变检查点图片并存储位置
        {
            CheckpointController.instance.DeactivateCheckpoints();
            theSR.sprite=cpOn;
            CheckpointController.instance.SetSpawnPoint(transform.position);

        }
    }
    
    public void ResetCheckpoint()
    {
        theSR.sprite = cpOff;
    }
    
}
