using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public static Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;

    public bool isTestingMovement = false;
    public Vector2 testMovementDirection;

    SavePlayerPos playerPosData;

    private void Awake()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();
        playerPosData.PlayerPosLoad();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isTestingMovement)
        {
            movement = testMovementDirection;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    public void RestartPos()
    {
        rb.position = new Vector2(-4, -1);
        movement = Vector2.zero;
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetFloat("Speed", 0);
    }
}
