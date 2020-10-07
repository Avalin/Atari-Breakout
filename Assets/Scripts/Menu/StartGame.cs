using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartButtonIsPressed() 
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
