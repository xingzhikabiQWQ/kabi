using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LsPlayer : MonoBehaviour
{
    public Mappoint currentPoint;

    public float moveSpeed = 10f;

    private bool levelLoading;

    public LsManager Themanager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position,
            moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f&&!levelLoading)
        {
            if (Input.GetAxisRaw("Horizontal") > .5f)
            {
                if (currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }

            if (Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }

            if (Input.GetAxisRaw("Vertical") > .5f)
            {
                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }

            if (Input.GetAxisRaw("Vertical") < -.5f)
            {
                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            if (currentPoint.islevel&&currentPoint.levelToLoad!=""&&!currentPoint.islocked)
            {
                LSUIcontroller.instance.ShowInfo(currentPoint);
                if (Input.GetButtonDown("Jump"))
                {
                    
                    levelLoading = true;
                    Themanager.LoadLevel();
                }
            }
        }
    }

    public void SetNextPoint(Mappoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUIcontroller.instance.HideInfo();
		Audiomanager.instance.PlaySFX(5);
    }
}
