using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mappoint : MonoBehaviour
{
    public Mappoint up,right,left,down;

    public bool islevel, islocked;

    public string levelToLoad, levelToCheck,levelName;

    public int gemsCollected, totalGems;

    public float bestTime, targetTime;
    public GameObject gemBadge,timeBadge;
    // Start is called before the first frame update
    void Start()
    {
        if (islevel && levelToLoad != null)
        {
            if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
            }

            if (PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }

            if (gemsCollected >= totalGems)
            {
                gemBadge.SetActive(true);
            }

            if (bestTime <= targetTime&&bestTime!=0)
            {
                timeBadge.SetActive(true);
            }
            islocked = true;
            if (levelToCheck != null)
            {
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        islocked = false;
                    }
                    
                }
                
            }

            if (levelToLoad == levelToCheck)
            {
                islocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
