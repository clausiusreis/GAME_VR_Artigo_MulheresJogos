using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Objeto a ser reposicionado (instância na cena)")]
    [SerializeField] private GameObject controlObject;

    [Header("Spawn points (6)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Opções")]
    [SerializeField] private bool detachFromParentBeforeMove = true;
    [SerializeField] private bool runAtEndOfFrame = true; // evita scripts que mexem em Start()

    private Vector3 originalScale;

    private void Awake()
    {
        // Se outros scripts reposicionarem em Start(), mover no fim do frame ajuda.
        if (!runAtEndOfFrame)
            RepositionNow();

        if (controlObject != null)
            originalScale = controlObject.transform.localScale;
    }

    private void Start()
    {
        if (runAtEndOfFrame)
            StartCoroutine(RepositionEndOfFrame());
    }

    private IEnumerator RepositionEndOfFrame()
    {
        yield return null; // espera 1 frame
        RepositionNow();
    }

    public void reposition() {
        RepositionNow();
    }

    private void RepositionNow()
    {
        if (controlObject == null)
        {
            Debug.LogError("[GameManager] controlObject não atribuído.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("[GameManager] spawnPoints vazio.");
            return;
        }

        int idx = Random.Range(0, spawnPoints.Length);
        Transform sp = spawnPoints[idx];

        Transform t = controlObject.transform;

        // Guarda escala ANTES de qualquer mudança estrutural
        Vector3 scaleBackup = t.localScale;

        // Rotação aleatória no plano XZ (em torno de Y)
        float randomYaw = Random.Range(0f, 360f);
        Quaternion finalRotation = Quaternion.Euler(0f, randomYaw, 0f);

        // Desparenta sem recalcular escala
        t.SetParent(null, false);

        // Debug para confirmar que está rodando
        Debug.Log($"[GameManager] Reposicionando '{controlObject.name}' para spawn #{idx} ({sp.position}).");

        if (detachFromParentBeforeMove && controlObject.transform.parent != null)
            controlObject.transform.SetParent(null, true);

        // Se tiver Rigidbody, mova via Rigidbody
        Rigidbody rb = controlObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Para evitar “briga” com a física no primeiro frame
            bool wasKinematic = rb.isKinematic;
            rb.isKinematic = true;

            rb.position = sp.position;
            rb.rotation = finalRotation;

            // Zera velocidades para não “sair voando”
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = wasKinematic;
        }
        else
        {
            controlObject.transform.SetPositionAndRotation(sp.position, finalRotation);
        }

        // RESTAURA ESCALA
        t.localScale = scaleBackup;
    }

}

