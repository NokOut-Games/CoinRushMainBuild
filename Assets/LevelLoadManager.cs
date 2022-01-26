using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelLoadManager : MonoBehaviour
{
    public static LevelLoadManager instance;
    [SerializeField] GameObject mCanvas;
    [SerializeField] Animator mCloudAnimator;
    string levelPrefix = "Level";
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
    public async void LoadLevelASyncOf(int inLevelIndex)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(levelPrefix + inLevelIndex);
        scene.allowSceneActivation = false;
        mCanvas.SetActive(true);
        do
        {

            await System.Threading.Tasks.Task.Delay(100);
        } while (scene.progress<0.9f);
        scene.allowSceneActivation = true;
        await System.Threading.Tasks.Task.Delay(500);
        mCloudAnimator.SetBool("Loaded", true);
        await System.Threading.Tasks.Task.Delay(1000);
        mCanvas.SetActive(false);
        mCloudAnimator.SetBool("Loaded", false);



    }
    public async void LoadLevelASyncOf(string inLevelIndex)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(inLevelIndex);
        scene.allowSceneActivation = false;
        mCanvas.SetActive(true);
        await System.Threading.Tasks.Task.Delay(1000);
        do
        {

            await System.Threading.Tasks.Task.Delay(1000);
        } while (scene.progress < 0.9f);
        scene.allowSceneActivation = true;
        await System.Threading.Tasks.Task.Delay(500);
        mCloudAnimator.SetBool("Loaded", true);
        await System.Threading.Tasks.Task.Delay(1000);
        mCanvas.SetActive(false);
        mCloudAnimator.SetBool("Loaded", false);



    }
    public async void BacktoHome()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(levelPrefix + GameManager.Instance._playerCurrentLevel);
        scene.allowSceneActivation = false;
        mCanvas.SetActive(true);
        await System.Threading.Tasks.Task.Delay(1000);
        do
        {

            await System.Threading.Tasks.Task.Delay(1000);
        } while (scene.progress < 0.9f);
        GameManager.Instance._IsRefreshNeeded = true;

        await System.Threading.Tasks.Task.Delay(500);
        mCloudAnimator.SetBool("Loaded", true);
        scene.allowSceneActivation = true;

        await System.Threading.Tasks.Task.Delay(1000);
        mCanvas.SetActive(false);
        mCloudAnimator.SetBool("Loaded", false);
        //Make the GameToLoad GameManager Data
    }

}
