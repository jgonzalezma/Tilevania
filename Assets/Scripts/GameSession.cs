using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLifes = 3;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    [SerializeField] int score = 0;
    [SerializeField] float gameOverDelay = 0.5f;
    [SerializeField] TextMeshProUGUI vidasText;
    [SerializeField] TextMeshProUGUI scoreText;

    // Timer
    [SerializeField] float startTime = 60; // Tiempo inicial en segundos
    private float timeRemaining;
    [SerializeField] TextMeshProUGUI timerText;
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        vidasText.text = playerLifes.ToString();
        scoreText.text = score.ToString();
        timeRemaining = startTime;
        timerIsRunning = true;
        UpdateTimerText();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLifes > 1)
        {
            TakeLife();
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    void TakeLife()
    {
        playerLifes--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        vidasText.text = playerLifes.ToString();
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(gameOverDelay);
        FindObjectOfType<ScenePersistence>().DeleteScenePersistance();
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    // Metodos para el timer
    private bool timerIsRunning = false;

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                timerText.text = "0";
                TimerEnded();
            }
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (playerLifes > numOfHearts)
            {
                playerLifes = numOfHearts;
            }
            if (i < playerLifes)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60); // Convierte a minutos
        int seconds = Mathf.FloorToInt(timeRemaining % 60); // Obtén los segundos restantes
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Formato MM:SS
    }

    void TimerEnded()
    {
        playerLifes = 0;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
        Debug.Log("¡El tiempo se ha acabado!");
    }

    public void ResetTimer()
    {
        timeRemaining = startTime; // Reinicia el tiempo al valor inicial
        timerIsRunning = true;
        UpdateTimerText(); // Actualiza el texto inmediatamente
    }

    public void SetStartTime(float newStartTime)
    {
        startTime = newStartTime; // Configura el nuevo tiempo de inicio
        ResetTimer(); // Reinicia el temporizador con el nuevo tiempo
    }
}
