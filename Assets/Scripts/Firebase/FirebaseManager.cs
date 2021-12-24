using UnityEngine;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using System.Collections.Generic;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;

    DatabaseReference reference;
    FirebaseAuth auth;

    private string mPlayerNameData, mPlayerIDData, mCoinData, mEnergyData, mPlayerCurrentLevelData;

    private GameManager mGameManager;
    private LevelLoadManager mLevelLoadManager;

    [SerializeField] string mLevelPrefix = "Level";


    string userTitle = "Guest Users";



    bool readUserData;

    private void Awake()
    {
        mGameManager = FindObjectOfType<GameManager>();
        mLevelLoadManager = FindObjectOfType<LevelLoadManager>();
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //Singleton
            return;
        }

    }





    void ReadDataForGuest()
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


                /*mGameManager._coins = int.Parse(mCoinData);
                 mGameManager._energy = int.Parse(mEnergyData);
                 mGameManager._playerCurrentLevel = int.Parse(mPlayerCurrentLevelData);*/

                string levelName = mLevelPrefix + mPlayerCurrentLevelData;
                GameManager.Instance._buildingGameManagerDataRef.Clear();


                List<GameManagerBuildingData> BuildingDetails = new List<GameManagerBuildingData>();
                for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                {

                    GameManagerBuildingData builddata = new GameManagerBuildingData();

                    builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                    builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                    builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());

                    BuildingDetails.Add(builddata);

                }
                mGameManager.UpdateUserDetails(BuildingDetails, int.Parse(mCoinData), int.Parse(mEnergyData), int.Parse(mPlayerCurrentLevelData));
                readUserData = true;

            }
        });

    }

    public void WritePlayerDataToFirebase()
    {
        Player playerDetails = new Player();

        playerDetails._coins = mGameManager._coins;
        playerDetails._energy = mGameManager._energy;
        playerDetails._playerCurrentLevel = mGameManager._playerCurrentLevel;
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
    //ScreenSwitch


    public void GuestLogin()
    {
        if (auth.CurrentUser != null)
        {

            ReadDataForGuest();
            // WriteBuildingData();
            //WriteBuildingDataToFirebase();
        }
        else
        {
            CreateNewUser();
            WriteBuildingDataToFirebase();

        }
    }



    public void CreateNewUser()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            FirebaseUser newUser;
            newUser = task.Result;
            Player newPlayer = new Player(newUser.UserId);
            Debug.Log(newUser.UserId);
            SaveNewUserInFirebase(newPlayer);
        });
    }


    void SaveNewUserInFirebase(Player inPlayerDataToSave)
    {
        var LoggedInUser = FirebaseAuth.DefaultInstance.CurrentUser;
        string json = JsonUtility.ToJson(inPlayerDataToSave);
        reference.Child(userTitle).Child(LoggedInUser.UserId).Child("UserDetails").SetRawJsonValueAsync(json);
    }
    private void Update()
    {
        if (readUserData)
        {
            LoadToTheCurrentLevel(mGameManager._playerCurrentLevel);

        }
        /* if (GameManager.Instance._NxtLvlInitiated)
         {
             WriteBuildingDataToFirebase();
             WritePlayerDataToFirebase();
             Debug.Log("One");
         }*/
    }



    //Scenemanager Job.......
    void LoadToTheCurrentLevel(int inCurrentLevelNo)
    {
        mLevelLoadManager.LoadLevelOf(inCurrentLevelNo);
        readUserData = false;
    }


    private void OnApplicationQuit()
    {

        WriteBuildingDataToFirebase();
        WritePlayerDataToFirebase();
    }
}


