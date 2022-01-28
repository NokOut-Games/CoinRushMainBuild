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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int _coins;

    public int _energy = 25;
    public int _maxEnergy = 50;
    public int _regenerationEnergy = 1;

    public int _shield;
    public int _maxShield;

    public int _playerCurrentLevel=1;
    public int _minutes;
    

    public List<GameObject> _BuildingDetails;
    public List<BuildingTypes> _BuildingTypes;
    public List<Vector3> _PositionDetails;
    public List<Quaternion> _RotationList;
    public List<int> _BuildingUpgradationLevel;
    public List<int> _BuildingCost;
    public List<bool> _BuildingShield;
    public List<Vector3> _TargetMarkPost;

    public Transform[] OpenHandCardsPositions;
    /*(or)*/
    public Vector3[] OpenHandCardsVectorPositions;

    public List<GameManagerBuildingData> _buildingGameManagerDataRef;
    public BuildingManager _buildingManagerRef;
    public int _buildingCount;

    public bool _IsRefreshNeeded;

    public bool _IsBuildingFromFBase = true;

    
    private bool mIsFull = true;

    public List<int> _SavedCardTypes = new List<int>();
    public int _MaxLevelsInGame;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        
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
//        _buildingManagerRef = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
//        _maxShield = _buildingManagerRef._buildingData.Count;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //_buildingManagerRef = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
            //_buildingCount = _buildingManagerRef._buildingData.Count;
            //Debug.Log("Get The Building Manager");
        }
        //_buildingGameManagerDataRef = new List<GameManagerBuildingData>(new GameManagerBuildingData[_buildingCount]);
        StartCoroutine(AutomaticEnergyRefiller());
        //GetBuildingManagerDetails();
    }

    public void GetBuildingManagerDetails()
    {
        for (int i = 0; i < _buildingCount; i++)
        {
            _buildingGameManagerDataRef[i]._buildingName = _buildingManagerRef._buildingData[i]._buildingName;
            _buildingGameManagerDataRef[i]._buildingCurrentLevel = _buildingManagerRef._buildingData[i]._buildingLevel;
        }
    }
    private void Update()
    {
        _shield = Mathf.Clamp(_shield, 0, _maxShield);
        _energy = Mathf.Clamp(_energy, 0, 1000);
        if(_energy < 0)
        {
            return;
        }
        if(EventSystem.current.IsPointerOverGameObject())
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
    }
    private IEnumerator AutomaticEnergyRefiller()
    {
        while (mIsFull)
        {
            yield return new WaitForSeconds(MinutesToSecondsConverter(_minutes));
            _energy += _regenerationEnergy;
        }
    }

    /// <summary>
    /// Converts the minutes given at Inspector into seconds and passes it to the coroutine function
    /// </summary>
    /// <param name="inMinutes"></param>
    /// <returns></returns>
    public float MinutesToSecondsConverter(float inMinutes) 
    {
        float seconds = inMinutes * 60;
        return seconds;
    }


    public void UpdateBuildingData(string inBuildingName, int inBuildingIndex, int inLevel, bool inIsbuildingSpawn , bool inIsBuildingDestroyed)
    {
        _buildingGameManagerDataRef[inBuildingIndex]._buildingNo = inBuildingIndex;
        _buildingGameManagerDataRef[inBuildingIndex]._buildingName = inBuildingName;
        _buildingGameManagerDataRef[inBuildingIndex]._buildingCurrentLevel = inLevel;
        _buildingGameManagerDataRef[inBuildingIndex]._isBuildingSpawned = inIsbuildingSpawn;
        _buildingGameManagerDataRef[inBuildingIndex]._isBuildingDestroyed = inIsBuildingDestroyed;
        //FirebaseManager.Instance.WriteBuildingDataToFirebase();
    }

    public void AddShieldToBuilding(int inBuildingIndex)
    {
        _buildingGameManagerDataRef[inBuildingIndex]._isBuildingShielded = true;
    }

    public void UpdateUserDetails(List<GameManagerBuildingData> inBuildingData, int inCoinData, int inEnergyData, int inCurrentLevel)
    {
        _buildingGameManagerDataRef = inBuildingData;
        _coins = inCoinData;
        _energy = inEnergyData;
        _playerCurrentLevel = inCurrentLevel;

        _IsRefreshNeeded = true;
        _IsBuildingFromFBase = true;

    }

    public bool HasEnoughCoins(int amount)
    {
        return (_coins >= amount);
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(400, 200, 300, 100), "Sprint-6");
        GUI.skin.textField.fontSize = 70;
    }

    public void CurrentLevelCompleted()
    {
        if (_playerCurrentLevel < _MaxLevelsInGame)
        {
            _playerCurrentLevel++;
            _IsBuildingFromFBase = false;
            LevelLoadManager.instance.LoadLevelASyncOf(_playerCurrentLevel);
            FirebaseManager.Instance.WriteBuildingDataToFirebase();
        }
        else
        {
            Debug.Log("MaxLevel");
        }

    }
}

