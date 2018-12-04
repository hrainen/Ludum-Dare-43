using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchGameOver : MonoBehaviour {

    public bool levelFinished = false;
    public bool goToNextLevel = false;
    private string nextLevel = "GAME OVER";

    // Update is called once per frame
    void Update()
    {
        if (levelFinished && goToNextLevel)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void readyToTransition()
    {
        levelFinished = true;
        goToNextLevel = true;
    }

    public void resetScene()
    {
        SceneManager.LoadScene("Level1" +
            "");
    }
}
