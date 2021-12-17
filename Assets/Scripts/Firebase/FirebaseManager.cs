using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine.UI;
using System;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    DatabaseReference reference;
    Player playerDetails = new Player();
    public TextMeshProUGUI _guestPlayerName;

    // FirebaseAuth auth;
    GuestLogin mGuestLogin;
    bool canWrite=false;
    private string mPlayerNameData, mPlayerIDData, mCoinData, mEnergyData, mPlayerCurrentLevelData;
    private GameManager mGameManager;
   
    public static FirebaseManager Instance;

    private void Awake()
    {
        mGameManager = FindObjectOfType<GameManager>();
        mGuestLogin = FindObjectOfType<GuestLogin>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //Singleton
            return;
        }     
    }
    private void Start()
    {
        if (mGuestLogin.fetchDetails == true)
        {
            ReadDataForGuest();
        }
    }
    void ReadDataForGuest()
    {
        reference.Child("Guest Users").Child(mGuestLogin.auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task => 
        {
            if(task.IsCompleted)
            {  
                DataSnapshot snapshot = task.Result;
                mPlayerNameData = snapshot.Child("_playerName").Value.ToString();
                mPlayerIDData = snapshot.Child("_playerID").Value.ToString();
                mPlayerCurrentLevelData = snapshot.Child("_playerCurrentLevel").Value.ToString();
                mCoinData = snapshot.Child("_coins").Value.ToString();
                mEnergyData = snapshot.Child("_energy").Value.ToString();

                mGameManager._coins = int.Parse(mCoinData);
                mGameManager._energy = int.Parse(mEnergyData);
                mGameManager._playerCurrentLevel = int.Parse(mPlayerCurrentLevelData);
            }
        });
     }

    void WritedataForGuest()
    {
        playerDetails._coins = mGameManager._coins;
        playerDetails._energy = mGameManager._energy;
        playerDetails._playerCurrentLevel = mGameManager._playerCurrentLevel;
        string json = JsonUtility.ToJson(playerDetails);
        reference.Child("Guest Users").Child(mGuestLogin.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Write Successful");
            }
        });
    }

    void Update()
    {
       // _guestPlayerName.text = mPlayerNameData;
        if (mGuestLogin.fetchDetails == true)
        {
            ReadDataForGuest();
            mGuestLogin.fetchDetails = false;
            canWrite = true;         
        }
        if(canWrite)
        {
            Invoke("WriteDataToDatabase", 3f);
        }     
    }
 
    void WriteDataToDatabase()
    {
        WritedataForGuest();
    }
}
