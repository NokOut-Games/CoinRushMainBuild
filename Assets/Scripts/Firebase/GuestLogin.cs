using UnityEngine;
using UnityEngine.UI;

public class GuestLogin : MonoBehaviour
{

    [SerializeField] Button gustLogInBtn;
    public void OnClickGuestLogin()
    {
        FirebaseManager.Instance.GuestLogin();
        gustLogInBtn.interactable = false;
        gustLogInBtn.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Loading...";
    }

    private void Update()
    {
        if (FirebaseManager.Instance.readUserData)
            LevelLoadManager.instance.LoadLevelOf(GameManager.Instance._playerCurrentLevel);
    }
}