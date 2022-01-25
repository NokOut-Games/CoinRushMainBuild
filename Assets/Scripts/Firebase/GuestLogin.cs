using UnityEngine;

public class GuestLogin : MonoBehaviour
{
    public void OnClickGuestLogin()
    {
        FirebaseManager.Instance.GuestLogin();
        Invoke("GoToGame", 3f);
    }

    void GoToGame()
    {
        FirebaseManager.Instance.readUserData = true;
    }
}