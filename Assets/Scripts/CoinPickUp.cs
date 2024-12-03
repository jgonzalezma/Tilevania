using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpAudio;
    [SerializeField] int coinValue = 100;
    bool isTaken = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !isTaken)
        {
            isTaken = true;
            FindObjectOfType<GameSession>().AddToScore(coinValue);
            AudioSource.PlayClipAtPoint(coinPickUpAudio, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
