using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LsManager : MonoBehaviour
{
    public LsPlayer thePlayer;
    private Mappoint[] allPoints;
    // Start is called before the first frame update
    void Start()
    {
        allPoints = FindObjectsOfType<Mappoint>();
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach (Mappoint point in allPoints )
            {
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }   
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }
    
    public IEnumerator LoadLevelCo()
    {
        Audiomanager.instance.PlaySFX(4);
        LSUIcontroller.instance.FadeToBlack();
        
        yield return new WaitForSeconds((10f/LSUIcontroller.instance.fadeSpeed)*.25f);
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
