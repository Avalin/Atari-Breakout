using Rewired;
using UnityEngine;

public class MovePaddle : MonoBehaviour
{
    private int playerID = 0;
    private Player player;
    [SerializeField] 
    private float movementSpeed;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movementSpeed = 0.2F;
    }

    private void FixedUpdate()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        float moveHorizontal = player.GetAxis("Move Horizontal");
        rb.MovePosition(new Vector2(transform.position.x + moveHorizontal * movementSpeed, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.Play("Quick_Blink");
    }
}
