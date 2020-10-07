using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Hardcoded for now
    private int MaxLevels = 2;

    public void SetGameOver()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        CSVLevelReader.Level = 0;
    }

    public void AdvanceToNextLevel()
    {
        CSVLevelReader.Level++;
        if (MaxLevels >= CSVLevelReader.Level)
        {
            CSVLevelReader.LoadCSVData(CSVLevelReader.Level);
            GameManagers.BrickManager.LoadBricksIntoScene();
            GameManagers.BallManager.ResetBall();
        }
        else
            SetGameOver();
    }
}
