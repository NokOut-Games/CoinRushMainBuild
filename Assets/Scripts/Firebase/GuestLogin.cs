using UnityEngine;
using UnityEngine.UI;

public class GuestLogin : MonoBehaviour
{

    [SerializeField] Button gustLogInBtn;
    [SerializeField] GameObject loadingScreen;
    public void OnClickGuestLogin()
    {
        FirebaseManager.Instance.GuestLogin();
        gustLogInBtn.interactable = false;
        gustLogInBtn.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Loading...";
        //Invoke(nameof(MakeReadDataTrue), 3f);
    }
    private void Start()
    {
       // if (FirebaseManager.Instance.auth.CurrentUser != null && !FirebaseManager.Instance.auth.CurrentUser.IsAnonymous) loadingScreen.SetActive(true);
    }
    private void Update()
    {
        if (FirebaseManager.Instance.readUserData)
        {
            LevelLoadManager.instance.LoadLevelOf(GameManager.Instance._playerCurrentLevel);
            PlayerPrefs.SetInt("MadeHisChoice", 1);
            GameManager.Instance._IsBuildingFromFBase = true;
        }else if (FirebaseManager.Instance.loadMapScene)
        {
            LevelLoadManager.instance.GoToMapScreen(true);
        }
    }
    void MakeReadDataTrue()
    {
        FirebaseManager.Instance.readUserData=true;
        GameManager.Instance._IsRefreshNeeded = true;
    }
}