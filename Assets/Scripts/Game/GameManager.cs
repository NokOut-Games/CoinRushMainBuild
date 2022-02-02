using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public enum BuildingTypes
{
    Stable,
    Statue,
    Castle,
    Building,
    House
}

[System.Serializable]
public class GameManagerBuildingData
{
    public string _buildingName;
    public int _buildingNo;
    public int _buildingCurrentLevel;
    public bool _isBuildingSpawned;
    public bool _isBuildingShielded;
    public bool _isBuildingDestroyed;
}

//[System.Serializable]
//public class GameManagerOpenCardDetails
//{
//    public string _openedPlayerID;
//    public string _openedPlayerName;
//    public string _openedPlayerPhotoURL;
//    public int _openedCardSlot;
//    public int _openedCardIndex;
//}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int _coins;

    public int _energy = 25;
    public int _maxEnergy = 50;
    public int _regenerationEnergy = 1;

    public int _shield;
    public int _maxShield;

    public int _playerCurrentLevel = 1;
    public int _minutes;
    public int _openedCards;
    public string _playerFBPhotoURL;

    public string _attackedPlayerName;
    public Texture _attackedPlayerImageTexture;
    public string _attackedBuildingName;

    public GameObject AttackedCard;

    public List<GameObject> _BuildingDetails;
    public List<BuildingTypes> _BuildingTypes;
    public List<Vector3> _PositionDetails;
    public List<Quaternion> _RotationList;
    public List<int> _BuildingUpgradationLevel;
    public List<int> _BuildingCost;
    public List<bool> _BuildingShield;
    public List<Vector3> _TargetMarkPost;

    //[Space]
    //[Header("OpenCardDetails")]
    //public string _openedPlayerID, _openedPlayerName, _openedPlayerPhotoURL; 
    //public int _openedSlot, _openedIndex;
    //[Space]

    public List<OpenCardData> OpenCardDetails;
    public List<string> OpenedPlayerPhotoURL = new List<string>();
    public List<int> OpenedCardSlot = new List<int>();

    public Transform[] OpenHandCardsPositions;
    /*(or)*/
    public Vector3[] OpenHandCardsVectorPositions;

    public List<GameManagerBuildingData> _buildingGameManagerDataRef;
    public BuildingManager _buildingManagerRef;
    public int _buildingCount;

    public bool _IsRefreshNeeded;

    public bool _IsBuildingFromFBase = true;

    
    private bool mIsFull = true;

    [Header("Card Deck Data")]
    public List<int> _SavedCardTypes = new List<int>();
    public bool _IsInAutoDraw;

    public bool _PauseGame;

    public bool OpenCardWritten = false;

    [Header("Multiplier")]
    public int _MultiplierValue = 1;

    [Header("Map Screen Values")]
    public int _SetIndex;
    public List<int> _CompletedLevelsInSet;
    public bool hasChoiceInLevel;

    [Header("Tutorial Values")]
    Tutorial tutorial;
    public bool isInTutorial;

    private void Awake()
    {     
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); //Singleton
            return;
        }
        Destroy(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(AutomaticEnergyRefiller());
    }

    private void Update()
    {
        _shield = Mathf.Clamp(_shield, 0, _maxShield);
        _energy = Mathf.Clamp(_energy, 0, 1000);
        if(_energy < 0)
        {
            return;
        }
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (_energy == _maxEnergy)
        {
            mIsFull = false;
            return;
        }
        else
        {
            mIsFull = true;
        }

        if(OpenCardWritten)
        {
            FirebaseManager.Instance.WriteopenCardData();
            OpenCardWritten = false;
        }
    }
    private IEnumerator AutomaticEnergyRefiller()
    {
        while (mIsFull)
        {
            yield return new WaitForSeconds(MinutesToSecondsConverter(_minutes));
            _energy += _regenerationEnergy;
        }
    }
    public float MinutesToSecondsConverter(float inMinutes) 
    {
        float seconds = inMinutes * 60;
        return seconds;
    }


    public void UpdateBuildingData(string inBuildingName, int inBuildingIndex, int inLevel, bool inIsbuildingSpawn , bool inIsBuildingDestroyed)
    {
        if (tutorial != null)
            tutorial.RegisterUserAction();
        _buildingGameManagerDataRef[inBuildingIndex]._buildingNo = inBuildingIndex;
        _buildingGameManagerDataRef[inBuildingIndex]._buildingName = inBuildingName;
        _buildingGameManagerDataRef[inBuildingIndex]._buildingCurrentLevel = inLevel;
        _buildingGameManagerDataRef[inBuildingIndex]._isBuildingSpawned = inIsbuildingSpawn;
        _buildingGameManagerDataRef[inBuildingIndex]._isBuildingDestroyed = inIsBuildingDestroyed;
    }

    public void AddShieldToBuilding(int inBuildingIndex)
    {
        _buildingGameManagerDataRef[inBuildingIndex]._isBuildingShielded = true;
    }

    public void UpdateUserDetails(List<GameManagerBuildingData> inBuildingData, int inCoinData, int inEnergyData, int inCurrentLevel, int inOpenedCards, string inPlayerPhotoURL)
    {
        _buildingGameManagerDataRef = inBuildingData;
        _coins = inCoinData;
        _energy = inEnergyData;
        _playerCurrentLevel = inCurrentLevel;

        _IsRefreshNeeded = true;
        _IsBuildingFromFBase = true;
        _openedCards = inOpenedCards;
        _playerFBPhotoURL = inPlayerPhotoURL;
        FirebaseManager.Instance.readUserData = true;
    }
    public void UpdateOpenCardDetails(List<OpenCardData> inOpenCardDetails, List<int> inOpenCardSlot, List<string> inOpenedPlayerPhotoURL)
    {
        OpenCardDetails = inOpenCardDetails;
        OpenedCardSlot = inOpenCardSlot;
        OpenedPlayerPhotoURL = inOpenedPlayerPhotoURL;
    }

    // void DisplayAttackedEnemyDetails()
    //{
    //    Debug.Log("Attacked Enemy Details Here.." + _attackedPlayerName);
    //}
    public bool HasEnoughCoins(int amount)
    {
        return (_coins >= amount);
    }

    public void CurrentLevelCompleted()
    {
        _IsBuildingFromFBase = false;
        hasChoiceInLevel = true;
        LevelLoadManager.instance.LoadLevelASyncOf("Map", 1000);
    }
    public void AssignTutorial(Tutorial tutorial)
    {
        this.tutorial = tutorial;
    }
}

