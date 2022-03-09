using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.MiniJSON;
using Facebook.Unity;
using System.Text.RegularExpressions;

public class FacebookManager : MonoBehaviour
{
	public static FacebookManager Instance;
	public static Action<Sprite> UserProfilePic;
	public static Action<string> UserProfileName;
	public Text TextStatus;
	public Text TextID;
	public GameObject LoginButton;
	public GameObject FriendsListButton;
	//public GameObject LogOutButton;
	//public GameObject ProfilePicture;
	public Image ProfilePicture;

	//FB Friends
	public GameObject friendsTextPrefab;
	public Transform GetFriendPos;
	public string UserID;

	public List<string> FBFriendsNameList = new List<string>();
	public List<string> FBFriendsIDList = new List<string>();
	AccessToken aToken;
	public bool isinFbPopup;
	public bool isFromTutorial;

	public List<Sprite> randomPicID = new List<Sprite>();

	public GameObject _loadingScreen;
	void Awake()
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
			FB.Init(InitCallback, OnHideUnity);
		}
		else
		{
			FB.ActivateApp();
		}
	}
    private void InitCallback()
	{
		if (FB.IsInitialized)
		{
			FB.ActivateApp();
		}
		else
		{
			TextStatus.text = "Failed to Initialize the Facebook SDK";
		}

		if (FB.IsLoggedIn)
		{
			_loadingScreen.SetActive(true);
			FB.API("/me?fields=name", HttpMethod.GET, DispName);
			FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
			FB.API("/me?fields=id", HttpMethod.GET, DispID);
			GetFBFriends();

			FirebaseManager.Instance.userTitle = "Facebook Users";
			FirebaseManager.Instance.ReadData();
			LoginButton.SetActive(false); //LogOutButton.SetActive(true);
			//FriendsListButton.SetActive(true);
			//DisplayFbFriends();
		}
		else
		{
			//TextStatus.text = "Please login to continue.";
			//LoginButton.SetActive(true);// LogOutButton.SetActive(false);
		}
	}

	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0; //pause
		}
		else
		{
			Time.timeScale = 1; //resume
		}
	}

	public void LoginWithFB(bool isFromTuto =false)
	{
		isFromTutorial = isFromTuto;
		var permission = new List<string>() { "email","public_profile" };
		FB.LogInWithReadPermissions(permission, AuthCallback);
		isinFbPopup = true;
	}

	public void LogoutFromFB( )
	{
		FB.LogOut(); LoginButton.SetActive(true); //LogOutButton.SetActive(false);
	}

	private void AuthCallback(ILoginResult result)
	{
        if (result.Error != null)
        {
            TextStatus.text = result.Error;
        }
		if (result.Cancelled) return;
		if(isFromTutorial) FirebaseManager.Instance.RemoveGuestUser(FirebaseManager.Instance.auth.CurrentUser.UserId);
		FirebaseManager.Instance.userTitle = "Facebook Users";
		FB.API("/me?fields=name", HttpMethod.GET, DispName);
		FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
		FB.API("/me?fields=id", HttpMethod.GET, DispID);
		//LoginButton.SetActive(false);
		GetFBFriends();
         aToken = AccessToken.CurrentAccessToken;
		Debug.Log(aToken.TokenString);
	}
	void DispID(IResult result)
    {
		if (result.Error != null)
		{
			TextID.text = result.Error;
		}
		else
		{
			//TextID.text = "ID is: " + result.ResultDictionary["id"];
			FirebaseManager.Instance._PlayerID = "" + result.ResultDictionary["id"];
			Debug.Log(FirebaseManager.Instance._PlayerID);
			PlayerPrefs.SetString("id", "" + result.ResultDictionary["id"]);
			FirebaseManager.Instance.CreateNewFBUser(aToken.TokenString);
			isinFbPopup = false;
		}
	}
	void DispName(IResult result)
	{
		if (result.Error != null)
		{
			TextStatus.text = result.Error;
		}
		else
		{
			//TextStatus.text = "Hi there: " + result.ResultDictionary["name"];
			FirebaseManager.Instance._PlayerName =""+ result.ResultDictionary["name"];
			UserProfileName?.Invoke(result.ResultDictionary["name"].ToString());
		}
	}

	private void GetPicture(IGraphResult result)
	{
		if (result.Error == null && result.Texture != null)
		{
			FirebaseManager.Instance.CurrentPlayerPhotoSprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
			//ProfilePicture.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
			UserProfilePic?.Invoke(Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2()));
		}
	}

    public void GetFriends(Transform content)
    {
        //GameObject go = Instantiate(friendsTextPrefab, content);
        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            //Debug.Log("the raw" + result.RawResult);
            var localDictionary = (Dictionary<string, object>)Json.Deserialize(result.RawResult);
            //Debug.Log("Local Dictionary: " + localDictionary);
            var friendList = (List<object>)localDictionary["data"];
            //Debug.Log(friendList);

            Vector3 offset = new Vector3(0, 0, 0);
            foreach (var dict in friendList)
            {
                GameObject go = Instantiate(friendsTextPrefab, content);
                go.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = ((Dictionary<string, object>)dict)["name"].ToString();
                FB.API("https" + "://graph.facebook.com/" + ((Dictionary<string, object>)dict)["id"] + "/picture?type=large", HttpMethod.GET, delegate (IGraphResult result)
                {
                    go.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(result.Texture, new Rect(0, 0, 200, 125), new Vector2(0.5f, 0.5f), 100);
                });
                /*Debug.Log(((Dictionary<string, object>)dict)["name"].ToString());*/
                /*				go.transform.SetParent(GetFriendPos.transform, false);*/
                offset += new Vector3(0, -50, 0);

            }
        });
    }


    public void GetFBFriends()
	{
		//Debug.Log("Loged in");
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
				//FB Friends List
				FBFriendsNameList.Add(((Dictionary<string, object>)dict)["name"].ToString());
				FBFriendsIDList.Add(((Dictionary<string, object>)dict)["id"].ToString());
			}
		});
	}




	public void GetProfilePictureWithId(string inId,Action<Sprite> picture,bool isGuest =false)
    {

        if (isGuest)
        {
			picture(randomPicID[UnityEngine.Random.Range(0, randomPicID.Count)]);
			return;
		}
		FB.API("https" + "://graph.facebook.com/" + inId + "/picture?type=large", HttpMethod.GET, delegate (IGraphResult result)
		{
			Sprite s =result.Texture==null || result.Texture.height == 8 ? 
			randomPicID[UnityEngine.Random.Range(0, randomPicID.Count)] : 
			Sprite.Create(result.Texture, new Rect(0, 0, result.Texture.width, result.Texture.height), new Vector2(0.5f, 0.5f), 100.0f);
			 picture(s);
		});
	}

	public void GetProfilePictureWithId(string inId, Action<Sprite,int> picture,int index)
	{
		FB.API("https" + "://graph.facebook.com/" + inId + "/picture?type=large", HttpMethod.GET, delegate (IGraphResult result)
		{
			Sprite s = result.Texture == null || result.Texture.height == 8 ?
			randomPicID[UnityEngine.Random.Range(0, randomPicID.Count)] :
			Sprite.Create(result.Texture, new Rect(0, 0, result.Texture.width, result.Texture.height), new Vector2(0.5f, 0.5f), 100.0f);
			picture(s,index);
		});
	}




	int GetIntFromID(string id)
    {		
		string s =Regex.Match(id, @"\d+").Value;
		int number = s[1];
		Debug.Log(number);
		Debug.Log((int)number % 50);
		return (int)number%50;
	}
}
