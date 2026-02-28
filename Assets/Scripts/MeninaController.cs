using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeninaController : MonoBehaviour
{
    [Header("Acesso a TV")]
    public SpriteGroupCycler cycler;

    [Header("Referências")]
    public GameObject normalChild;
    public GameObject happyChild;

    public AudioSource som;

    public event Action OnBecomeHappy;
    public event Action OnBecomeNormal;

    void Start()
    {
        ShowNormal();        
    }

    public void ShowNormal()
    {
        // TV jogando sozinho
        cycler.SetGroup(SpriteGroupCycler.Group.Group1);
        cycler.Play();

        normalChild.SetActive(true);
        happyChild.SetActive(false);
        OnBecomeNormal?.Invoke();
    }

    public void ShowHappy()
    {
        // TV jogando juntos
        cycler.SetGroup(SpriteGroupCycler.Group.Group2);
        cycler.Play();

        som.Play();

        normalChild.SetActive(false);
        happyChild.SetActive(true);
        OnBecomeHappy?.Invoke();

        Time.timeScale = 0f;
    }
}


