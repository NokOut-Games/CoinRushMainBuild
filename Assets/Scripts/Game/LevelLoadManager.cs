using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour
{
    public int currentSceneIndex;
    public int nextSceneIndex;
    string levelPrefix = "Level";

    public void LoadNextLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }



    public void LoadLevelOf(int inLevelIndex)
    {
        SceneManager.LoadScene(levelPrefix + inLevelIndex);
    }
    public void LoadLevelASyncOf(int inLevelIndex)
    {
        SceneManager.LoadScene(levelPrefix + inLevelIndex);
    }
    public void BacktoHome()
    {
        SceneManager.LoadScene(levelPrefix + GameManager.Instance._playerCurrentLevel);
        //Make the GameToLoad GameManager Data
        GameManager.Instance._IsRefreshNeeded = true;
    }

}
