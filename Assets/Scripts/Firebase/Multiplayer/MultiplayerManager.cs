using UnityEngine;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;
public class AttackedPlayerInformation
{
    public string _attackedPlayerID;
    public string _attackedPlayerPhotoURL;
}
public class OpenCardData
{
    public string OpenedPlayerID;
    public string openedPlayerphotoURL;
    public int OpenedCardIndex;
}
public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public string _currentPlayerId;
    public string _currentPlayerPhotoURL;

    public string _enemyPlayerID;

    DatabaseReference reference;
    FirebaseAuth auth;
    private PlayerIDDetails mplayerIDDetails;
    private GameManager mGameManager;
    private MultiplayerPlayerData mMultiplayerPlayerData;
    private LevelLoadManager mLevelLoadManager;
    public OpenCardsManager mOpenCardsManager;
    AttackManager mAttackManager;
    public  string mPlayerNameData, mPlayerIDData, mCoinData, mEnergyData, mPlayerCurrentLevelData, mOpenCardData, mPlayerPhotoURLData;
    [SerializeField] string mLevelPrefix = "Level";
    private string mLevelName;
    public float _dataBaseFetchTime;

    public List<GameObject> _enemyBuildingDetails;

    

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //Singleton
            return;
        }
    }
    void Start()
    {
        mAttackManager = FindObjectOfType<AttackManager>();
        mplayerIDDetails = FindObjectOfType<PlayerIDDetails>();
        mGameManager = FindObjectOfType<GameManager>();
        mMultiplayerPlayerData = FindObjectOfType<MultiplayerPlayerData>();
        mLevelLoadManager = FindObjectOfType<LevelLoadManager>();
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        
    }
    // Update is called once per frame
    void Update()
    {
        _currentPlayerId = FirebaseManager.Instance.CurrentPlayerID;
        _currentPlayerPhotoURL = FirebaseManager.Instance.CurrentPlayerPhotoURL;
        //_enemyPlayerID = Random. mplayerIDDetails._playerList;

        if (SceneManager.GetActiveScene().name == "OPENCARD")
        {
            mOpenCardsManager = FindObjectOfType<OpenCardsManager>();
        }
    }

    IEnumerator ReadEnemyData()
    {
        yield return new WaitForSeconds(_dataBaseFetchTime);
        
        reference.Child("Facebook Users").Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
        {
           
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                mPlayerNameData = snapshot.Child("UserDetails").Child("_playerName").Value.ToString();
                mPlayerIDData = snapshot.Child("UserDetails").Child("_playerID").Value.ToString();
                mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
                mOpenCardData = snapshot.Child("UserDetails").Child("_openedCards").Value.ToString();
                mPlayerPhotoURLData = snapshot.Child("UserDetails").Child("_playerPhotoURL").Value.ToString();

                mLevelName = mLevelPrefix + mPlayerCurrentLevelData;
                // OpenedCardinfo = int.Parse(mOpenCardData);
                // mManager_Multiplayer._buildingMultiplayerDataRef.Clear(); 

                List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
                for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                {

                    MultiplayerBuildingData builddata = new MultiplayerBuildingData();
                    builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                    builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                    builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
                    builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                    builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());

                    BuildingDetails.Add(builddata);
                }
                
                mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerNameData, mPlayerPhotoURLData);
            }
        });
    }
    public void ReadMyData()
    {
        reference.Child(FirebaseManager.Instance.userTitle).Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task =>
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

                /*mGameManager._coins = int.Parse(mCoinData);
                 mGameManager._energy = int.Parse(mEnergyData);
                 mGameManager._playerCurrentLevel = int.Parse(mPlayerCurrentLevelData);*/

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

                ////Time difference Calculation
                //var difference = crntDateTime - DateTime.Parse(snapshot.Child("UserDetails").Child("LogOutTime").Value.ToString());
                //int value = difference.Minutes;
                //Debug.Log("The Time Diff is: " + value);

                //if (value >= mGameManager._minutes)
                //{
                //    int energyAmount = value / mGameManager._minutes;
                //    Mathf.Ceil(energyAmount);
                //    Debug.Log("The energy amount gained is : " + energyAmount);
                //    mGameManager._energy += energyAmount;
                //}
                //readUserData = true;
                //canWrite = true;

            }
        });
        
    }

    public void WriteDetailsOnAttackComplete()
    {    
        int i = 0;
        foreach (MultiplayerBuildingData buildings in mMultiplayerPlayerData._buildingMultiplayerDataRef)
        {
            string json = JsonUtility.ToJson(buildings);
            reference.Child("Facebook Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mMultiplayerPlayerData._enemyPlayerLevel).Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Write Successful");
                }
            });
            i++;
        }
    }
    public  void OnGettingAttackCard()
    {
        FirebaseManager.Instance.WriteCardDataToFirebase();
        FirebaseManager.Instance.WriteBuildingDataToFirebase();
        FirebaseManager.Instance.WritePlayerDataToFirebase();
        StartCoroutine(ReadEnemyData());
        Invoke(nameof(LoadAttackScene), 2f);
    }
    void LoadAttackScene()
    {
        SceneManager.LoadScene("ATTACK");
    }
    public void CheckAndWriteAttackData()
    {
        reference.Child("Facebook Users").Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();

                if (int.Parse(mPlayerCurrentLevelData) == mMultiplayerPlayerData._enemyPlayerLevel)
                {
                    List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
                    for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                    {
                        MultiplayerBuildingData builddata = new MultiplayerBuildingData();
                        builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                        builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                        builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
                        builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                        builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());
                        BuildingDetails.Add(builddata);
                    }
                    mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerNameData, mPlayerPhotoURLData);

                    mAttackManager.ChangeEnemyBuildingData();

                    //Write attacked Player Info in Firebase
                    AttackedPlayerInformation AttakedPlayerInfo = new AttackedPlayerInformation();
                    AttakedPlayerInfo._attackedPlayerID = _currentPlayerId;
                    AttakedPlayerInfo._attackedPlayerPhotoURL = _currentPlayerPhotoURL;
                    reference.Child("Facebook Users").Child(_enemyPlayerID).Child("AttackedPlayer").SetValueAsync(AttakedPlayerInfo).ContinueWith(task => { });
                }
                else
                {
                    MultiplayerManager.Instance.ReadMyData();
                }
            }
        });
        Invoke("BackToGame", 4f);
    }
  
    void ApplyAttackChanges()
    {

    }
    public void OnClickViewIslandToOpenCard()
    {
        FirebaseManager.Instance.WriteCardDataToFirebase();
        FirebaseManager.Instance.WriteBuildingDataToFirebase();
        FirebaseManager.Instance.WritePlayerDataToFirebase();
        StartCoroutine(ReadEnemyData());
        Invoke(nameof(LoadOpenCardScene), 2f);
    }

    void LoadOpenCardScene()
    {
        LevelLoadManager.instance.LoadLevelASyncOf("OPENCARD");
    }
    public void CheckDetailsForOpenCard()
    {
        reference.Child("Facebook Users").Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();

                if (int.Parse(mPlayerCurrentLevelData) == mMultiplayerPlayerData._enemyPlayerLevel)
                {
                    List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
                    for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                    {
                        MultiplayerBuildingData builddata = new MultiplayerBuildingData();
                        builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                        builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                        builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
                        builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                        builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());
                        BuildingDetails.Add(builddata);
                    }
                    mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerNameData, mPlayerPhotoURLData);
                }
                else
                {
                    MultiplayerManager.Instance.ReadMyData();
                }
            }
        });
    }
    public void WriteOpenCardDataToFirebase()
    {
        OpenCardData cardsDetails = new OpenCardData();
        cardsDetails.OpenedPlayerID = _currentPlayerId;
        cardsDetails.OpenedCardIndex = mOpenCardsManager._openedCardIndex;
        cardsDetails.openedPlayerphotoURL = _currentPlayerPhotoURL;
        string json = JsonUtility.ToJson(cardsDetails);
        reference.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(mOpenCardsManager._OpenCardNumberIndex.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>{});

        mOpenCardsManager._OpenCardNumberIndex += 1;
        Debug.Log(mOpenCardsManager._OpenCardNumberIndex + "LatestNum");

        if(mOpenCardsManager._OpenCardNumberIndex <=5)
        {
          reference.Child("Facebook Users").Child(_enemyPlayerID).Child("UserDetails").Child("_openedCards").SetValueAsync(mOpenCardsManager._OpenCardNumberIndex).ContinueWith(task =>{});
        }
    }

    public void BackToGame()
    {
        LevelLoadManager.instance.BacktoHome();
    }
}
