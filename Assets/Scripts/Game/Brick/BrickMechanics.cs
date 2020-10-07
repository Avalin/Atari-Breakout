using UnityEngine;

public class BrickMechanics : MonoBehaviour
{
    public int HitsToBreak { get; set; }
    public bool IsPowerUp { get; set; }

    private Animator animator;
    [SerializeField]
    private GameObject ball;

    private void Start()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        animator = GetComponent<Animator>();
        if (IsPowerUp)
        {
            animator.enabled = false;
            GetComponent<SpriteRenderer>().color = Color.cyan;
            HitsToBreak = 1;
        }
        if (HitsToBreak == 0) Destroy(gameObject);
    }

    public void HitBrick()
    {
        HitsToBreak--;
        GameManagers.ScoreManager.AddToScore();
        GetComponent<SpriteRenderer>().sprite = Resources.Load("Graphics/Game/Brick/BrickBroken"+HitsToBreak, typeof(Sprite)) as Sprite;
        if (HitsToBreak == 0)
        {
            RemoveBrick();
            GameManagers.BrickManager.CheckIfBricksAreCleared();
        }

        if (IsPowerUp)
        {
            AudioManager.PlaySound("Ka-ching!");
            HitPowerUp();
        }
        else AudioManager.PlaySound("Blob");
    }

    void HitPowerUp()
    {
        int coinFlip = Random.Range(0, 1);
        GameManagers.BrickManager.DoFunkyPowerUpThingy();

        //In another revision this functionality could be added
        coinFlip = 0;
        if (coinFlip == 0)
        {
            GameManagers.BallManager.AddBallToGame();
            GameManagers.BallManager.AddBallToGame();
        }
        else
        {
            ScalePaddle paddleScaler = GameObject.FindWithTag("Paddle").GetComponent<ScalePaddle>();
            //paddleScaler.StretchPaddle();
            //Invoke(nameof(paddleScaler.SquishPaddle), 1f);
        }
    }

    void RemoveBrick() 
    {
        GameManagers.BrickManager.Bricks.Remove(gameObject);
        Destroy(gameObject);
    }
}
