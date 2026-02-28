using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteGroupCycler : MonoBehaviour
{
    public enum Group { Group1, Group2 }

    // ====== DADOS SERIALIZADOS ======
    [System.Serializable]
    public class SpriteGroup
    {
        public string groupName = "Novo Grupo";
        public List<Sprite> sprites = new List<Sprite>();
    }

    [Header("Configuração")]
    [Min(0.01f)] public float intervalSeconds = 0.25f;
    public bool playOnEnable = true;
    public bool shuffle = false;

    [Header("Grupos (editáveis no Inspector)")]
    public SpriteGroup group1 = new SpriteGroup { groupName = "Grupo 1" };
    public SpriteGroup group2 = new SpriteGroup { groupName = "Grupo 2" };

    [Header("Estado (read-only)")]
    [SerializeField] private Group currentGroup = Group.Group1;
    [SerializeField] private int currentIndex = 0;

    private SpriteRenderer sr;
    private Coroutine cycleRoutine;
    private System.Random rng;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rng = new System.Random();
    }

    void OnEnable()
    {
        if (playOnEnable) Play();
    }

    void OnDisable()
    {
        Stop();
    }

    // === API ===
    public void SetGroup(Group group)
    {
        currentGroup = group;
        ResetIndex();
        if (isActiveAndEnabled)
        {
            if (cycleRoutine != null) { StopCoroutine(cycleRoutine); }
            cycleRoutine = StartCoroutine(Cycle());
        }
    }
    public void SetGroupByNumber(int n) => SetGroup(n == 2 ? Group.Group2 : Group.Group1);

    public void Play()
    {
        if (cycleRoutine == null) cycleRoutine = StartCoroutine(Cycle());
    }

    public void Pause() => Stop();

    public void StopAndReset()
    {
        Stop();
        ResetIndex();
    }

    public void NextNow()
    {
        StepIndex();
        ApplySprite();
    }

    public void SetInterval(float seconds) => intervalSeconds = Mathf.Max(0.01f, seconds);

    // === Internos ===
    private void Stop()
    {
        if (cycleRoutine != null)
        {
            StopCoroutine(cycleRoutine);
            cycleRoutine = null;
        }
    }

    private IEnumerator Cycle()
    {
        if (!ApplySprite()) yield break;

        while (true)
        {
            yield return new WaitForSeconds(intervalSeconds);
            StepIndex();
            if (!ApplySprite())
            {
                Stop();
                yield break;
            }
        }
    }

    private void ResetIndex()
    {
        currentIndex = 0;
        if (shuffle)
        {
            var list = GetActiveList();
            if (list != null && list.Count > 0)
                currentIndex = rng.Next(0, list.Count);
        }
    }

    private void StepIndex()
    {
        var list = GetActiveList();
        if (list == null || list.Count == 0) return;

        if (shuffle)
        {
            int next = list.Count == 1 ? 0 : rng.Next(0, list.Count);
            if (list.Count > 1)
                while (next == currentIndex) next = rng.Next(0, list.Count);
            currentIndex = next;
        }
        else
        {
            currentIndex = (currentIndex + 1) % list.Count;
        }
    }

    private bool ApplySprite()
    {
        var list = GetActiveList();
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning($"{name}: grupo {currentGroup} está vazio.");
            return false;
        }

        currentIndex = Mathf.Clamp(currentIndex, 0, list.Count - 1);
        if (sr != null) sr.sprite = list[currentIndex];
        return true;
    }

    private List<Sprite> GetActiveList()
    {
        return currentGroup == Group.Group1 ? group1.sprites : group2.sprites;
    }
}
