using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;
    int score;
    void Start()
    {
        score = 0;
    }

    public void AddToScore() 
    {
        score++;
        scoreText.text = score+"";
    }
}
