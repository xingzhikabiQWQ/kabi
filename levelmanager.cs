using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelmanager : MonoBehaviour
{
   public static levelmanager instance;
   public float waitToRespawn;
   public int gemsCollected;
   public string levelToload;
   public float timeInlevel;
   

   private void Awake()
   {
       instance = this;
   }

   void Start()
   {
       timeInlevel = 0f;

   }

    // Update is called once per frame
    void Update()
    {
        timeInlevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
        
    }

    private IEnumerator RespawnCo()//协程
    {
        
        PlayerController.instance.gameObject.SetActive(false);
        Audiomanager.instance.PlaySFX(8);
        
        yield return new WaitForSeconds(waitToRespawn-(1f/UIcontroller.instance.fadeSpeed));
        UIcontroller.instance.FadeToBlack();
        yield return new WaitForSeconds((10f/UIcontroller.instance.fadeSpeed)*.2f);
        UIcontroller.instance.FadeFromBlack();
        PlayerController.instance.gameObject.SetActive(true);
        
        PlayerController.instance.transform.position=CheckpointController.instance.spawnPoint;

        PlayerHealthControl.instance.currentHealth = PlayerHealthControl.instance.maxHealth;
        UIcontroller.instance.UpdateHealthDisplay();


    }

    public void Endlevel()
    {
        StartCoroutine(EndlevelCo());
    }

    public IEnumerator EndlevelCo()
    {
        Audiomanager.instance.PlayLevelVictory();
        PlayerController.instance.stopInput = true;
        cameraContronler.instance.stopFollow = true;
        UIcontroller.instance.levelCompleteText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIcontroller.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIcontroller.instance.fadeSpeed) * 3f);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel",SceneManager.GetActiveScene().name);
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if (timeInlevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInlevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInlevel);
        }
        
        SceneManager.LoadScene(levelToload);
    }
}
