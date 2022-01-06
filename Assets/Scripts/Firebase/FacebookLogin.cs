
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;
using Firebase.Database;
public class FacebookLogin : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }
    private void InitCallBack()
    {
        if (!FB.IsInitialized)
        {
            FB.ActivateApp();
        }
    }
    private void OnHideUnity(bool isgameshown)
    {
        if (!isgameshown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void OnClickFacebookLogin()
    {
            FirebaseManager.Instance.userTitle = "Facebook Users";
            var permission = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(permission, AuthCallBack);
    }
    private void AuthCallBack(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = AccessToken.CurrentAccessToken;
            Debug.Log(aToken.TokenString);
            FirebaseManager.Instance.CreateNewFBUser(aToken.TokenString);
        }
    }

}
