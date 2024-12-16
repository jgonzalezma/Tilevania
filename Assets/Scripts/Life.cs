using UnityEngine;

public class Life : MonoBehaviour
{
    GameSession gameSession;
    bool isTaken = false;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isTaken)
        {
            isTaken = true;

            if (gameSession.playerLifes < gameSession.numOfHearts)
            {
                gameSession.playerLifes++;
            }

            Destroy(gameObject);
        }
    }
}
