using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.MiniJSON;
using Facebook.Unity;
public class FacebookManager : MonoBehaviour
{
	public static FacebookManager Instance;
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
			FB.API("/me?fields=name", HttpMethod.GET, DispName);
			FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
			FB.API("/me?fields=id", HttpMethod.GET, DispID);
			GetFriends();

			FirebaseManager.Instance.userTitle = "Facebook Users";
			FirebaseManager.Instance.ReadData();
			LoginButton.SetActive(false); //LogOutButton.SetActive(true);
			//FriendsListButton.SetActive(true);
			//DisplayFbFriends();
		}
		else
		{
			TextStatus.text = "Please login to continue.";
			LoginButton.SetActive(true);// LogOutButton.SetActive(false);
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

	public void LoginWithFB()
	{
		FirebaseManager.Instance.userTitle = "Facebook Users";
		var permission = new List<string>() { "public_profile", "email" };
		FB.LogInWithReadPermissions(permission, AuthCallback);
	}

	public void LogoutFromFB()
	{
		FB.LogOut(); LoginButton.SetActive(true); //LogOutButton.SetActive(false);
	}

	private void AuthCallback(ILoginResult result)
	{
        if (result.Error != null)
        {
            TextStatus.text = result.Error;
        }
        if (FB.IsLoggedIn)
		{
			FB.API("/me?fields=name", HttpMethod.GET, DispName);
			FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
			FB.API("/me?fields=id", HttpMethod.GET, DispID);
			LoginButton.SetActive(false);// LogOutButton.SetActive(true);
			var aToken = AccessToken.CurrentAccessToken;
			Debug.Log(aToken.TokenString);
			FirebaseManager.Instance.CreateNewFBUser(aToken.TokenString);
			GetFriends();
		}
	}
	void DispID(IResult result)
    {
		if (result.Error != null)
		{
			TextID.text = result.Error;
		}
		else
		{
			TextID.text = "ID is: " + result.ResultDictionary["id"];
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
			TextStatus.text = "Hi there: " + result.ResultDictionary["name"];
		}
	}

	private void GetPicture(IGraphResult result)
	{
		if (result.Error == null && result.Texture != null)
		{
			FirebaseManager.Instance.CurrentPlayerPhotoSprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
			ProfilePicture.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
		}
	}

	public void GetFriends()
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
				FB.API("https" + "://graph.facebook.com/" + UserID + "/picture?type=large", HttpMethod.GET, delegate (IGraphResult result)
				{
					ProfilePicture.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 200, 125), new Vector2(0.5f, 0.5f), 100);
				});
				Debug.Log(((Dictionary<string, object>)dict)["name"].ToString());
				go.transform.SetParent(GetFriendPos.transform, false);
				offset += new Vector3(0, -50, 0);
			}
		});
	}
}