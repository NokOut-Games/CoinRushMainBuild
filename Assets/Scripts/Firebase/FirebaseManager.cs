using UnityEngine;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    DatabaseReference reference;
    FirebaseAuth auth;

    private string mPlayerNameData, mPlayerIDData, mCoinData, mEnergyData, mPlayerCurrentLevelData, mPlayerPhotoURLData, mOpenCardData;
    public string CurrentPlayerID;
    public string CurrentPlayerName;
    public string CurrentPlayerPhotoURL;

    public string _attackedPlayerName,_attackedPlayerPhotoURL,_attackedBuildingName;
    public Texture AttackedPlayerImageTexture;

    public List<OpenCardData> OpenCardDetails;
    public List<string> OpenedPlayerPhotoURL = new List<string>();
    public List<int> OpenedCardSlot = new List<int>();
    // public string _openedPlayerName, _openedPlayerPhotoURL, _openedCardIndex,_openedCardSlot, _openedPlayerID; 

    private GameManager mGameManager;
    private LevelLoadManager mLevelLoadManager;

    [SerializeField] string mLevelPrefix = "Level";

    public Texture CurrentPlayerImageTexture;
    //private GameObject _FacebookInfo;
   // private RawImage _FacebookPicture;

    public string userTitle = "Guest Users";

   public bool canWrite;

   // public bool CanUpgradeToFacebook = false;
    public bool readUserData;
    //public GameObject _GuestUpgradeButton;
    //Time
    DateTime crntDateTime;

    private void Awake()
    {
        mGameManager = FindObjectOfType<GameManager>();
        mLevelLoadManager = FindObjectOfType<LevelLoadManager>();
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        crntDateTime = System.DateTime.Now;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //Singleton
            return;
        }

    }

    private void Start()
    {
        if (!auth.CurrentUser.IsAnonymous)
        {
            userTitle = "Facebook Users";
            ReadData();
            CurrentPlayerID = auth.CurrentUser.UserId;
            StartCoroutine(DownloadFacebookImage(auth.CurrentUser.PhotoUrl.ToString()));
            //WriteBuildingDataToFirebase();
        }
    }

    public void ReadData()
    {
        
        reference.Child(userTitle).Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                mPlayerNameData = snapshot.Child("UserDetails").Child("_playerName").Value.ToString();
                mPlayerIDData = snapshot.Child("UserDetails").Child("_playerID").Value.ToString();
                mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
                mCoinData = snapshot.Child("UserDetails").Child("_coins").Value.ToString();
                mEnergyData = snapshot.Child("UserDetails").Child("_energy").Value.ToString();
                mOpenCardData = snapshot.Child("UserDetails").Child("_openedCards").Value.ToString();
                mPlayerPhotoURLData = snapshot.Child("UserDetails").Child("_playerPhotoURL").Value.ToString();
               
                CurrentPlayerPhotoURL = mPlayerPhotoURLData;
                CurrentPlayerName = mPlayerNameData;

                GameManager.Instance._SavedCardTypes.Clear();

                for (int i = 0; i < snapshot.Child("SaveCards").ChildrenCount; i++)
                {
                    GameManager.Instance._SavedCardTypes.Add(int.Parse(snapshot.Child("SaveCards").Child("" + i).Value.ToString()));//Get Save Card Details From Firebase
                }

                string levelName = mLevelPrefix + mPlayerCurrentLevelData;
                GameManager.Instance._buildingGameManagerDataRef.Clear();


                List<GameManagerBuildingData> BuildingDetails = new List<GameManagerBuildingData>();
                for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                {

                    GameManagerBuildingData builddata = new GameManagerBuildingData();

                    builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                    builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                    builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
                    builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                    builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());

                    BuildingDetails.Add(builddata);

                }
                mGameManager.UpdateUserDetails(BuildingDetails, int.Parse(mCoinData), int.Parse(mEnergyData), int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerPhotoURLData);

                readUserData = true; // Works only if it gets all data ,otherwise it wont work

                //if (snapshot.Child(userTitle).Child(auth.CurrentUser.UserId).HasChild("OpenCards") == true)
                {   //OpencardInfo
                    OpenedCardSlot.Clear();
                    OpenCardDetails = new List<OpenCardData>();
                    for (int i = 0; i < snapshot.Child("OpenCards").ChildrenCount; i++)
                    {
                        OpenCardData CardData = new OpenCardData();
                        CardData._openedPlayerName = snapshot.Child("OpenCards").Child(i.ToString()).Child("_openedPlayerName").Value.ToString();
                        CardData._openedPlayerID = snapshot.Child("OpenCards").Child(i.ToString()).Child("_openedPlayerID").Value.ToString();
                        CardData._openedPlayerPhotoURL = snapshot.Child("OpenCards").Child(i.ToString()).Child("_openedPlayerPhotoURL").Value.ToString();
                        CardData._openedCardSlot = int.Parse(snapshot.Child("OpenCards").Child(i.ToString()).Child("_openedCardSlot").Value.ToString());
                        CardData._openedCardSelectedCard = int.Parse(snapshot.Child("OpenCards").Child(i.ToString()).Child("_openedCardSelectedCard").Value.ToString());
                        OpenCardDetails.Add(CardData);
                        OpenedCardSlot.Add(CardData._openedCardSlot);
                        OpenedPlayerPhotoURL.Add(CardData._openedPlayerPhotoURL);
                    }
                    mGameManager.UpdateOpenCardDetails(OpenCardDetails, OpenedCardSlot, OpenedPlayerPhotoURL);
                }
                ////AttackedPlayerInfo  
                //_attackedPlayerName = snapshot.Child("AttackedPlayer").Child("_attackedPlayerName").Value.ToString();
                //_attackedBuildingName= snapshot.Child("AttackedPlayer").Child("_attackedBuildingName").Value.ToString();
                //_attackedPlayerPhotoURL = snapshot.Child("AttackedPlayer").Child("_attackedPlayerPhotoURL").Value.ToString(); //After getting details it goes to Update().



                

                //Time difference Calculation
                var difference = crntDateTime - DateTime.Parse(snapshot.Child("UserDetails").Child("LogOutTime").Value.ToString());
                int value = difference.Minutes;
                Debug.Log("The Time Diff is: " + value);

                if (value >= mGameManager._minutes)
                {
                    int energyAmount = value / mGameManager._minutes;
                    Mathf.Ceil(energyAmount);
                    Debug.Log("The energy amount gained is : " + energyAmount);
                    mGameManager._energy += energyAmount;
                }
              
                //canWrite = true;
                Debug.LogError("I have read the data from firebase succesfully");
            }
        });

    }

   


    public void GuestLogin()
    {
        if (auth.CurrentUser != null)
        {

            CurrentPlayerID = auth.CurrentUser.UserId;
            ReadData();

            //WritePlayerDataToFirebase();

            //CanUpgradeToFacebook = true;
        }
        else
        {
            CreateNewGuestUser();
        }
    }

    public void CreateNewGuestUser()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            FirebaseUser newUser;
            newUser = task.Result;
            Player newPlayer = new Player(newUser.UserId);
            Debug.Log(newUser.UserId);
            CurrentPlayerID = auth.CurrentUser.UserId;
            SaveNewUserInFirebase(newPlayer);
            WriteBuildingDataToFirebase();
            //CanUpgradeToFacebook = true;
            readUserData = true;
           // canWrite = true;
        });
    }

    public void CreateNewFBUser(string inAccessToken)
    {
        Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(inAccessToken);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            FirebaseUser newFBUser;
            newFBUser = task.Result;
            string newId = newFBUser.UserId.ToString();
            Debug.Log(newId);
            reference.GetValueAsync().ContinueWith(task =>
            {
                DataSnapshot snapshot = task.Result;
                //DataSnapshot GuestUsers = snapshot.Child("Guest Users");
                //var some = GuestUsers.Children;
                //Debug.Log(some);
                if (snapshot.Child("Facebook Users").HasChild(newId) == true)
                {
                    ReadData();
                    StartCoroutine(DownloadFacebookImage(auth.CurrentUser.PhotoUrl.ToString()));
                }
                else
                {
                    Player newPlayer = new Player(newFBUser.UserId, newFBUser.DisplayName);
                    Debug.Log(newFBUser.UserId);
                    CurrentPlayerID = auth.CurrentUser.UserId;
                    SaveNewUserInFirebase(newPlayer);
                    WriteBuildingDataToFirebase();
                    StartCoroutine(DownloadFacebookImage(auth.CurrentUser.PhotoUrl.ToString()));
                    readUserData = true;
                }
            });
        });
    }

    public void WritePlayerDataToFirebase()
    {
        Player playerDetails = new Player(auth.CurrentUser.UserId, auth.CurrentUser.DisplayName);

        playerDetails._coins = mGameManager._coins;
        playerDetails._energy = mGameManager._energy;
        playerDetails._playerCurrentLevel = mGameManager._playerCurrentLevel;
        playerDetails._playerPhotoURL = auth.CurrentUser.PhotoUrl.ToString();
        playerDetails._openedCards = mGameManager._openedCards;
        string json = JsonUtility.ToJson(playerDetails);
        reference.Child(userTitle).Child(auth.CurrentUser.UserId).Child("UserDetails").SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });
    }


    public void WriteBuildingDataToFirebase()
    {

        int i = 0;
        foreach (GameManagerBuildingData buildings in mGameManager._buildingGameManagerDataRef)
        {

            string json = JsonUtility.ToJson(buildings);
            reference.Child(userTitle).Child(auth.CurrentUser.UserId).Child("Buildings").Child(mLevelPrefix + mGameManager._playerCurrentLevel).Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Write Successful");
                }
            });
            i++;
        }
    }

    public void WriteopenCardData()
    {
        reference.Child(userTitle).Child(auth.CurrentUser.UserId).Child("OpenCards").RemoveValueAsync();
        int i = 0;
        foreach (OpenCardData cards in mGameManager.OpenCardDetails)
        {
            string json = JsonUtility.ToJson(cards);
            reference.Child(userTitle).Child(auth.CurrentUser.UserId).Child("OpenCards").Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    //  Debug.Log("Write Successful");
                }
            });
            i++;
        }
    }

    public void SaveNewUserInFirebase(Player inPlayerDataToSave)
    {
        var LoggedInUser = FirebaseAuth.DefaultInstance.CurrentUser;
        string json = JsonUtility.ToJson(inPlayerDataToSave);
        reference.Child(userTitle).Child(LoggedInUser.UserId).Child("UserDetails").SetRawJsonValueAsync(json);
        //canWrite = true;
        GameManager.Instance._IsBuildingFromFBase = false;
    }

    private void Update()
    {
        if (readUserData)
        {
            LoadToTheCurrentLevel(mGameManager._playerCurrentLevel);
        }

       // _GuestUpgradeButton = FindInActiveObjectByName("FacebookUpgrade");
       // _FacebookInfo = FindInActiveObjectByName("FacebookInformation");
        //_FacebookPicture = FindObjectOfType<RawImage>();

        //if (CanUpgradeToFacebook)
        //{
        //    _GuestUpgradeButton.SetActive(true);
        //}
        //else
        //{
        //    //if (_FacebookInfo != null)
        //    //{
        //    //    _FacebookInfo.SetActive(true);

        //    //    Invoke("DisplayFacebookInformation", 0.7f);
        //    //}

        //}

        if (_attackedPlayerPhotoURL != null)
        {
            StartCoroutine(DownloadOtherPlayerFacebookImage(_attackedPlayerPhotoURL));
        }
    }

    //void DisplayFacebookInformation()
    //{
    //   // _FacebookPicture.texture = FbImg;
    //}

    IEnumerator DownloadFacebookImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
            Debug.Log(request.error);
        else
        {
            CurrentPlayerImageTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
    IEnumerator DownloadOtherPlayerFacebookImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
            Debug.Log(request.error);
        else
        {
            AttackedPlayerImageTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }



    //Scenemanager Job.......
    void LoadToTheCurrentLevel(int inCurrentLevelNo)
    {
        LevelLoadManager.instance.LoadLevelOf(inCurrentLevelNo);
        readUserData = false;
    }

    public void CalculateLogOutTime()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Timestamp").RunTransaction(TimeData =>
        {
            TimeData.Value = ServerValue.Timestamp;

            return TransactionResult.Success(TimeData);
        })
         .ContinueWith(task =>
         {
             long currentTimestamp = (long)(task.Result.Value);
             var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(currentTimestamp / 1000d)).ToLocalTime();
             reference.Child(userTitle).Child(auth.CurrentUser.UserId).Child("UserDetails").Child("LogOutTime").SetValueAsync(dt.ToString());
         });
    }

    public void WriteCardDataToFirebase()
    {

        reference.Child(userTitle).Child(auth.CurrentUser.UserId).Child("SaveCards").SetValueAsync(GameManager.Instance._SavedCardTypes).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });


    }
    // private void OnApplicationFocus(bool focus)
    // {
    //     if (!focus)
    //     {
    //        WriteCardDataToFirebase();
    //        CalculateLogOutTime();
    //        WriteBuildingDataToFirebase();
    //             WritePlayerDataToFirebase();

    //     }
    // }
    // private void OnApplicationPause(bool pause)
    // {
    //     if (pause)
    //     {
    //        WriteCardDataToFirebase();
    //        CalculateLogOutTime();
    //        WriteBuildingDataToFirebase();
    //         WritePlayerDataToFirebase();
    //     }
    // }

    //private void OnApplicationQuit()
    //{
    //    CalculateLogOutTime();

    //        WriteCardDataToFirebase();
    //        WriteBuildingDataToFirebase();
    //        WritePlayerDataToFirebase();


    //}

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            WriteAllDataToFireBase();
        }
    }

    private void OnApplicationQuit()
    {
        WriteAllDataToFireBase();
    }


    public void WriteAllDataToFireBase()
    {
        WriteCardDataToFirebase();
        CalculateLogOutTime();
        WriteBuildingDataToFirebase();
        WritePlayerDataToFirebase();
        //WriteMapDataToFirebase();
    }
}


