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
public class Building
{
    public string _buildingName;
    public int _buildingCurrentLevel;
    public bool _isBuildingShielded;
    public bool _isBuildingDestroyed;
}

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
    [HideInInspector] public int _openedCards;

    public List<OpenCard> OpenCardDetails;
   // public List<int> OpenedCardSlot = new List<int>();

    public List<Building> _buildingGameManagerDataRef;

    public bool _IsRefreshNeeded;

    [HideInInspector] public bool _IsBuildingFromFBase = true;

    
    private bool mIsFull = true;

     public List<int> _SavedCardTypes = new List<int>();
    [HideInInspector] public bool _IsInAutoDraw;

    public bool _PauseGame;

    [HideInInspector] public bool OpenCardWritten = false;

    [HideInInspector] public int _MultiplierValue = 1;

     public int _SetIndex;
     public List<int> _CompletedLevelsInSet;
     public bool hasChoiceInLevel;



    Tutorial tutorial;
    [HideInInspector] public bool isInTutorial;
    public static System.Action GotAnOpenCard;
    [HideInInspector] public bool refreshForOpenCard;

    public List<string> BuildingCost = new List<string>();

    public float cucuMultiplier = 1;

    public int _cucuLevel = 1;
    public int _stars = 0;

    public int islandNumber =1;

    private float mRegenerationTimer;
    private float mNextRegenTimer;
    public float mMinutes;
    public float mSeconds;
    public int energyBarMax;
    private void Awake()
    {     
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); 
            return;
        }
        Destroy(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(AutomaticEnergyRefiller());
        mRegenerationTimer = MinutesToSecondsConverter(_minutes);
        mNextRegenTimer = MinutesToSecondsConverter(_minutes);
    }

    private void Update()
    {
        energyBarMax = Mathf.Clamp(_energy, 0, 50);
        if (_energy < _maxEnergy)
        {
            DisplayTime(mRegenerationTimer);
        }

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

    public void DisplayTime(float inTimeToDisplay)
    {
        if (mRegenerationTimer > 0)
        {
            mRegenerationTimer -= Time.deltaTime;
        }
        if (mRegenerationTimer < 0 && inTimeToDisplay < 0)
        {
            inTimeToDisplay = 0;
            mRegenerationTimer = mNextRegenTimer;
        }
        mMinutes = Mathf.FloorToInt(inTimeToDisplay / 60);
        mSeconds = Mathf.FloorToInt(inTimeToDisplay % 60);
    }

    [ContextMenu("Add Stars")]
    public void AddStars()
    {
        _stars += 1;
        CheckCUCULevel(_stars);

    }

    public void CheckCUCULevel(int _inStars)
    {
        switch (_inStars)
        {
            case 5:
                _cucuLevel = 1;
                cucuMultiplier = 1.25f;
                break;
            case 10:
                _cucuLevel = 2;
                cucuMultiplier = 1.50f;
                break;
            case 15:
                _cucuLevel = 3;
                cucuMultiplier = 1.75f;
                break;
            case 20:
                _cucuLevel = 4;
                cucuMultiplier = 2;
                break;
            default:
                break;
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
        _buildingGameManagerDataRef[inBuildingIndex]._buildingName = inBuildingName;
        _buildingGameManagerDataRef[inBuildingIndex]._buildingCurrentLevel = inLevel;
        _buildingGameManagerDataRef[inBuildingIndex]._isBuildingDestroyed = inIsBuildingDestroyed;
    }

    public void AddShieldToBuilding(int inBuildingIndex)
    {
        _buildingGameManagerDataRef[inBuildingIndex]._isBuildingShielded = true;
    }

    public void UpdateUserDetails(List<Building> inBuildingData, int inCoinData, int inEnergyData, int inCurrentLevel, int inOpenedCards)
    {
        _buildingGameManagerDataRef = inBuildingData;
        _coins = inCoinData;
        _energy = inEnergyData;
        _playerCurrentLevel = inCurrentLevel;

        _IsRefreshNeeded = true;
        _IsBuildingFromFBase = true;
        _openedCards = inOpenedCards;
        FirebaseManager.Instance.readUserData = true;
    }
    public void UpdateOpenCardDetails(List<OpenCard> inOpenCardDetails/*, List<int> inOpenCardSlot*/)
    {
        OpenCardDetails = inOpenCardDetails;
        GotAnOpenCard?.Invoke();
        //OpenedCardSlot = inOpenCardSlot;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (_coins >= amount);
    }

    public void CurrentLevelCompleted()
    {
        _IsBuildingFromFBase = false;
        hasChoiceInLevel = true;
        LevelLoadManager.instance.LoadLevelASyncOf("Map", 1000);
        OpenCardDetails.Clear();
        islandNumber++;
        FirebaseManager.Instance.ReadEconomy();
    }
    public void AssignTutorial(Tutorial tutorial)
    {
        this.tutorial = tutorial;
    }
}

