//using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class AttackedPlayerInformation
{
    public string _attackedPlayerID;
    public string _attackedPlayerName;
    public string _attackedBuildingName;
}

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;
    private DatabaseReference reference;

    private AttackManager mAttackManager;
    private CardDeck mCardDeck;
    private PlayerIDDetails mplayerIDDetails;
    private LevelLoadManager mLevelLoadManager;
    private OpenCardsManager mOpenCardsManager;

    public string _currentPlayerId;
    public string _currentPlayerName;

    public string _enemyPlayerID;
    public string _enemyTitle;
    public int _enemyPlayerLevel;
    //public string _enemyName;

//Open Card and Attack Scene References
    public List<OpenCard> OpenCardDetails;
    public List<int> OpenedCardSlot = new List<int>();
    public List<string> OpenedPlayerID = new List<string>();
    public bool isReWriting;

    public List<Building> MultiplayerBuildingDetails = new List<Building>();
   
    public int AttackCount;
    public bool isRevenging;
    public List<string> attackedplayerIDList = new List<string>();
    public List<string> attackedplayerNameList = new List<string>();
    public List<AttackedPlayerInformation> CurrenetPlayerAttackData = new List<AttackedPlayerInformation>();

   public float _dataBaseFetchTime;
    //Get Enemy Name and Picture
    public static Action<string> GotEnemyName;
    //Get Enemy Data
    public UserData Enemydata = new UserData();
    
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
        mLevelLoadManager = FindObjectOfType<LevelLoadManager>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    void Update()
    {
        _currentPlayerName = FirebaseManager.Instance.userdata.UserDetails._playerName;
        _currentPlayerId = FirebaseManager.Instance.userdata.UserDetails._playerID;

        if (SceneManager.GetActiveScene().name == "OPENCARD")
        {
            mOpenCardsManager = FindObjectOfType<OpenCardsManager>();
            mCardDeck = FindObjectOfType<CardDeck>();
        }
        if (SceneManager.GetActiveScene().name == "ATTACK")
        {
            mAttackManager = FindObjectOfType<AttackManager>();
        }

        //Writing OpenCards Details to a list in Multiplayer Manager
        if (isReWriting)
        {
            for (int i = 0; i < mCardDeck._OpenCardSlotFilled.Count; i++)
            {
                OpenCard card = new OpenCard();
                card._openedPlayerID = _currentPlayerId;
                card._openedPlayerName = _currentPlayerName;
                card._openedCardSlot = mCardDeck._openCardSlot;
                card._openedCardSelectedCard = mCardDeck._openedCardIndex;

                OpenCardDetails.Add(card);
                CheckOpenCardDataFromFirebase();
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
                    GetAttackData();
                }
                else if (snapshot.Child("Guest Users").HasChild(_enemyPlayerID))
                {
                    _enemyTitle = "Guest Users";
                    GetUserDetails();
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
                    var json = snapshot.GetRawJsonValue();
                    Enemydata = JsonUtility.FromJson<UserData>(json);

                    _enemyPlayerLevel = Enemydata.UserDetails._playerCurrentLevel;

                    for (int i = 0;i< Enemydata.OpenCards.Count;i++)
                    {
                        OpenedCardSlot.Add(Enemydata.OpenCards[i]._openedCardSlot);
                        OpenedPlayerID.Add(Enemydata.OpenCards[i]._openedPlayerID);
                    }

                    MultiplayerBuildingDetails = Enemydata.Buildings;

                    GotEnemyName?.Invoke(Enemydata.UserDetails._playerName);
                }
            }
        });
    }

    void GetAttackData()
    {
        CurrenetPlayerAttackData.Clear(); attackedplayerIDList.Clear(); attackedplayerNameList.Clear();
        CurrenetPlayerAttackData = new List<AttackedPlayerInformation>();
        CurrenetPlayerAttackData = FirebaseManager.Instance.AttackedData;
        for (int i = 0; i < FirebaseManager.Instance.userdata.AttackedPlayer.Count; i++)
        {
            attackedplayerIDList.Add(FirebaseManager.Instance.userdata.AttackedPlayer[i]._attackedPlayerID);
            attackedplayerNameList.Add(FirebaseManager.Instance.userdata.AttackedPlayer[i]._attackedPlayerName);
        }
    }
    public void OnGettingAttackCard()
    {
        if (!isRevenging)
        {
            //mplayerIDDetails.GetRandomEnemyID(FirebaseManager.Instance._PlayerID);
            //_enemyPlayerID = mplayerIDDetails._randomEnemyID;
           // _enemyPlayerID = "109561708307210";
            FirebaseManager.Instance.WriteCardDataToFirebase();
            FirebaseManager.Instance.WriteBuildingDataToFirebase();
            FirebaseManager.Instance.WritePlayerDataToFirebase();
            StartCoroutine(ReadEnemyData());
            Invoke(nameof(LoadAttackScene), 1);
        }
        else
        {
            StartCoroutine(ReadEnemyData());
            Invoke(nameof(LoadAttackScene), 1);
            isRevenging = false;
        }
    }
    void LoadAttackScene()
    {
        LevelLoadManager.instance.LoadLevelASyncOf("ATTACK",0,"ATTACK");
    }
    public void CheckAttackDataFromFirebase()
    {
        reference.Child(_enemyTitle).Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                var json = snapshot.GetRawJsonValue();
                Enemydata = JsonUtility.FromJson<UserData>(json);

                if (Enemydata.UserDetails._playerCurrentLevel == _enemyPlayerLevel)
                {
                    MultiplayerBuildingDetails.Clear();
                    MultiplayerBuildingDetails = Enemydata.Buildings;
                    Debug.LogError("Levels Same");
                }
                else
                {
                    Debug.LogError("Levels Not Same");
                    mAttackManager.isDataChanging = false;
                    FirebaseManager.Instance.ReadData(false, false);
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
            reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("Buildings").Child(i.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
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
        reference.Child(_enemyTitle).Child(_enemyPlayerID).Child("UserDetails").Child("_numberOfTimesGotAttacked").SetValueAsync(AttackCount.ToString()).ContinueWith(task => { });
        reference.Child(_enemyTitle).Child(_currentPlayerId).Child("UserDetails").Child("_coins").SetValueAsync(GameManager.Instance._coins).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                FirebaseManager.Instance.ReadData(false, false);
            }
        });
    }

    public void OnClickViewIslandToOpenCard()
    {
        OpenedCardSlot.Clear();
        OpenCardDetails.Clear();
        OpenedPlayerID.Clear();

        //  mplayerIDDetails.GetRandomEnemyID(FirebaseManager.Instance._PlayerID);
        // _enemyPlayerID = mplayerIDDetails._randomOpencardID;
       // _enemyPlayerID = "109561708307210";
        FirebaseManager.Instance.WriteCardDataToFirebase();
        FirebaseManager.Instance.WriteBuildingDataToFirebase();
        FirebaseManager.Instance.WritePlayerDataToFirebase();
        StartCoroutine(ReadEnemyData());
        Invoke(nameof(LoadOpenCardScene), 1.5f);
    }

    void LoadOpenCardScene()
    {
        LevelLoadManager.instance.LoadLevelASyncOf("OPENCARD");
    }
    public void CheckOpenCardDataFromFirebase()
    {
        reference.Child(_enemyTitle).Child(_enemyPlayerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                var json = snapshot.GetRawJsonValue();
                Enemydata = JsonUtility.FromJson<UserData>(json);

                if (Enemydata.UserDetails._playerCurrentLevel == _enemyPlayerLevel)
                {
                    Debug.LogError("Levels Same");
                    WriteOpenCardDataToFirebase();
                }
                else
                {
                    Debug.LogError("Levels Not Same");
                }
            }
        });
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
            });
            i++;
        }
    }
    public void BackToGame()
    {
        LevelLoadManager.instance.BacktoHome();
    }
}

