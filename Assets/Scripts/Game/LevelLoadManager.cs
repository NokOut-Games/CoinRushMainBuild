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
    bool isSceneLoad;
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
        if (isSceneLoad) return;
        isSceneLoad = true;
         StartCoroutine(LoadScene(inLevelIndex, delayInMilisec, animName));       
        //LoadScene(inLevelIndex, delayInMilisec, animName);
    }


    IEnumerator /*void*/ LoadScene(string inLevelIndex,int loadTime=0, string animName = "BACK")
    {
        //yield return new WaitForSeconds(loadTime/1000);
        mCanvas.SetActive(true);
        // yield return new WaitForSeconds(2f);
        AsyncOperation scene = SceneManager.LoadSceneAsync(inLevelIndex);
        mCloudAnimator.Play("MAIN");

        while (!scene.isDone)
        {
            yield return new WaitForSeconds(.005f);
        }
        mCloudAnimator.Play(animName);
        isSceneLoad = false;
        if (tutorial != null)
             tutorial.RegisterUserAction();
    }

    public void BacktoHome()
    {
        if (isSceneLoad) return;
        isSceneLoad = true;
        StartCoroutine(LoadScene(levelPrefix + GameManager.Instance._playerCurrentLevel));
        //LoadScene(levelPrefix + GameManager.Instance._playerCurrentLevel);
        GameManager.Instance._IsRefreshNeeded = true;
        GameManager.Instance._PauseGame = false;
    }
    public void AssignTutorial(Tutorial tutorial)
    {
        this.tutorial = tutorial;
    }
}
