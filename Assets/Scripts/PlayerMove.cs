using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D cuerpoCollider;
    BoxCollider2D piesCollider;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] Vector2 spikesDeadKick = new Vector2(10f, 10f);
    [SerializeField] GameObject kamikaze;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shakeTimer = 1.0f;
    [SerializeField] float shakeIntensity = 5.0f;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    float gravityAtStart;
    bool isAlive = true;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        cuerpoCollider = GetComponent<CapsuleCollider2D>();
        piesCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = myRigidbody2D.gravityScale;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        OnClimbing();
        Dead();    
    }

    // Funcion de movimiento del personaje
    void Run()
    {
        myRigidbody2D.velocity = new Vector2(moveInput.x * movementSpeed, myRigidbody2D.velocity.y);
        // Si el personaje tiene movimiento horizontal (si se mueve), le pone la animación de correr, sino se queda en idle
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRuning", playerHasHorizontalSpeed);
    }

    // Funcion para girar al personaje izquierda/derecha
    void FlipSprite()
    {
        bool playerHorizontalMovement = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }

    // Funcion de moverse, por defecto con el Input System
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        // (1,0) derecha (-1,0) izquierda (0,1) arriba (0,-1) abajo
    }

    // Funcion de salto, la variable OnJump ha sido creada con el Input System (descargado con el package manager)
    void OnJump(InputValue value)
    {
        if (value.isPressed && isAlive && piesCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpSpeed);
        }
    }

    void OnClimbing()
    {
        if (!piesCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody2D.gravityScale = gravityAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, moveInput.y * climbSpeed);
        myRigidbody2D.gravityScale = 0f;
        bool playerVerticalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerVerticalSpeed);
    }

    void Dead()
    {
        if ((cuerpoCollider.IsTouchingLayers(LayerMask.GetMask("Enemigos", "Spikes"))))
        {
            if (shakeTimer <= 0f)
            {
                ShakeCamera(shakeIntensity, shakeTimer);
                isAlive = false;
                myAnimator.SetTrigger("dead");
                myRigidbody2D.velocity = deathKick;
                shakeTimer -= Time.deltaTime;
            }
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;
        Instantiate(kamikaze, shootPoint.position, shootPoint.rotation);
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
}
