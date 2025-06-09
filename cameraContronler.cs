using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraContronler : MonoBehaviour
{
    public static cameraContronler instance;
    // 声明一个公有 Transform 类型的变量 target，用于指定相机要跟随的目标（比如游戏角色）
    public Transform target;
    // 声明两个公有 Transform 类型的变量 farBackground 和 middleBackground，用于表示不同层次的背景对象
    public Transform farBackground, middleBackground;
    // 声明一个私有 float 类型的变量 lastXPos，用于记录相机上一帧的 X 坐标位置
    public float minHeight, maxHeight;
    
    // private float lastXPos;
    private Vector2 lastPos;
    // Start 方法在脚本实例化时调用，用于初始化操作
    public bool stopFollow;
    void Start()
    {
        // 将当前相机的 x 坐标值赋给 lastXPos，获取相机初始位置的 x 坐标
        //lastXPos = transform.position.x;
        lastPos=transform.position;
    }

    // Update 方法在每一帧都会被调用，用于处理每一帧的逻辑
    private void Awake()
    {
        instance=this;
    }

    void Update()
    {
        /*transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);

        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x,clampedY,transform.position.z);*/
        if (!stopFollow)
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight),
                transform.position.z); //为了确保相机当中的最高和最低位置


            //float amountToMoveX = target.position.x - lastXPos;
            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0) * .5f;
            // lastXPos=transform.position.x;
            lastPos = transform.position;
        }
    }
}