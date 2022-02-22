using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

public class LevelLoadManager : MonoBehaviour
{
    public static LevelLoadManager instance;
    [SerializeField] GameObject mCanvas;
    [SerializeField] Animator mCloudAnimator;
    string levelPrefix = "Level";
    Tutorial tutorial;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void LoadLevelOf(int inLevelIndex)
    {
        SceneManager.LoadScene(levelPrefix + inLevelIndex);
    }
    public void GoToMapScreen(bool hasChoise =false)
    {
        GameManager.Instance.hasChoiceInLevel = hasChoise;
        SceneManager.LoadScene("Map");

    }
    public void LoadLevelASyncOf(string inLevelIndex,int delayInMilisec=0,string animName = "BACK")
    {
        GameManager.Instance._PauseGame = false;

        StartCoroutine(LoadScene(inLevelIndex, delayInMilisec, animName));       
    }


    IEnumerator LoadScene(string inLevelIndex,int loadTime=0, string animName = "BACK")
    {
        yield return new WaitForSeconds(loadTime/1000);
        //mCanvas.SetActive(true);
        mCloudAnimator.Play("MAIN");
        yield return new WaitForSeconds(2f);
        AsyncOperation scene = SceneManager.LoadSceneAsync(inLevelIndex);

        while (!scene.isDone)
        {
            yield return new WaitForSeconds(.75f);
        }
        mCloudAnimator.Play(animName);
        if (tutorial != null)
            tutorial.RegisterUserAction();
    }



    public void BacktoHome()
    {
        StartCoroutine(LoadScene(levelPrefix + GameManager.Instance._playerCurrentLevel));
        GameManager.Instance._IsRefreshNeeded = true;
        GameManager.Instance._PauseGame = false;
    }
    public void AssignTutorial(Tutorial tutorial)
    {
        this.tutorial = tutorial;
    }
}
