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
    [SerializeField] GameObject LoadingScreen;
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
    public void LoadLevelASyncOf(string inLevelIndex,int delayInMilisec=0)
    {
        GameManager.Instance._PauseGame = false;

        StartCoroutine(LoadScene(inLevelIndex, delayInMilisec));       
    }


    IEnumerator LoadScene(string inLevelIndex,int loadTime=0)
    {
        mCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        AsyncOperation scene = SceneManager.LoadSceneAsync(inLevelIndex);

        while (!scene.isDone)
        {
            yield return new WaitForSeconds(1f);
        }
        mCloudAnimator.SetBool("Loaded", true);

        if (tutorial != null)
            tutorial.RegisterUserAction();
        yield return new WaitForSeconds(2f);

        mCanvas.SetActive(false);
        mCloudAnimator.SetBool("Loaded", false);
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
