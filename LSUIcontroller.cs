using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIcontroller : MonoBehaviour
{
    public static LSUIcontroller instance;
    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;

    public Text levelName, gemsFound, gemsTarget, bestTime, timeTarget;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    public void ShowInfo(Mappoint levelInfo)
    {
        levelName.text = levelInfo.levelName;
        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text="IN LEVEL: "+levelInfo.totalGems;
        timeTarget.text = "TARGET: "+levelInfo.targetTime+"s";
        if (levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST TIME:---- ";
        }
        else
        {
            bestTime.text = "BEST TIME: "+levelInfo.bestTime.ToString("F1")+"s";
        }
        
        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }

}
