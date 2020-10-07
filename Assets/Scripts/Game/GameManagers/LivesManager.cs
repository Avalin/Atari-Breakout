using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] 
    private Transform lives;
    private int livesCount;

    private void Start()
    {
        livesCount = 3;
    }

    public void ReduceHeart()
    {
        AudioManager.PlaySound("Sad Trombone");
        Transform heart = lives.GetChild(livesCount-1);
        livesCount--;
        heart.GetComponent<Animator>().Play("BeatingHeartGrey");
        if (livesCount == 0)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

}
