using UnityEngine;
using TMPro;

public class FindObjectTimerUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public TMP_Text timerText;

    [Header("Config")]
    [SerializeField] public float durationSeconds = 60f;

    private float remaining;
    private bool running;

    private void OnEnable()
    {
        StartTimer(durationSeconds);
    }

    public void StartTimer(float seconds)
    {
        remaining = Mathf.Max(0f, seconds);
        running = true;
        UpdateUI();
    }

    public void StopTimer()
    {
        running = false;
    }

    private void Update()
    {
        if (!running) return;

        remaining -= Time.deltaTime;
        if (remaining <= 0f)
        {
            remaining = 0f;
            running = false;
            UpdateUI();
            OnTimeUp();
            return;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText == null) return;

        int sec = Mathf.CeilToInt(remaining);
        int minutes = sec / 60;
        int seconds = sec % 60;

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnTimeUp()
    {
        GetComponent<AudioSource>().Play();
        Time.timeScale = 0f;
    }
}
