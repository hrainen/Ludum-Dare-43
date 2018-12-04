using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour {

    public bool levelFinished = false;
    public bool goToNextLevel = false;
    private string nextLevel;
    public string CurrentLevel;

	// Update is called once per frame
	void Update () {
		if ( levelFinished && goToNextLevel)
        {
            SceneManager.LoadScene(nextLevel);
        }
	}

    public void readyToTransition( string newLevel )
    {
        nextLevel = newLevel;
        levelFinished = true;
        goToNextLevel = true;
    }

    public void resetScene( )
    {
        SceneManager.LoadScene( CurrentLevel );
    }
}
