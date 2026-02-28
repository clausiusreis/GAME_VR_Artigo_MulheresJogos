using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectToy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MeninaController child = other.GetComponent<MeninaController>();
        if (child != null)
        {
            child.ShowHappy();            
            Destroy(gameObject); // brinquedo some            
        }
    }
}

