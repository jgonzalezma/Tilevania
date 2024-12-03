using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField] float kamikazeSpeed = 10f;
    Rigidbody2D myRigidbody2D;
    PlayerMove playerMove;
    float xMovement;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playerMove = FindObjectOfType<PlayerMove>();
        xMovement = playerMove.transform.localScale.x * kamikazeSpeed;
    }
    private void Update()
    {
        myRigidbody2D.velocity = new Vector2(xMovement, myRigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        if (other.gameObject.tag == "Enemigo")
        {
                Destroy(other.gameObject);
        }
    }
}
