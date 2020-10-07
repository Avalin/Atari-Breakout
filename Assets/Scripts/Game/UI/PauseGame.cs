using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    bool isPaused;
    [SerializeField] Image btnImage;

    public void PauseGamePressed() 
    {
        if (isPaused) OnResume();
        else OnPause();
    }

    void OnPause() 
    {
        Time.timeScale = 0;
        btnImage.sprite = Resources.Load<Sprite>("Graphics/Game/UI/Buttons/Resume");
        isPaused = true;
    }

    void OnResume()
    {
        Time.timeScale = 1;
        btnImage.sprite = Resources.Load<Sprite>("Graphics/Game/UI/Buttons/Pause");
        isPaused = false;
    }
}
