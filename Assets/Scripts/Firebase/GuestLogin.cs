using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using Facebook.Unity;
public class GuestLogin : MonoBehaviour
{
    public FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser newUser;
    public FirebaseUser _SignInuser;
    public FirebaseUser _existingUser;
    
    public TextMeshProUGUI _guestPlayerName;
    public GameObject LoginButton;

    public bool CanSignIn = false;
    public bool fetchDetails = false;
   //Firebase Database References
   DatabaseReference reference;
    Player playerDetails = new Player();

    public GameObject _LoginPanel;
    public bool disableLoginPanel = false;

   // SceneManager mSceneManager;
    void Awake()
    {
        InitializeFirebase();
    }
    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
       // mSceneManager = GetComponent<SceneManager>();
    }
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser/*.IsAnonymous*/ != null)
        {
            bool signedIn = auth.CurrentUser != null;
            if (!signedIn)
            {
                Debug.Log("Signed out " + _existingUser.UserId);
            }
            _existingUser = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + _existingUser.UserId);
                Debug.Log("Now u can Upgrade the Guest Account");
                fetchDetails = true;
                CanSignIn = true;
                Invoke("PanelDisable", 1.5f);
                Debug.Log("u logged in New scene incoming");
            }
        }
    }

    void PanelDisable()
    {
        // _LoginPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }
    void Start()
    {
        playerDetails._playerName = "Guest";
        playerDetails._playerCurrentLevel = 1;
        playerDetails._coins = 1000;
        playerDetails._energy = 25;
    }
    public void OnClickGuestLogin()
    {
        if (auth.CurrentUser/*.IsAnonymous*/ != null)
        {
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
           // LoginButton.SetActive(false);
        }
        else
        {
            auth = FirebaseAuth.DefaultInstance;
            auth.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                newUser = task.Result;
                playerDetails._playerID = newUser.UserId;              
                SaveNewUser(newUser.UserId);            
                Invoke("PanelDisable", .5f);
                //_guestPlayerName.text = "Guest";
                // _LoginPanel.SetActive(false);
            });
        }
    }
    void SaveNewUser(string userId)
    {       
        var LoggedInUser = FirebaseAuth.DefaultInstance.CurrentUser;
        string json = JsonUtility.ToJson(playerDetails);
        reference.Child("Guest Users").Child(LoggedInUser.UserId).SetRawJsonValueAsync(json);
    }

}