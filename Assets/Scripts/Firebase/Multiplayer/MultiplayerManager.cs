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
    public string _attackedBuildingName;
}

[System.Serializable]
public class OpenCard
{
    public int _openedCardSlot;
    public string _openedPlayerName;
    public string _openedPlayerID;
    public int _openedCardSelectedCard;
}
public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public string _currentPlayerId;
    public string _currentPlayerName;

    public string _enemyPlayerID;
    public string _enemyName;

    public string _enemyTitle;
    public int _enemyPlayerLevel;

    public List<OpenCard> OpenCardDetails;
    public List<int> OpenedCardSlot = new List<int>();
    public List<string> OpenedPlayerID = new List<string>();

    public bool isReWriting;

    public int AttackCount;
    public List<string> attackedplayerIDList = new List<string>();
    public List<string> attackedplayerNameList = new List<string>();
    public List<AttackedPlayerInformation> CurrenetPlayerAttackData = new List<AttackedPlayerInformation>();

    public List<Building> MultiplayerBuildingDetails = new List<Building>();

    public bool isRevenging;

    DatabaseReference reference;

    private PlayerIDDetails mplayerIDDetails;

    private MultiplayerPlayerData mMultiplayerPlayerData;
    private LevelLoadManager mLevelLoadManager;
    public OpenCardsManager mOpenCardsManager;

    public CardDeck _cardDeck;
    AttackManager mAttackManager;
    public string mPlayerName, mPlayerID, mCoinData, mEnergyData, mPlayerCurrentLevelData, mNumberOfTimesGotAttacked;
    [SerializeField] string mLevelPrefix = "Level";
    private string mLevelName;
    public float _dataBaseFetchTime;

    public List<GameObject> _enemyBuildingDetails;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); 
            return;
        }
    }
    void Start()
    {
        mplayerIDDetails = FindObjectOfType<PlayerIDDetails>();
        mMultiplayerPlayerData = FindObjectOfType<MultiplayerPlayerData>();
        mLevelLoadManager = FindObjectOfType<LevelLoadManager>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;


    }
    void Update()
    {
        _currentPlayerName = FirebaseManager.Instance._PlayerName;
        _currentPlayerId = FirebaseManager.Instance._PlayerID;

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
                OpenCard card = new OpenCard();
                card._openedPlayerID = _currentPlayerId;
                card._openedPlayerName = _currentPlayerName;
                card._openedCardSlot = _cardDeck._openCardSlot;
                card._openedCardSelectedCard = _cardDeck._openedCardIndex;

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

                if (snapshot.Child("Facebook Users").HasChild(_enemyPlayerID))
                {
                    _enemyTitle = "Facebook Users";
                    GetUserDetails();
                    GetBuildingDetails();
                    GetAttackData();
                    GetOpenCardsDetails();
                }
                else if (snapshot.Child("Guest Users").HasChild(_enemyPlayerID))
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
                    mPlayerName = snapshot.Child("UserDetails").Child("_playerName").Value.ToString();
                    mPlayerID = snapshot.Child("UserDetails").Child("_playerID").Value.ToString();
                    mPlayerCurrentLevelData = snapshot.Child("UserDetails").Child("_playerCurrentLevel").Value.ToString();
                    mNumberOfTimesGotAttacked = snapshot.Child("UserDetails").Child("_numberOfTimesGotAttacked").Value.ToString();

                    AttackCount = int.Parse(mNumberOfTimesGotAttacked);
                    mLevelName = mLevelPrefix + mPlayerCurrentLevelData;
                    _enemyPlayerLevel = int.Parse(mPlayerCurrentLevelData);
                    _enemyName = mPlayerName;
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
                    List<Building> BuildingDetails = new List<Building>();
                    for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                    {
                        Building builddata = new Building();
                        builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                        builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                        builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                        builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());

                        BuildingDetails.Add(builddata);
                        MultiplayerBuildingDetails.Add(builddata);
                    }
                    mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mNumberOfTimesGotAttacked), mPlayerName);

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
                    OpenCardDetails = new List<OpenCard>();
                    for (int i = 0; i < OpenCardSnapshot.ChildrenCount; i++)
                    {
                        OpenCard CardData = new OpenCard();

                        CardData._openedPlayerName = OpenCardSnapshot.Child(i.ToString()).Child("_openedPlayerName").Value.ToString();
                        CardData._openedPlayerID = OpenCardSnapshot.Child(i.ToString()).Child("_openedPlayerID").Value.ToString();
                        CardData._openedCardSlot = int.Parse(OpenCardSnapshot.Child(i.ToString()).Child("_openedCardSlot").Value.ToString());
                        CardData._openedCardSelectedCard = int.Parse(OpenCardSnapshot.Child(i.ToString()).Child("_openedCardSelectedCard").Value.ToString());

                        OpenCardDetails.Add(CardData);
                        OpenedCardSlot.Add(CardData._openedCardSlot);
                        OpenedPlayerID.Add(CardData._openedPlayerID);
                    }
                    mMultiplayerPlayerData.UpdateOpenCardDetails(OpenCardDetails, OpenedCardSlot);
                }
                else
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
        reference.Child(FirebaseManager.Instance.userTitle).Child(FirebaseManager.Instance._PlayerID).Child("AttackedPlayer").GetValueAsync().ContinueWith(task =>
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

    public void OnGettingAttackCard()
    {
        if (!isRevenging)
        {
            mplayerIDDetails.GetRandomEnemyID(FirebaseManager.Instance._PlayerID);
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
                    List<Building> BuildingDetails = new List<Building>();
                    for (int i = 0; i < snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).ChildrenCount; i++)
                    {
                        Building builddata = new Building();
                        builddata._buildingName = snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingName").Value.ToString();
                        builddata._buildingCurrentLevel = int.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_buildingCurrentLevel").Value.ToString());
                        builddata._isBuildingDestroyed = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingDestroyed").Value.ToString());
                        builddata._isBuildingShielded = bool.Parse(snapshot.Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).Child("_isBuildingShielded").Value.ToString());
                        BuildingDetails.Add(builddata);
                        MultiplayerBuildingDetails.Add(builddata);
                    }
                    mMultiplayerPlayerData.UpdateUserDetails(BuildingDetails, int.Parse(mPlayerCurrentLevelData), int.Parse(mNumberOfTimesGotAttacked), mPlayerName);
                }
                else
                {
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
        foreach (Building buildings in MultiplayerBuildingDetails)
        {
            string json = JsonUtility.ToJson(buildings);
            reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("Buildings").Child(mLevelPrefix + mPlayerCurrentLevelData).Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Write Successful");
                }
            });
            i++;
        }
        AttackedPlayerInformation AttackedPlayerInfo = new AttackedPlayerInformation();
        AttackedPlayerInfo._attackedPlayerID = _currentPlayerId;
        AttackedPlayerInfo._attackedPlayerName = _currentPlayerName;
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

        mplayerIDDetails.GetRandomEnemyID(FirebaseManager.Instance._PlayerID);
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
    public void WriteOpenCardDataToFirebase()
    {
        reference.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").RemoveValueAsync();
        int i = 0;
        foreach (OpenCard cards in OpenCardDetails)
        {
            string json = JsonUtility.ToJson(cards);
            reference.Child("Facebook Users").Child(_enemyPlayerID).Child("OpenCards").Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {                  
                }
            });
            i++;
        }
    }

    public void BackToGame()
    {
        LevelLoadManager.instance.BacktoHome();
    }
}

