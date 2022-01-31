
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;
using Firebase.Database;
using Facebook.Unity.Example;
using Facebook.MiniJSON;
using UnityEngine.UI;
public class FacebookLogin : MonoBehaviour
{
    public GameObject friendsTextPrefab;
    public Transform GetFriendPos;

    public static FacebookLogin Instance;
    public Button FBFriendsButton;
    public bool onceDone = false;

    public bool mFbInitialized = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //Singleton
            return;
        }

    }
    private void Start()
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

    //New Items For fb friends
    private void Update()
    {
        //if (!onceDone && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
        //if (/*!onceDone &&*/ UnityEngine.SceneManagement.SceneManager.GetActiveScene().name=="ATTACK")
        //{
        //    FBFriendsButton = GameObject.Find("FBFriends").GetComponent<Button>();
        //    GetFriendPos = GameObject.Find("FriendList").transform;
        //    FBFriendsButton.onClick.AddListener(() =>
        //    {
        //        GetFriendsPlayingTheGame();
        //        Debug.Log("HI");
        //    });
        //    //onceDone = true;
      //  }
    }

    public void GetFriendsPlayingTheGame()
    {
        if (!mFbInitialized)
        {
            FB.Init();
            FB.LogInWithReadPermissions(null, LoginFB);
        }
        else
        {
            OnFbInit();
        }
    }


    void OnFbInit()
    {
        {
            Debug.Log("Loged in");
            string query = "/me/friends";
            FB.API(query, HttpMethod.GET, result =>
            {
                Debug.Log("the raw" + result.RawResult);
                var localDictionary = (Dictionary<string, object>)Json.Deserialize(result.RawResult);
                Debug.Log("Local Dictionary: " + localDictionary);
                var friendList = (List<object>)localDictionary["data"];
                Debug.Log(friendList);

                Vector3 offset = new Vector3(0, 0, 0);
                foreach (var dict in friendList)
                {
                    GameObject go = Instantiate(friendsTextPrefab, transform.position + offset, transform.rotation);
                    go.GetComponent<Text>().text = ((Dictionary<string, object>)dict)["name"].ToString();

                    Debug.Log(((Dictionary<string, object>)dict)["name"].ToString());
                    go.transform.SetParent(GetFriendPos.transform, false);
                    offset += new Vector3(0, -50, 0);
                }
            });
        }
    }

    void LoginFB(ILoginResult result)
    {
        var aToken = AccessToken.CurrentAccessToken;
        Debug.Log(aToken.TokenString);
        Debug.Log("Loggged In");

        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            Debug.Log("the raw" + result.RawResult);
            var localDictionary = (Dictionary<string, object>)Json.Deserialize(result.RawResult);
            Debug.Log("Local Dictionary: " + localDictionary);
            var friendList = (List<object>)localDictionary["data"];
            Debug.Log(friendList);

            Vector3 offset = new Vector3(0, 0, 0);
            foreach (var dict in friendList)
            {
                GameObject go = Instantiate(friendsTextPrefab, transform.position + offset, transform.rotation);
                go.GetComponent<Text>().text = ((Dictionary<string, object>)dict)["name"].ToString();
                Debug.Log(((Dictionary<string, object>)dict)["name"].ToString());
                go.transform.SetParent(GetFriendPos.transform, false);
                offset += new Vector3(0, -50, 0);
            }
        });
    }
}
