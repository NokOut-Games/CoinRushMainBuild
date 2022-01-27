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
public class AttackedPlayerInformation
{
    public string _attackedPlayerID;
    public string _attackedPlayerName;
    public string _attackedPlayerPhotoURL;
    public string _attackedBuildingName;

}
//public class OpenCardData
//{
//    public string _openedPlayerID;
//    public string _openedPlayerName;
//    public string _openedPlayerPhotoURL;
//    public int _openedCardSlot;
//    public int _openedCardIndex;
//}
[System.Serializable]
public class OpenCardData
{
    public int _openedCardSlot;
    public string _openedPlayerName;
    public string _openedPlayerID;
    public string _openedPlayerPhotoURL;
    public int _openedCardSelectedCard;
}
public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public string _currentPlayerId;
    public string _currentPlayerName;
    public string _currentPlayerPhotoURL;

    public string _enemyPlayerID;
    public string _enemyTitle;
    public List<OpenCardData> OpenCardDetails;
    public List<string> OpenedPlayerPhotoURL = new List<string>();
    public List<int> OpenedCardSlot = new List<int>();

    DatabaseReference reference;
    FirebaseAuth auth;
    private PlayerIDDetails mplayerIDDetails;
    private GameManager mGameManager;
    private MultiplayerPlayerData mMultiplayerPlayerData;
    private LevelLoadManager mLevelLoadManager;
    public OpenCardsManager mOpenCardsManager;
    public CardDeck _cardDeck;
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
        _currentPlayerName = FirebaseManager.Instance.CurrentPlayerName;
        _currentPlayerId = FirebaseManager.Instance.CurrentPlayerID;
        _currentPlayerPhotoURL = FirebaseManager.Instance.CurrentPlayerPhotoURL;
        //_enemyPlayerID = mplayerIDDetails._randomEnemyID;

        if (SceneManager.GetActiveScene().name == "OPENCARD")
        {
            mOpenCardsManager = FindObjectOfType<OpenCardsManager>();
            _cardDeck = FindObjectOfType<CardDeck>();
        }
        if(SceneManager.GetActiveScene().name == "ATTACK")
        {
            mAttackManager = FindObjectOfType<AttackManager>();
        }
    }

    //IEnumerator ReadEnemyData()
    //{
    //    yield return new WaitForSeconds(_dataBaseFetchTime);

    //    reference.Child("Facebook Users").Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
    //    {

    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            mPlayerNameData = snapshot.Child("UserDetails").Child("_playerName").Value.ToString();
    //            mPlayerIDData = snapshot.Child("UserDetails").Child("_playerID").Value.ToString();
    //            mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
    //            mOpenCardData = snapshot.Child("UserDetails").Child("_openedCards").Value.ToString();
    //            mPlayerPhotoURLData = snapshot.Child("UserDetails").Child("_playerPhotoURL").Value.ToString();

    //            mLevelName = mLevelPrefix + mPlayerCurrentLevelData;
    //            // mManager_Multiplayer._buildingMultiplayerDataRef.Clear(); 

    //            List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
    //            for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
    //            {

    //                MultiplayerBuildingData builddata = new MultiplayerBuildingData();
    //                builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
    //                builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
    //                builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
    //                builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
    //                builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());

    //                BuildingDetails.Add(builddata);
    //            }
    //            mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerNameData, mPlayerPhotoURLData);
    //        }
    //    });
    //}
    IEnumerator ReadEnemyData()
    {
        yield return new WaitForSeconds(_dataBaseFetchTime);

        reference/*.Child("Facebook Users").Child(_enemyPlayerID)*/.GetValueAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Child("Facebook Users").HasChild(_enemyPlayerID) == true)
                {
                    Debug.Log("Visiting Facebook user");
                    _enemyTitle = "Facebook Users";
                    mPlayerNameData = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("UserDetails").Child("_playerName").Value.ToString();
                    mPlayerIDData = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("UserDetails").Child("_playerID").Value.ToString();
                    mPlayerCurrentLevelData = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
                    mOpenCardData = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("UserDetails").Child("_openedCards").Value.ToString();
                    mPlayerPhotoURLData = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("UserDetails").Child("_playerPhotoURL").Value.ToString();

                    mLevelName = mLevelPrefix + mPlayerCurrentLevelData;
                    // mManager_Multiplayer._buildingMultiplayerDataRef.Clear(); 

                    List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
                    for (int i = 0; i < snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                    {

                        MultiplayerBuildingData builddata = new MultiplayerBuildingData();
                        builddata._buildingName = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                        builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                        builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
                        builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                        builddata._isBuildingShielded = bool.Parse(snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());

                        BuildingDetails.Add(builddata);
                    }
                    mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerNameData, mPlayerPhotoURLData);


                    if (snapshot.Child(_enemyPlayerID).HasChild("OpenCards") == true)
                    {

                        //OpencardInfo
                        OpenedCardSlot.Clear();
                        OpenCardDetails = new List<OpenCardData>();

                        for (int i = 0; i < snapshot.Child("OpenCards").ChildrenCount; i++)
                        {
                            OpenCardData CardData = new OpenCardData();

                            CardData._openedPlayerName = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(i.ToString()).Child("_openedPlayerName").Value.ToString();
                            CardData._openedPlayerID = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(i.ToString()).Child("_openedPlayerID").Value.ToString();
                            CardData._openedPlayerPhotoURL = snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(i.ToString()).Child("_openedPlayerPhotoURL").Value.ToString();
                            CardData._openedCardSlot = int.Parse(snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(i.ToString()).Child("_openedCardSlot").Value.ToString());
                            CardData._openedCardSelectedCard = int.Parse(snapshot.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(i.ToString()).Child("_openedCardSelectedCard").Value.ToString());

                            OpenCardDetails.Add(CardData);
                            OpenedCardSlot.Add(CardData._openedCardSlot);
                            OpenedPlayerPhotoURL.Add(CardData._openedPlayerPhotoURL);
                        }
                    }
                }
                else if (snapshot.Child("Guest Users").HasChild(_enemyPlayerID) == true)
                {
                    Debug.Log("Visiting Guest user");
                    _enemyTitle = "Guest Users";
                    mPlayerNameData = snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("UserDetails").Child("_playerName").Value.ToString();
                    mPlayerIDData = snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("UserDetails").Child("_playerID").Value.ToString();
                    mPlayerCurrentLevelData = snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
                    mOpenCardData = snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("UserDetails").Child("_openedCards").Value.ToString();
                    mPlayerPhotoURLData = snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("UserDetails").Child("_playerPhotoURL").Value.ToString();

                    mLevelName = mLevelPrefix + mPlayerCurrentLevelData;
                    // mManager_Multiplayer._buildingMultiplayerDataRef.Clear(); 

                    List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
                    for (int i = 0; i < snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                    {

                        MultiplayerBuildingData builddata = new MultiplayerBuildingData();
                        builddata._buildingName = snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                        builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                        builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
                        builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                        builddata._isBuildingShielded = bool.Parse(snapshot.Child("Guest Users").Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());

                        BuildingDetails.Add(builddata);
                    }
                    mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerNameData, mPlayerPhotoURLData);

                    
                }
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

                if (snapshot.Child(FirebaseManager.Instance.userTitle).Child(auth.CurrentUser.UserId).HasChild("OpenCards") == true)
                {
                    //OpencardInfo
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
            }
        });

    }

    public  void OnGettingAttackCard()
    {
        mplayerIDDetails.GetRandomEnemyID(auth.CurrentUser.UserId);
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
    //public void CheckAndWriteAttackData()
    //{
    //    reference.Child("Facebook Users").Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
    //            if (int.Parse(mPlayerCurrentLevelData) == mMultiplayerPlayerData._enemyPlayerLevel)
    //            {
    //                List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
    //                for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
    //                {
    //                    MultiplayerBuildingData builddata = new MultiplayerBuildingData();
    //                    builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
    //                    builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
    //                    builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
    //                    builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
    //                    builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());
    //                    BuildingDetails.Add(builddata);
    //                }
    //                mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerNameData, mPlayerPhotoURLData);
    //            }
    //            else
    //            {
    //               ReadMyData();
    //                mAttackManager.isDataChanging = false;
    //            }
    //        }
    //    });
    //    Invoke("BackToGame", 3f);
    //}

    public void CheckAndWriteAttackData()
    {
        reference.Child(_enemyTitle).Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
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
                    ReadMyData();
                    mAttackManager.isDataChanging = false;
                }
            }
        });
        Invoke("BackToGame", 3f);
    }

    public void WriteDetailsOnAttackComplete()
    {    
        int i = 0;
        foreach (MultiplayerBuildingData buildings in mMultiplayerPlayerData._buildingMultiplayerDataRef)
        {
            string json = JsonUtility.ToJson(buildings);
            reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mMultiplayerPlayerData._enemyPlayerLevel).Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Write Successful");
                }
            });
            i++;
        }
        ////Write attacked Player Info in Firebase
        //AttackedPlayerInformation AttackedPlayerInfo = new AttackedPlayerInformation();
        //AttackedPlayerInfo._attackedPlayerID = _currentPlayerId;
        //AttackedPlayerInfo._attackedPlayerName = _currentPlayerName;
        //AttackedPlayerInfo._attackedPlayerPhotoURL = _currentPlayerPhotoURL;
        //AttackedPlayerInfo._attackedBuildingName = mAttackManager._TargetTransform.name;
        //string attackDetails = JsonUtility.ToJson(AttackedPlayerInfo);
        //reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("AttackedPlayer").SetRawJsonValueAsync(attackDetails).ContinueWith(task =>
        //{
        //    if (task.IsCompleted)
        //    { Debug.Log("Attaacked info Written"); }
        //});
    }
    public void OnClickViewIslandToOpenCard()
    {
        mplayerIDDetails.GetRandomEnemyID(auth.CurrentUser.UserId);
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
    //public void CheckDetailsForOpenCard()
    //{
    //    reference.Child("Facebook Users").Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();

    //            if (int.Parse(mPlayerCurrentLevelData) == mMultiplayerPlayerData._enemyPlayerLevel)
    //            {
    //                List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
    //                for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
    //                {
    //                    MultiplayerBuildingData builddata = new MultiplayerBuildingData();
    //                    builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
    //                    builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
    //                    builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
    //                    builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
    //                    builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());
    //                    BuildingDetails.Add(builddata);
    //                }
    //                mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mOpenCardData), mPlayerNameData, mPlayerPhotoURLData);
    //            }
    //            else
    //            {
    //                MultiplayerManager.Instance.ReadMyData();
    //            }
    //        }
    //    });
    //    Invoke("BackToGame", 3f);
    //}

    //public void WriteOpenCardDataToFirebase()
    //{
    //    Invoke("BackToGame", 3f);
    //    OpenCardData cardsDetails = new OpenCardData();
    //    cardsDetails._openedPlayerID = _currentPlayerId;
    //    cardsDetails._openedPlayerName = _currentPlayerName;
    //    cardsDetails._openedCardSlot = mMultiplayerPlayerData._openCardInfo;
    //    cardsDetails._openedCardIndex = mOpenCardsManager._openedCardIndex;
    //    cardsDetails._openedPlayerPhotoURL = _currentPlayerPhotoURL;
    //    string json = JsonUtility.ToJson(cardsDetails);
    //    reference.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(mOpenCardsManager._OpenCardNumberIndex.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>{});

    //    mOpenCardsManager._OpenCardNumberIndex += 1;

    //    if(mOpenCardsManager._OpenCardNumberIndex <=5)
    //    {
    //      reference.Child("Facebook Users").Child(_enemyPlayerID).Child("UserDetails").Child("_openedCards").SetValueAsync(mOpenCardsManager._OpenCardNumberIndex).ContinueWith(task =>{});
    //    }
    //   ReadMyData();
    //}

    public void WriteOpenCardDataToFirebase()
    {
        Invoke("BackToGame", 3f);
        OpenCardData cardsDetails = new OpenCardData();
        cardsDetails._openedPlayerID = _currentPlayerId;
        cardsDetails._openedPlayerName = _currentPlayerName;
        cardsDetails._openedCardSlot = _cardDeck._openCardSlot;
        cardsDetails._openedCardSelectedCard = _cardDeck._openedCardIndex;
        cardsDetails._openedPlayerPhotoURL = _currentPlayerPhotoURL;
        string json = JsonUtility.ToJson(cardsDetails);
        reference.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(_cardDeck._OpenCardNumberIndex.ToString()).SetRawJsonValueAsync(json).ContinueWith(task => { });

        _cardDeck._OpenCardNumberIndex += 1;

        if (_cardDeck._OpenCardNumberIndex <= 5)
        {
            reference.Child("Facebook Users").Child(_enemyPlayerID).Child("UserDetails").Child("_openedCards").SetValueAsync(_cardDeck._OpenCardNumberIndex).ContinueWith(task => { });
        }
        ReadMyData();
    }

    public void BackToGame()
    {
        LevelLoadManager.instance.BacktoHome();
    }
}
