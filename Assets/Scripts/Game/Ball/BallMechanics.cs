using UnityEngine;

public class BallMechanics : MonoBehaviour
{
    [SerializeField]
    Transform ball;
    Rigidbody2D rb;
    int moveSpeed = 3;

    public void PushBallUpwards()
    {
        if(!rb) rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, moveSpeed);
    }

    private void OnEnable()
    {
        PushBallUpwards();
    }
    private void FixedUpdate()
    {
        SetConstantVelocity();
    }

    private void SetConstantVelocity()
    {
        rb.velocity = moveSpeed * (rb.velocity.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Brick")) 
        {
            collision.collider.GetComponent<BrickMechanics>().HitBrick();
        }

        if (collision.collider.CompareTag("Paddle")) 
        {
            MoveBallAccordingToCollisionPoint(collision);
            AudioManager.PlaySound("Paddle Blob");
        }
    }

    private void MoveBallAccordingToCollisionPoint(Collision2D collision) 
    {
        Transform paddle = collision.collider.transform;
        Vector2 collisionPoint = collision.contacts[0].point;
        Vector2 centerOfPaddle = new Vector2(paddle.position.x, paddle.position.y);
        float difference = centerOfPaddle.x - collisionPoint.x;

        rb.velocity = Vector2.zero;

        if (collisionPoint.x < centerOfPaddle.x)
        {
            rb.velocity = new Vector2(-Mathf.Abs(difference * 10), moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Abs(difference * 10), moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DecreaseLifeIfNoBallsAreLeft();
        gameObject.SetActive(false);
        DecreaseLifeIfNoBallsAreLeft();
    }

    private void DecreaseLifeIfNoBallsAreLeft()
    {
        if (GameManagers.BallManager.GetActiveBallAmount() == 0)
        {
            GameManagers.LivesManager.ReduceHeart();
            GameManagers.BallManager.ResetBall();
        }
    }
}
