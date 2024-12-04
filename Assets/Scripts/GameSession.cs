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

    private void Update()
    {
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
}
