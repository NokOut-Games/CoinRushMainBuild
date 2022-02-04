using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
public class FacebookManager : MonoBehaviour
{
	public static FacebookManager Instance;
	public Text TextStatus;
	public Text TextID;
	public GameObject LoginButton;
	//public GameObject LogOutButton;
	//public GameObject ProfilePicture;
	public Image ProfilePicture;

	void Awake()
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
			LoginButton.SetActive(false); //LogOutButton.SetActive(true);
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
		var perms = new List<string>() { "public_profile" };
		FB.LogInWithReadPermissions(perms, AuthCallback);
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
			//http://stackoverflow.com/questions/19756453/how-to-get-users-profile-picture-with-facebooks-unity-sdk
			/*if (result.Texture != null) {
				Image img = ProfilePicture.GetComponent<Image> ();
				img.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
			}*/
			ProfilePicture.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
		}
	}
}
