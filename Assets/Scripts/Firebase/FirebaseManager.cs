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
using Unity.IO;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    public DatabaseReference reference;
    public FirebaseAuth auth;

    public string _PlayerName, _PlayerID, _Coins, _Energy, _CurrentLevel, _NumberOfTimesAttacked;


    public Sprite CurrentPlayerPhotoSprite;

    public List<AttackedPlayerInformation> AttackedData = new List<AttackedPlayerInformation>();

    public List<OpenCard> OpenCardDetails;
    public List<int> OpenedCardSlot = new List<int>();

    [SerializeField] string mLevelPrefix = "Level";

    public string userTitle = "Guest Users";

    public bool readUserData;
    DateTime crntDateTime;
    public bool loadMapScene;

    public UserData userdata = new UserData();
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        crntDateTime = DateTime.Now;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
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
            _PlayerID = PlayerPrefs.GetString("id");
        }
        else
        {
            userTitle = "Guest Users";
            _PlayerID = auth.CurrentUser.UserId;
            _PlayerName = PlayerPrefs.GetString("userName","123edr56");
            Action<Sprite> OnGettingPic = (sprite) =>
            {
                CurrentPlayerPhotoSprite = sprite;

            };
            FacebookManager.Instance.GetProfilePictureWithId(_PlayerName, OnGettingPic, true);
        }
    }

    public void RemoveGuestUser(string id)
    {
        reference.Child("Guest Users").Child(id).RemoveValueAsync();
    }


    public void ReadData(bool calculatetime = true, bool readUserData=true)
    {
        if (userTitle == "Facebook Users")
        {
            reference.Child(userTitle).Child(_PlayerID).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    GetUserData();
                    if (calculatetime) GetTimeCalculations();                   
                }
            });
        }
        else if (userTitle == "Guest Users")
        {
            reference.Child(userTitle).Child(_PlayerID).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    GetUserData();
                    if (calculatetime) GetTimeCalculations();
                }
            });
        }
    }
    void GetUserData()
    {
        reference.Child(userTitle).Child(_PlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    var json = snapshot.GetRawJsonValue();
                    userdata = JsonUtility.FromJson<UserData>(json);

                    GameManager.Instance.UpdateUserDetails(userdata.Buildings, userdata.UserDetails._coins, userdata.UserDetails._energy, userdata.UserDetails._playerCurrentLevel, userdata.UserDetails._numberOfTimesGotAttacked);
                    GameManager.Instance._CompletedLevelsInSet = userdata.MapData.LevelsInSet;//Get map  Details From Firebase
                    GameManager.Instance.UpdateOpenCardDetails(userdata.OpenCards);
                    GameManager.Instance._SetIndex = userdata.MapData.SetIndex;
                    GameManager.Instance._CompletedLevelsInSet = userdata.MapData.LevelsInSet;
                    GameManager.Instance._SavedCardTypes = userdata.SaveCards;  //.Add(int.Parse(snapshot.Child("SaveCards").Child("" + i).Value.ToString()));//Get Save Card Details From Firebase
                    readUserData = true;
                }               
            }
        });
    }

    //void GetMapDataAndSavedCards()
    //{
    //    reference.Child(userTitle).Child(_PlayerID).GetValueAsync().ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            if (snapshot.Exists)
    //            {
    //                GameManager.Instance._SavedCardTypes.Clear();
    //                GameManager.Instance._CompletedLevelsInSet.Clear();
    //                GameManager.Instance._SetIndex = int.Parse(snapshot.Child("MapData").Child("SetIndex").Value.ToString());
    //                for (int i = 0; i < snapshot.Child("MapData").Child("LevelsInSet").ChildrenCount; i++)
    //                {
    //                    GameManager.Instance._CompletedLevelsInSet.Add(int.Parse(snapshot.Child("MapData").Child("LevelsInSet").Child("" + i).Value.ToString()));//Get map  Details From Firebase
    //                }
    //            }
    //        }
    //    });
    //}

    //void GetAttackData()
    //{
    //    reference.Child(userTitle).Child(_PlayerID).Child("AttackedPlayer").GetValueAsync().ContinueWith(task =>
    //    {
    //        AttackedData.Clear();
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;

    //            if (snapshot.Exists)
    //            {
    //                AttackedData = new List<AttackedPlayerInformation>();

    //                for (int i = 0; i < snapshot.ChildrenCount; i++)
    //                {
    //                    AttackedPlayerInformation attackData = new AttackedPlayerInformation();

    //                    attackData._attackedPlayerID = snapshot.Child(i.ToString()).Child("_attackedPlayerID").Value.ToString();
    //                    attackData._attackedPlayerName = snapshot.Child(i.ToString()).Child("_attackedPlayerName").Value.ToString();
    //                    attackData._attackedBuildingName = snapshot.Child(i.ToString()).Child("_attackedBuildingName").Value.ToString();

    //                    AttackedData.Add(attackData);
    //                }
                  
    //               // reference.Child(userTitle).Child(_PlayerID).Child("AttackedPlayer").ValueChanged += HandleAttackData;// Handle Event
    //            }
    //        }
    //    });
    //}


    void GetTimeCalculations()
    {
        reference.Child(userTitle).Child(_PlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                var difference = crntDateTime - DateTime.Parse(snapshot.Child("UserDetails").Child("LogOutTime").Value.ToString());
                int value = difference.Minutes;
                if (value >= GameManager.Instance._minutes)
                {
                    int energyAmount = value / GameManager.Instance._minutes;
                    Mathf.Ceil(energyAmount);
                    GameManager.Instance._energy += energyAmount;
                }
            }
        });
    } 

    public void GuestLogin()
    {
        userTitle = "Guest Users";
        if (auth.CurrentUser != null)
        {
            if (PlayerPrefs.HasKey("MadeHisChoice"))
            {
                _PlayerID = auth.CurrentUser.UserId;
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
            _PlayerID = newUser.UserId;
            SaveNewUserInFirebase(newPlayer);
            WriteBuildingDataToFirebase();
            loadMapScene = true;
        });
        _PlayerName = "Guest" + UnityEngine.Random.Range(100000,999999);

        PlayerPrefs.SetString("userName", _PlayerName);

        Action<Sprite> OnGettingPic = (sprite) => CurrentPlayerPhotoSprite = sprite;
        FacebookManager.Instance.GetProfilePictureWithId(_PlayerName, OnGettingPic, true);
    }

    public void CreateNewFBUser(string inAccessToken)
    {
       Credential credential = FacebookAuthProvider.GetCredential(inAccessToken);
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
        reference.Child("Facebook Users").Child(_PlayerID).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            if(task.IsCompleted)
            {
                if (snapshot.Exists)
                {
                    ReadData();
                }
                else
                {
                    Player newPlayer = new Player(_PlayerID, newFBUser.DisplayName);
                    SaveNewUserInFirebase(newPlayer);
                    WriteBuildingDataToFirebase();
                    loadMapScene = true;
                }
            }   
        });
    }

    public void WritePlayerDataToFirebase()
    {
        Player playerDetails = new Player(_PlayerID, _PlayerName);
        playerDetails._coins = GameManager.Instance._coins;
        playerDetails._energy = GameManager.Instance._energy;
        playerDetails._playerCurrentLevel = GameManager.Instance._playerCurrentLevel;
        playerDetails._numberOfTimesGotAttacked = GameManager.Instance._openedCards;

        string json = JsonUtility.ToJson(playerDetails);
        reference.Child(userTitle).Child(_PlayerID).Child("UserDetails").SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });
    }
    public void DeleteBuildingDataOnNewLevel()
    {
        reference.Child(userTitle).Child(_PlayerID).Child("Buildings").RemoveValueAsync();
    }
    public void WriteBuildingDataToFirebase()
    {
        int i = 0;
        foreach (Building buildings in GameManager.Instance._buildingGameManagerDataRef)
        {
            string json = JsonUtility.ToJson(buildings);
            reference.Child(userTitle).Child(_PlayerID).Child("Buildings").Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
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

        reference.Child(userTitle).Child(_PlayerID).Child("OpenCards").SetValueAsync("0").ContinueWith(task =>{});
        int i = 0;
        foreach (OpenCard cards in GameManager.Instance.OpenCardDetails)
        {
            string json = JsonUtility.ToJson(cards);
            reference.Child(userTitle).Child(_PlayerID).Child("OpenCards").Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
            });
            i++;
        }
    }

    public void SaveNewUserInFirebase(Player inPlayerDataToSave)
    {
        string json = JsonUtility.ToJson(inPlayerDataToSave);
        reference.Child(userTitle).Child(_PlayerID).Child("UserDetails").SetRawJsonValueAsync(json);
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
             reference.Child(userTitle).Child(_PlayerID).Child("UserDetails").Child("LogOutTime").SetValueAsync(dt.ToString());
         });
    }

    public void WriteCardDataToFirebase()
    {

        reference.Child(userTitle).Child(_PlayerID).Child("SaveCards").SetValueAsync(GameManager.Instance._SavedCardTypes).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });
    }
    void WriteMapDataToFirebase()
    {

        reference.Child(userTitle).Child(_PlayerID).Child("MapData").Child("SetIndex").SetValueAsync(GameManager.Instance._SetIndex).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });
        reference.Child(userTitle).Child(_PlayerID).Child("MapData").Child("LevelsInSet").SetValueAsync(GameManager.Instance._CompletedLevelsInSet).ContinueWith(task =>
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