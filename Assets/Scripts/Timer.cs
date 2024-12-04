using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float levelTime = 60f;
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeRemaining;
    private bool isTimerRunning;

    void Start()
    {
        InitializeTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                TimerEnded();
            }
        }
    }

    public void InitializeTimer()
    {
        timeRemaining = levelTime; // Reinicia el tiempo según el inspector
        isTimerRunning = true;     // Activa el Timer
        UpdateTimerDisplay();      // Actualiza la interfaz
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void TimerEnded()
    {
        timerText.text = "00:00";
        FindAnyObjectByType<GameSession>().TakeLife();
    }
}
