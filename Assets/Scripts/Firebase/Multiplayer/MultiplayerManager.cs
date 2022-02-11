//using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AttackedPlayerInformation
{
    public string _attackedPlayerID;
    public string _attackedPlayerName;
    public string _attackedPlayerPhotoURL;
    public string _attackedBuildingName;

}

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
    public string _enemyName;

    public string _enemyTitle;
    public int _enemyPlayerLevel;
    public List<OpenCardData> OpenCardDetails;
    public List<int> OpenedCardSlot = new List<int>();
    public List<string> OpenedPlayerID = new List<string>();
    private List<string> OpenedPlayerPhotoURL = new List<string>();

    public bool isReWriting;

    public int AttackCount;
    public List<string> attackedplayerIDList = new List<string>();
    public List<string> attackedplayerNameList = new List<string>();
    public List<AttackedPlayerInformation> CurrenetPlayerAttackData = new List<AttackedPlayerInformation>();

    public List<MultiplayerBuildingData> MultiplayerBuildingDetails = new List<MultiplayerBuildingData>();

    public bool isRevenging;

    DatabaseReference reference;
    //FirebaseAuth auth;
    private PlayerIDDetails mplayerIDDetails;
    private GameManager mGameManager;
    private MultiplayerPlayerData mMultiplayerPlayerData;
    private LevelLoadManager mLevelLoadManager;
    public OpenCardsManager mOpenCardsManager;
    public CardDeck _cardDeck;
    AttackManager mAttackManager;
    public string mPlayerNameData, mPlayerIDData, mCoinData, mEnergyData, mPlayerCurrentLevelData, mNumberOfTimesGotAttacked, mPlayerPhotoURLData;
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
        //auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;


    }
    // Update is called once per frame
    void Update()
    {
        _currentPlayerName = FirebaseManager.Instance.CurrentPlayerName;
        _currentPlayerId = FirebaseManager.Instance.CurrentPlayerID;
        _currentPlayerPhotoURL = FirebaseManager.Instance.CurrentPlayerPhotoURL;
       // _enemyPlayerID = mplayerIDDetails._randomEnemyID;

        if (SceneManager.GetActiveScene().name == "OPENCARD")
        {
            mOpenCardsManager = FindObjectOfType<OpenCardsManager>();
            _cardDeck = FindObjectOfType<CardDeck>();
        }
        if (SceneManager.GetActiveScene().name == "ATTACK")
        {
            mAttackManager = FindObjectOfType<AttackManager>();
        }

        //Writing OpenCards Details to a list in Multiplayer Manager
        if (isReWriting)
        {
            for (int i = 0; i < _cardDeck._OpenCardSlotFilled.Count; i++)
            {
                OpenCardData card = new OpenCardData();
                card._openedPlayerID = _currentPlayerId;
                card._openedPlayerName = _currentPlayerName;
                card._openedCardSlot = _cardDeck._openCardSlot;
                card._openedCardSelectedCard = _cardDeck._openedCardIndex;
                card._openedPlayerPhotoURL = _currentPlayerPhotoURL;

                OpenCardDetails.Add(card);
                WriteOpenCardDataToFirebase();
                isReWriting = false;
            }
        }
    }


    IEnumerator ReadEnemyData()
    {
        yield return new WaitForSeconds(_dataBaseFetchTime);

        reference.GetValueAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Child("Facebook Users").HasChild(_enemyPlayerID) == true)
                {
                    _enemyTitle = "Facebook Users";
                    GetUserDetails();
                    GetBuildingDetails();
                    GetAttackData();
                    GetOpenCardsDetails();
                }
                else if (snapshot.Child("Guest Users").HasChild(_enemyPlayerID) == true)
                {
                    _enemyTitle = "Guest Users";
                    GetUserDetails();
                    GetBuildingDetails();
                    GetAttackData();
                }
            }
        });
    }

    void GetUserDetails()
    {
        
        reference.Child(_enemyTitle).Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Child("UserDetails").Exists)
                {
                    mPlayerNameData = snapshot.Child("UserDetails").Child("_playerName").Value.ToString();
                    mPlayerIDData = snapshot.Child("UserDetails").Child("_playerID").Value.ToString();
                    mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
                    mNumberOfTimesGotAttacked = snapshot.Child("UserDetails").Child("_numberOfTimesGotAttacked").Value.ToString();

                    AttackCount = int.Parse(mNumberOfTimesGotAttacked);
                    mLevelName = mLevelPrefix + mPlayerCurrentLevelData;
                    _enemyPlayerLevel = int.Parse(mPlayerCurrentLevelData);
                    _enemyName = mPlayerNameData;
                }

            }
        });
    }

    void GetBuildingDetails()
    {
        reference.Child(_enemyTitle).Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if(snapshot.Child("Buildings").Exists)
                {
                    mMultiplayerPlayerData._buildingMultiplayerDataRef.Clear();
                    MultiplayerBuildingDetails.Clear();
                    List<MultiplayerBuildingData> BuildingDetails = new List<MultiplayerBuildingData>();
                    for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                    {
                        Debug.LogError("Getting Building Details");
                        MultiplayerBuildingData builddata = new MultiplayerBuildingData();
                        builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                        builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                        builddata._isBuildingSpawned = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingSpawned").Value.ToString());
                        builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                        builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());

                        BuildingDetails.Add(builddata);
                        MultiplayerBuildingDetails.Add(builddata);
                    }
                    mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mNumberOfTimesGotAttacked), mPlayerNameData, mPlayerPhotoURLData);
                }
              
            }
        });
    }

    void GetOpenCardsDetails()
    {
        reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("OpenCards").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot OpenCardSnapshot = task.Result;

                if (OpenCardSnapshot.Exists)
                {
                    Debug.Log("OpenCardDetailsExists");
                  
                    //OpenCardDetails = new List<OpenCardData>();
                    OpenCardDetails = new List<OpenCardData>();
                    for (int i = 0; i < OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.ChildrenCount; i++)
                    {
                        OpenCardData CardData = new OpenCardData();

                        CardData._openedPlayerName = OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedPlayerName").Value.ToString();
                        CardData._openedPlayerID = OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedPlayerID").Value.ToString();
                      //  CardData._openedPlayerPhotoURL = OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedPlayerPhotoURL").Value.ToString();
                        CardData._openedCardSlot = int.Parse(OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedCardSlot").Value.ToString());
                        CardData._openedCardSelectedCard = int.Parse(OpenCardSnapshot/*.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards")*/.Child(i.ToString()).Child("_openedCardSelectedCard").Value.ToString());

                        OpenCardDetails.Add(CardData);
                        OpenedCardSlot.Add(CardData._openedCardSlot);
                        OpenedPlayerID.Add(CardData._openedPlayerID);
                        //OpenedPlayerPhotoURL.Add(CardData._openedPlayerPhotoURL);
                    }
                    mMultiplayerPlayerData.UpdateOpenCardDetails(OpenCardDetails, OpenedCardSlot, OpenedPlayerPhotoURL);
                }else
                {
                    reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("OpenCards").SetValueAsync("0").ContinueWith(task =>
                    {
                        if (task.IsCompleted)
                        {
                        }
                    });
                }
               
            }
        });
    }

    void GetAttackData()
    {
        reference.Child(FirebaseManager.Instance.userTitle).Child(FirebaseManager.Instance.CurrentPlayerID).Child("AttackedPlayer").GetValueAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    CurrenetPlayerAttackData.Clear();
                    CurrenetPlayerAttackData = new List<AttackedPlayerInformation>();

                    attackedplayerIDList.Clear();
                    attackedplayerNameList.Clear();
                    for (int i = 0; i < snapshot.ChildrenCount; i++)
                    {
                        AttackedPlayerInformation attackData = new AttackedPlayerInformation();

                        attackData._attackedPlayerID = snapshot.Child(i.ToString()).Child("_attackedPlayerID").Value.ToString();
                        attackData._attackedPlayerName = snapshot.Child(i.ToString()).Child("_attackedPlayerName").Value.ToString();

                        CurrenetPlayerAttackData.Add(attackData);
                        attackedplayerIDList.Add(attackData._attackedPlayerID);
                        attackedplayerNameList.Add(attackData._attackedPlayerName);
                    }
                }
            }
        });
    }



   /* public void ReadMyData()
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
                mNumberOfTimesGotAttacked = snapshot.Child("UserDetails").Child("mNumberOfTimesGotAttacked").Value.ToString();

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
                mGameManager.UpdateUserDetails(BuildingDetails, int.Parse(mCoinData), int.Parse(mEnergyData), int.Parse(mPlayerCurrentLevelData), int.Parse(mNumberOfTimesGotAttacked));

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

    }*/

    public void OnGettingAttackCard()
    {
        if (!isRevenging)
        {
            mplayerIDDetails.GetRandomEnemyID(FirebaseManager.Instance.CurrentPlayerID);
            _enemyPlayerID = mplayerIDDetails._randomEnemyID;
            FirebaseManager.Instance.WriteCardDataToFirebase();
            FirebaseManager.Instance.WriteBuildingDataToFirebase();
            FirebaseManager.Instance.WritePlayerDataToFirebase();
            StartCoroutine(ReadEnemyData());
            Invoke(nameof(LoadAttackScene), 2.5f);
        }
        else
        {
            StartCoroutine(ReadEnemyData());
            Invoke(nameof(LoadAttackScene), 2f);
            isRevenging = false;
        }

    }
    void LoadAttackScene()
    {
        LevelLoadManager.instance.LoadLevelASyncOf("ATTACK");
    }

    public void CheckAttackDataFromFirebase()
    {
        reference.Child(_enemyTitle).Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
                if (int.Parse(mPlayerCurrentLevelData) == mMultiplayerPlayerData._enemyPlayerLevel)
                {
                    MultiplayerBuildingDetails.Clear();
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
                        MultiplayerBuildingDetails.Add(builddata);
                    }
                    mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mNumberOfTimesGotAttacked), mPlayerNameData, mPlayerPhotoURLData);
                }
                else
                {
                    //ReadMyData();
                    FirebaseManager.Instance.ReadData(false);
                    mAttackManager.isDataChanging = false;
                }
            }
        });
        Invoke("BackToGame", 2.5f);
    }

    public void WriteDetailsOnAttackComplete()
    {
        int i = 0;
       // foreach (MultiplayerBuildingData buildings in mMultiplayerPlayerData._buildingMultiplayerDataRef)
        foreach (MultiplayerBuildingData buildings in MultiplayerBuildingDetails)
        {
            string json = JsonUtility.ToJson(buildings);
            reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData)/*Child(mLevelPrefix + mMultiplayerPlayerData._enemyPlayerLevel)*/.Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Write Successful");
                }
            });
            i++;
        }
        //Write attacked Player Info in Firebase
        AttackedPlayerInformation AttackedPlayerInfo = new AttackedPlayerInformation();
        AttackedPlayerInfo._attackedPlayerID = _currentPlayerId;
        AttackedPlayerInfo._attackedPlayerName = _currentPlayerName;
        AttackedPlayerInfo._attackedPlayerPhotoURL = _currentPlayerPhotoURL;
        AttackedPlayerInfo._attackedBuildingName = mAttackManager._TargetTransform.name;
        string attackDetails = JsonUtility.ToJson(AttackedPlayerInfo);
        reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("AttackedPlayer").Child(AttackCount.ToString()).SetRawJsonValueAsync(attackDetails).ContinueWith(task =>
        {
            if (task.IsCompleted)
            { Debug.Log("Attaacked info Written"); }
        });
        AttackCount += 1;
        reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("UserDetails").Child("_numberOfTimesGotAttacked").SetValueAsync(AttackCount.ToString()).ContinueWith(task =>
        {
            if (task.IsCompleted)
            { Debug.Log("Attaacked info Written"); }
        });

    }
    public void OnClickViewIslandToOpenCard()
    {
        OpenedCardSlot.Clear();
        OpenCardDetails.Clear();
        OpenedPlayerID.Clear();
        mplayerIDDetails.GetRandomEnemyID(FirebaseManager.Instance.CurrentPlayerID);
        _enemyPlayerID = mplayerIDDetails._randomOpencardID;
        FirebaseManager.Instance.WriteCardDataToFirebase();
        FirebaseManager.Instance.WriteBuildingDataToFirebase();
        FirebaseManager.Instance.WritePlayerDataToFirebase();
        StartCoroutine(ReadEnemyData());
        Invoke(nameof(LoadOpenCardScene), 1f);
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

    public void WriteOpenCardDataToFirebase()
    {
        //Invoke("BackToGame", 2f);
        reference.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").RemoveValueAsync();
        int i = 0;
        foreach (OpenCardData cards in OpenCardDetails)
        {
            string json = JsonUtility.ToJson(cards);
            reference.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Open Card Write Successful");
                }
            });
            i++;
        }

        //ReadMyData();
       // FirebaseManager.Instance.ReadData(false);
    }

    public void BackToGame()
    {
        LevelLoadManager.instance.BacktoHome();
    }
}

