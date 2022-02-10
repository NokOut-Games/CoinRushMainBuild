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
    public DatabaseReference reference;
    public FirebaseAuth auth;

    public string mPlayerNameData, mPlayerIDData, mCoinData, mEnergyData, mPlayerCurrentLevelData, mNumberOfTimesGotAttacked;

    public string CurrentPlayerID;
    public string CurrentPlayerName;
    public string CurrentPlayerPhotoURL;

    public Sprite CurrentPlayerPhotoSprite;


    public string _attackedPlayerName,_attackedPlayerPhotoURL,_attackedBuildingName;
    public Texture AttackedPlayerImageTexture;
    public List<AttackedPlayerInformation> CurrenetPlayerAttackData = new List<AttackedPlayerInformation>();

    public List<GameManagerBuildingData> BuildingDetailsaa = new List<GameManagerBuildingData>();

    public List<OpenCardData> OpenCardDetails;
    public List<string> OpenedPlayerPhotoURL = new List<string>();
    public List<int> OpenedCardSlot = new List<int>();

    [SerializeField] string mLevelPrefix = "Level";

    public Texture CurrentPlayerImageTexture;

    public string userTitle = "Guest Users";
    public bool readUserData;
    DateTime crntDateTime;
    public bool loadMapScene;

    [SerializeField] Sprite[] guestImages;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        crntDateTime = System.DateTime.Now;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //Singleton
            return;
        }
       // CurrentPlayerPhotoSprite = guestImages[0]; 
    }

    private void Start()
    {
       
        if (auth.CurrentUser == null && !PlayerPrefs.HasKey("MadeHisChoice"))
        {
            CreateNewGuestUser();
            LevelLoadManager.instance.GoToMapScreen(true);

        }
        else if (auth.CurrentUser != null && !auth.CurrentUser.IsAnonymous)
        {
            userTitle = "Facebook Users";
            CurrentPlayerID = PlayerPrefs.GetString("id");
        }
        else
        {
            userTitle = "Guest Users";
            CurrentPlayerID = auth.CurrentUser.UserId;
            CurrentPlayerName = PlayerPrefs.GetString("userName","123edr56");
            Action<Sprite> OnGettingPic = (sprite) =>
            {
                CurrentPlayerPhotoSprite = sprite;

            };
            FacebookManager.Instance.GetProfilePictureWithId(CurrentPlayerName, OnGettingPic, true);
        }

    }

    public void RemoveGuestUser(string id)
    {
        reference.Child("Guest Users").Child(id).RemoveValueAsync();
    }


    public void ReadData(bool calculatetime = true)
    {
        Debug.Log("ReadData");
        if (userTitle == "Facebook Users")
        {
            reference.Child(userTitle).Child(CurrentPlayerID).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    GetUserDetailsAndBuildingDetails();
                    GetMapDataAndSavedCards();
                    GetOpenCardsDetails();
                    GetAttackData();
                    if (calculatetime) GetTimeCalculations();
                    
                }
            });
        }
        else if (userTitle == "Guest Users")
        {
            reference.Child(userTitle).Child(CurrentPlayerID).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    GetUserDetailsAndBuildingDetails();
                    GetMapDataAndSavedCards();
                    GetAttackData();
                    if (calculatetime) GetTimeCalculations();
                }
            });
        }
    }

    private void HandleBuildingValueChanged(object sender, ValueChangedEventArgs e)
    {
        Debug.Log("HaveChange");
        for (int i = 0; i < e.Snapshot.ChildrenCount; i++)
        {
           GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed =bool.Parse(e.Snapshot.Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
           GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingShielded =bool.Parse(e.Snapshot.Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());
        }
        GameManager.Instance._IsRefreshNeeded = true;
    }

    void GetUserDetailsAndBuildingDetails()
    {
        reference.Child(userTitle).Child(CurrentPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                mPlayerNameData = snapshot.Child("UserDetails").Child("_playerName").Value.ToString();
                mPlayerIDData = snapshot.Child("UserDetails").Child("_playerID").Value.ToString();
                mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
                mCoinData = snapshot.Child("UserDetails").Child("_coins").Value.ToString();
                mEnergyData = snapshot.Child("UserDetails").Child("_energy").Value.ToString();
                mNumberOfTimesGotAttacked = snapshot.Child("UserDetails").Child("_numberOfTimesGotAttacked").Value.ToString();

                reference.Child(userTitle).Child(CurrentPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ValueChanged += HandleBuildingValueChanged;//Event Handle

                CurrentPlayerName = mPlayerNameData;
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
                    BuildingDetailsaa.Add(builddata);
                }
                GameManager.Instance.UpdateUserDetails(BuildingDetails, int.Parse(mCoinData), int.Parse(mEnergyData), int.Parse(mPlayerCurrentLevelData), int.Parse(mNumberOfTimesGotAttacked));
                readUserData = true;

            }
        });
    }

    void GetMapDataAndSavedCards()
    {
        reference.Child(userTitle).Child(CurrentPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    GameManager.Instance._SavedCardTypes.Clear();

                    GameManager.Instance._SetIndex = int.Parse(snapshot.Child("MapData").Child("SetIndex").Value.ToString());
                    for (int i = 0; i < snapshot.Child("MapData").Child("LevelsInSet").ChildrenCount; i++)
                    {
                        GameManager.Instance._CompletedLevelsInSet.Add(int.Parse(snapshot.Child("MapData").Child("LevelsInSet").Child("" + i).Value.ToString()));//Get map  Details From Firebase
                    }

                    for (int i = 0; i < snapshot.Child("SaveCards").ChildrenCount; i++)
                    {
                        GameManager.Instance._SavedCardTypes.Add(int.Parse(snapshot.Child("SaveCards").Child("" + i).Value.ToString()));//Get Save Card Details From Firebase
                    }
                }
            }
        });
    }
    public void AddBuildsInLevelListner()
    {
        //Remove listner-0---
        reference.Child(userTitle).Child(CurrentPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ValueChanged -= HandleBuildingValueChanged;
        reference.Child(userTitle).Child(CurrentPlayerID).Child("Buildings").Child(mLevelPrefix + GameManager.Instance._playerCurrentLevel).ValueChanged += HandleBuildingValueChanged;
        mPlayerCurrentLevelData = "" + GameManager.Instance._playerCurrentLevel;

    }
    void GetOpenCardsDetails()
    {
        reference.Child(userTitle).Child(CurrentPlayerID).Child("OpenCards").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot OpenCardSnapshot = task.Result;

                if (OpenCardSnapshot.Exists)
                {
                    Debug.Log("OpenCardDetailsExists");
                    OpenedCardSlot.Clear();
                    OpenCardDetails = new List<OpenCardData>();
                    for (int i = 0; i < OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.ChildrenCount; i++)
                    {
                        OpenCardData CardData = new OpenCardData();

                        CardData._openedPlayerName = OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedPlayerName").Value.ToString();
                        CardData._openedPlayerID = OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedPlayerID").Value.ToString();
                        CardData._openedPlayerPhotoURL = OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedPlayerPhotoURL").Value.ToString();
                        CardData._openedCardSlot = int.Parse(OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedCardSlot").Value.ToString());
                        CardData._openedCardSelectedCard = int.Parse(OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedCardSelectedCard").Value.ToString());

                        OpenCardDetails.Add(CardData);
                        OpenedCardSlot.Add(CardData._openedCardSlot);
                        OpenedPlayerPhotoURL.Add(CardData._openedPlayerPhotoURL);
                    }
                    GameManager.Instance.UpdateOpenCardDetails(OpenCardDetails, OpenedCardSlot, OpenedPlayerPhotoURL);
                    reference.Child(userTitle).Child(CurrentPlayerID).Child("OpenCards").ValueChanged += HandleOpenCardValueChanged;//Event Handle
                }
                else
                {
                    reference.Child(userTitle).Child(CurrentPlayerID).Child("OpenCards").SetValueAsync("0").ContinueWith(task =>
                    {
                        if (task.IsCompleted)
                        {

                            reference.Child(userTitle).Child(CurrentPlayerID).Child("OpenCards").ValueChanged += HandleOpenCardValueChanged;

                        }
                    });
                }
            }
        });
    }

    private void HandleOpenCardValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.Snapshot.Exists)
        {
            Debug.Log("OpenCardDetailsExists");
            OpenCardDetails = new List<OpenCardData>();
            OpenedCardSlot.Clear();
            for (int i = 0; i < e.Snapshot.ChildrenCount; i++)
            {
                OpenCardData CardData = new OpenCardData();

                CardData._openedPlayerName = e.Snapshot.Child(i.ToString()).Child("_openedPlayerName").Value.ToString();
                CardData._openedPlayerID = e.Snapshot.Child(i.ToString()).Child("_openedPlayerID").Value.ToString();
                CardData._openedPlayerPhotoURL = e.Snapshot.Child(i.ToString()).Child("_openedPlayerPhotoURL").Value.ToString();
                CardData._openedCardSlot = int.Parse(e.Snapshot.Child(i.ToString()).Child("_openedCardSlot").Value.ToString());
                CardData._openedCardSelectedCard = int.Parse(e.Snapshot.Child(i.ToString()).Child("_openedCardSelectedCard").Value.ToString());

                OpenCardDetails.Add(CardData);
                OpenedCardSlot.Add(CardData._openedCardSlot);
                //OpenedPlayerPhotoURL.Add(CardData._openedPlayerPhotoURL);
            }
            GameManager.Instance.UpdateOpenCardDetails(OpenCardDetails, OpenedCardSlot, OpenedPlayerPhotoURL);

            //OpenCardValueChange?.Invoke();
        }
    }

    void GetAttackData()
    {
        reference.Child(userTitle).Child(CurrentPlayerID).Child("AttackedPlayer").GetValueAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    CurrenetPlayerAttackData = new List<AttackedPlayerInformation>();

                    for (int i = 0; i < snapshot.ChildrenCount; i++)
                    {
                        AttackedPlayerInformation attackData = new AttackedPlayerInformation();

                        attackData._attackedPlayerID = snapshot.Child(i.ToString()).Child("_attackedPlayerID").Value.ToString();
                        attackData._attackedPlayerName = snapshot.Child(i.ToString()).Child("_attackedPlayerName").Value.ToString();
                        attackData._attackedBuildingName = snapshot.Child(i.ToString()).Child("_attackedBuildingName").Value.ToString();

                        CurrenetPlayerAttackData.Add(attackData);
                    }
                    reference.Child(userTitle).Child(CurrentPlayerID).Child("AttackedPlayer").ValueChanged += HandleAttackData;// Handle Event
                }
            }
        });
    }

    private void HandleAttackData(object sender, ValueChangedEventArgs e)
    {
        CurrenetPlayerAttackData.Clear();
        CurrenetPlayerAttackData = new List<AttackedPlayerInformation>();

        for (int i = 0; i < e.Snapshot.ChildrenCount; i++)
        {
            AttackedPlayerInformation attackData = new AttackedPlayerInformation();

            attackData._attackedPlayerID = e.Snapshot.Child(i.ToString()).Child("_attackedPlayerID").Value.ToString();
            attackData._attackedPlayerName = e.Snapshot.Child(i.ToString()).Child("_attackedPlayerName").Value.ToString();
            attackData._attackedBuildingName = e.Snapshot.Child(i.ToString()).Child("_attackedBuildingName").Value.ToString();

            CurrenetPlayerAttackData.Add(attackData);
        }
    }

    void GetTimeCalculations()
    {
        reference.Child(userTitle).Child(CurrentPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //Time difference Calculation
                var difference = crntDateTime - DateTime.Parse(snapshot.Child("UserDetails").Child("LogOutTime").Value.ToString());
                int value = difference.Minutes;
                Debug.Log("The Time Diff is: " + value);

                if (value >= GameManager.Instance._minutes)
                {
                    int energyAmount = value / GameManager.Instance._minutes;
                    Mathf.Ceil(energyAmount);
                    Debug.Log("The energy amount gained is : " + energyAmount);
                    GameManager.Instance._energy += energyAmount;
                }
            }
        });

    } 


    public void GuestLogin()
    {
        if (auth.CurrentUser != null)
        {
            if (PlayerPrefs.HasKey("MadeHisChoice"))
            {
                CurrentPlayerID = auth.CurrentUser.UserId;
                ReadData();

            }
            else
            {
                readUserData = true;
                LevelLoadManager.instance.GoToMapScreen(true);
            }
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
            CurrentPlayerID = newUser.UserId;
            SaveNewUserInFirebase(newPlayer);
            WriteBuildingDataToFirebase();
            loadMapScene = true;
        });
        CurrentPlayerName = "Guest" + UnityEngine.Random.Range(100000,999999);
        PlayerPrefs.SetString("userName", CurrentPlayerName);
        Action<Sprite> OnGettingPic = (sprite) =>
        {
            CurrentPlayerPhotoSprite = sprite;

        };
        FacebookManager.Instance.GetProfilePictureWithId(CurrentPlayerName, OnGettingPic, true);
    }


    public void CreateNewFBUser(string inAccessToken)
    {
       Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(inAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            FirebaseUser newFBUser;
            newFBUser = task.Result;

           if(task.IsCompleted) CheckFbUser(newFBUser);

        });
    }

    void CheckFbUser(FirebaseUser newFBUser)
    {
        Debug.Log(newFBUser.UserId);
        reference.Child("Facebook Users").Child(CurrentPlayerID).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            if(task.IsCompleted)
            {
                Debug.Log("asgaj");
                if (snapshot.Exists)
                {
                    Debug.Log("yess bro");
                    ReadData();
                }
                else
                {
                    //LevelLoadManager.instance.GoToMapScreen(true);

                    Player newPlayer = new Player(CurrentPlayerID, newFBUser.DisplayName);
                    Debug.Log("This is creating new fb user" + newFBUser.UserId);
                    //CurrentPlayerID = auth.CurrentUser.UserId;
                    //CurrentPlayerName = auth.CurrentUser.DisplayName;
                    SaveNewUserInFirebase(newPlayer);
                    //StartCoroutine(DownloadFacebookImage(auth.CurrentUser.PhotoUrl.ToString()));
                    WriteBuildingDataToFirebase();
                    //GameManager.Instance.hasChoiceInLevel = true;
                    loadMapScene = true;
                }
            }
           
        });
    }

    public void WritePlayerDataToFirebase()
    {
        Player playerDetails = new Player(CurrentPlayerID, CurrentPlayerName);

        playerDetails._coins = GameManager.Instance._coins;
        playerDetails._energy = GameManager.Instance._energy;
        playerDetails._playerCurrentLevel = GameManager.Instance._playerCurrentLevel;
        playerDetails._numberOfTimesGotAttacked = GameManager.Instance._openedCards;
        string json = JsonUtility.ToJson(playerDetails);
        reference.Child(userTitle).Child(CurrentPlayerID).Child("UserDetails").SetRawJsonValueAsync(json).ContinueWith(task =>
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
        foreach (GameManagerBuildingData buildings in GameManager.Instance._buildingGameManagerDataRef)
        {

            string json = JsonUtility.ToJson(buildings);
            reference.Child(userTitle).Child(CurrentPlayerID).Child("Buildings").Child(mLevelPrefix + GameManager.Instance._playerCurrentLevel).Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
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

        reference.Child(userTitle).Child(CurrentPlayerID).Child("OpenCards").SetValueAsync("0").ContinueWith(task=>
        {


        });

        int i = 0;
        foreach (OpenCardData cards in GameManager.Instance.OpenCardDetails)
        {
            string json = JsonUtility.ToJson(cards);
            reference.Child(userTitle).Child(CurrentPlayerID).Child("OpenCards").Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
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
        reference.Child(userTitle).Child(CurrentPlayerID).Child("UserDetails").SetRawJsonValueAsync(json);
        //canWrite = true;
        GameManager.Instance._IsBuildingFromFBase = false;
        WriteAllDataToFireBase();

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
             reference.Child(userTitle).Child(CurrentPlayerID).Child("UserDetails").Child("LogOutTime").SetValueAsync(dt.ToString());
         });
    }

    public void WriteCardDataToFirebase()
    {

        reference.Child(userTitle).Child(CurrentPlayerID).Child("SaveCards").SetValueAsync(GameManager.Instance._SavedCardTypes).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });


    }
    void WriteMapDataToFirebase()
    {

        reference.Child(userTitle).Child(CurrentPlayerID).Child("MapData").Child("SetIndex").SetValueAsync(GameManager.Instance._SetIndex).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });
        reference.Child(userTitle).Child(CurrentPlayerID).Child("MapData").Child("LevelsInSet").SetValueAsync(GameManager.Instance._CompletedLevelsInSet).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });
    }
    private void OnApplicationPause(bool pause)
    {
        if (readUserData&&!FacebookManager.Instance.isinFbPopup)
        {
            WriteAllDataToFireBase();
        }
    }

    private void OnApplicationQuit()
    {
        if (readUserData&&!FacebookManager.Instance.isinFbPopup)
        {
            WriteAllDataToFireBase();
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (readUserData&& !FacebookManager.Instance.isinFbPopup)
        {
            WriteAllDataToFireBase();
        }
    }



    public void WriteAllDataToFireBase()
    {
        WriteCardDataToFirebase();
        WriteMapDataToFirebase();
        WriteBuildingDataToFirebase();
        WritePlayerDataToFirebase();
        CalculateLogOutTime();

    }
}


