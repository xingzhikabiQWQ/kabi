using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;
    public AudioSource[] soundEffects;
	public AudioSource bgm,levelEndMusic,bossMusic;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();
        soundEffects[soundToPlay].Play();
    }

    public void PlayLevelVictory()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }

    public void PlayBossMusic()
    {
        bgm.Stop();
        bossMusic.Play();
        
    }

    public void StopBossMusic()
    {
        bossMusic.Stop();
        bgm.Play();
    }
}
