using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooMove : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    Rigidbody2D myRigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(movementSpeed, myRigidbody2D.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        movementSpeed *= -1;
        FlipBoo();
    }

    void FlipBoo()
    {
        Vector3 swap = transform.localScale;
        swap.x *= -1;
        transform.localScale = swap;
    }
}
