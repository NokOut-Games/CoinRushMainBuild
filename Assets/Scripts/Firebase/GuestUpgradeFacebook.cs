using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;
using Firebase;
using Firebase.Database;
using System;

public class GuestUpgradeFacebook : MonoBehaviour
{
    FirebaseAuth auth;
    public FirebaseUser _existingUser;
    //Firebase Database References
    DatabaseReference reference;
    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
    }
    public void OnClickFBUpgradeButton()
    {
        var permission = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permission, AuthCallBack);
        FirebaseManager.Instance.userTitle = "Facebook Users";
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
    void UpdateToDatabase(string inAccessToken)
    {
        var credential = Firebase.Auth.FacebookAuthProvider.GetCredential(inAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            FirebaseUser newFBUser;
            newFBUser = task.Result;
            string newId = newFBUser.UserId.ToString();
            Debug.Log(newId);
            reference.GetValueAsync().ContinueWith(task =>
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Child("Facebook Users").HasChild(newId) == true)
                {
                    FirebaseManager.Instance.WritePlayerDataToFirebase();
                    FirebaseManager.Instance.WriteBuildingDataToFirebase();
                }
            });
        });
    }
}
