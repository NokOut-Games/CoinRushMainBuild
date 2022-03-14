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
            _PlayerName = PlayerPrefs.GetString("userName", "123edr56");
            Action<Sprite> OnGettingPic = (sprite) =>
            {
                CurrentPlayerPhotoSprite = sprite;

            };
            FacebookManager.Instance.GetProfilePictureWithId(_PlayerName, OnGettingPic, true);
        }

    }

    public void RemoveGuestUser(string id)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user.UserId != null)
        {
            user.DeleteAsync().ContinueWith(task => {

                Debug.Log("User deleted successfully.");
            });
        }
        reference.Child("Guest Users").Child(id).RemoveValueAsync();
    }


    public void ReadData(bool calculatetime = true, bool readUserData = true)
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
                    GameManager.Instance._SavedCardTypes = userdata.SaveCards;
                    GameManager.Instance.islandNumber = userdata.UserDetails.islandNumber;
                    RNG.instance.Behaviour = (PlayerBehaviour)userdata.UserDetails.behaviour;
                    //Get Changed Build and OpenCard Value 
                    reference.Child(userTitle).Child(_PlayerID).Child("Buildings").ValueChanged += HandleBuildingValueChanged;//Event Handle
                    reference.Child(userTitle).Child(_PlayerID).Child("OpenCards").ValueChanged += HandleOpenCardValueChanged;//Event Handle

                    ReadEconomy(userdata.UserDetails.islandNumber);
                    ReadMiniGameEconomy();
                }
            }
        });
    }


    private void HandleBuildingValueChanged(object sender, ValueChangedEventArgs e)
    {
        Debug.Log("HaveChange");
        for (int i = 0; i < e.Snapshot.ChildrenCount; i++)
        {
            GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed = bool.Parse(e.Snapshot.Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
            GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingShielded = bool.Parse(e.Snapshot.Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());
        }
        GameManager.Instance._IsRefreshNeeded = true;
    }

    private void HandleOpenCardValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.Snapshot.Exists)
        {
            Debug.Log("OpenCardDetailsExists");
            OpenCardDetails.Clear();
            OpenCardDetails = new List<OpenCard>();
            //OpenedCardSlot.Clear();
            for (int i = 0; i < e.Snapshot.ChildrenCount; i++)
            {
                OpenCard CardData = new OpenCard();

                CardData._openedCardSlot = int.Parse(e.Snapshot.Child(i.ToString()).Child("_openedCardSlot").Value.ToString());
                CardData._openedPlayerName = e.Snapshot.Child(i.ToString()).Child("_openedPlayerName").Value.ToString();
                CardData._openedPlayerID = e.Snapshot.Child(i.ToString()).Child("_openedPlayerID").Value.ToString();
                CardData._openedCardSelectedCard = int.Parse(e.Snapshot.Child(i.ToString()).Child("_openedCardSelectedCard").Value.ToString());
                OpenCardDetails.Add(CardData);
            }
            GameManager.Instance.UpdateOpenCardDetails(OpenCardDetails);
        }
    }

 

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
            ReadMiniGameEconomy();
            ReadEconomy();
            loadMapScene = true;
        });
        _PlayerName = "Guest" + UnityEngine.Random.Range(100000, 999999);

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
            if (task.IsCompleted) CheckFbUser(newFBUser);

        });
    }

    void CheckFbUser(FirebaseUser newFBUser)
    {
        Debug.Log(newFBUser.UserId);
        reference.Child("Facebook Users").Child(_PlayerID).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            if (task.IsCompleted)
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
        playerDetails.islandNumber = GameManager.Instance.islandNumber;
        playerDetails.Behaviour = (int) RNG.instance.Behaviour;
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

    public void DeleteOpenCardData()
    {
        reference.Child(userTitle).Child(_PlayerID).Child("OpenCards").SetValueAsync("0").ContinueWith(task => { });
    }

    public void WriteopenCardData()
    {

        reference.Child(userTitle).Child(_PlayerID).Child("OpenCards").SetValueAsync("0").ContinueWith(task => { });
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
    public void ReadMiniGameData(string GameName)
    {
        reference.Child("Economy").Child(GameName).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            var json = snapshot.GetRawJsonValue();
            Debug.Log(json);
         

         
        });
    }
    public void ReadEconomy(int islandNumber=1)
    {
        reference.Child("Economy").Child("Level" + islandNumber).GetValueAsync().ContinueWith(task =>
        {
             List<string> cost = new List<string>();
             DataSnapshot snapshot = task.Result;
             foreach (var dataSnapshot in snapshot.Children)
             {
                 cost.Add(dataSnapshot.Value.ToString());
             }
             GameManager.Instance.BuildingCost = cost;
             readUserData = true;

        });
    }
    public void WriteEconomy(string title, List<string> level)
    {
        reference.Child("Economy").Child(title).SetValueAsync(level).ContinueWith(a =>
        {
        });
    }
    public void ReadMiniGameEconomy()
    {
        reference.Child("Economy").Child("MiniGameEconomy").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    var json = snapshot.GetRawJsonValue();
                    GameManager.Instance.minigameEconomy = JsonUtility.FromJson<MiniGameEconomy>(json);
                }
            }
        });
    }
    public void WriteMiniGameEconomy(MiniGameEconomy dataClass)
    {
        string json = JsonUtility.ToJson(dataClass);
        reference.Child("Economy").Child("MiniGameEconomy").SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });
    }
    private void OnApplicationPause(bool pause)
    {
        if (readUserData && !FacebookManager.Instance.isinFbPopup)
        {
            WriteAllDataToFireBase();
        }
    }
    private void OnApplicationQuit()
    {
        if (readUserData && !FacebookManager.Instance.isinFbPopup)
        {
            WriteAllDataToFireBase();
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (readUserData && !FacebookManager.Instance.isinFbPopup)
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
        RNG.instance.Save();
    }
}