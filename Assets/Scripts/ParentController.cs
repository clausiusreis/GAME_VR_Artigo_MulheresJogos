using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ParentController : MonoBehaviour
{
    [Header("Referências (arraste no Inspector)")]
    public GameObject SadMother;   // assign the SadMother GameObject
    public GameObject HappyMother; // assign the HappyMother GameObject

    [Header("Configurações")]
    // Troque Child por ChildController se sua classe for ChildController
    public MeninaController childController;

    void Start()
    {
        ShowNormal();

        if (childController == null)
        {
            // se a classe se chama ChildController, troque abaixo:
            childController = FindObjectOfType<MeninaController>();
        }

        if (childController != null)
        {
            childController.OnBecomeHappy += ShowHappy;
            childController.OnBecomeNormal += ShowNormal;
        }
        else
        {
            Debug.LogWarning("Mother: childController não encontrado (verificar nome da classe).");
        }
    }

    void OnDestroy()
    {
        if (childController != null)
        {
            childController.OnBecomeHappy -= ShowHappy;
            childController.OnBecomeNormal -= ShowNormal;
        }
    }

    public void ShowNormal()
    {
        if (SadMother != null) SadMother.SetActive(true);
        if (HappyMother != null) HappyMother.SetActive(false);
    }

    public void ShowHappy()
    {
        if (SadMother != null) SadMother.SetActive(false);
        if (HappyMother != null) HappyMother.SetActive(true);
    }
}

