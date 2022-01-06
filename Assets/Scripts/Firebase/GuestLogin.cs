using UnityEngine;

public class GuestLogin : MonoBehaviour
{
    public void OnClickGuestLogin()
    {
        FirebaseManager.Instance.GuestLogin();
    }
}