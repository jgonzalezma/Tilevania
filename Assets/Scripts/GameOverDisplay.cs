using TMPro;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "Puntuacion: " + GameSession.FinalScore.ToString();
    }
}
