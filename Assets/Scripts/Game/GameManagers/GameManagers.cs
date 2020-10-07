using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static AudioManager AudioManager;
    public static BallManager  BallManager;
    public static BrickManager BrickManager;
    public static LevelManager LevelManager;
    public static LivesManager LivesManager;
    public static ScoreManager ScoreManager;

    private void Start()
    {
        AudioManager = transform.Find("AudioManager").gameObject.GetComponent<AudioManager>();
        BallManager  = transform.Find("BallManager").gameObject.GetComponent<BallManager>();
        BrickManager = transform.Find("BrickManager").gameObject.GetComponent<BrickManager>();
        LevelManager = transform.Find("LevelManager").gameObject.GetComponent<LevelManager>();
        LivesManager = transform.Find("LivesManager").gameObject.GetComponent<LivesManager>();
        ScoreManager = transform.Find("ScoreManager").gameObject.GetComponent<ScoreManager>();
    }
}