using UnityEngine;

public class AutoHeightAdjust : MonoBehaviour
{
    [Header("Referências")]
    public Transform centerEyeAnchor;      // Arraste o CenterEyeAnchor do rig
    public CapsuleCollider capsule;        // CapsuleCollider do PlayerController
    public float minHeight = 0.9f;         // Altura mínima (agachado)
    public float maxHeight = 2.1f;         // Altura máxima (em pé)
    public float offsetY = 0.0f;           // Pequeno ajuste opcional

    void Reset()
    {
        capsule = GetComponent<CapsuleCollider>();

        // Calcula altura relativa da cabeça ao chão (posição local Y)
        float headHeight = Mathf.Clamp(centerEyeAnchor.localPosition.y, minHeight, maxHeight);
        capsule.height = headHeight;

        // Centraliza o collider de forma que a base toque o chão
        Vector3 center = capsule.center;
        center.y = (capsule.height / 2f) + offsetY;
        capsule.center = center;
    }

    /*void LateUpdate()
    {
        if (centerEyeAnchor == null || capsule == null) return;

        // Calcula altura relativa da cabeça ao chão (posição local Y)
        float headHeight = Mathf.Clamp(centerEyeAnchor.localPosition.y, minHeight, maxHeight);
        capsule.height = headHeight;

        // Centraliza o collider de forma que a base toque o chão
        Vector3 center = capsule.center;
        center.y = (capsule.height / 2f) + offsetY;
        capsule.center = center;
    }*/
}
