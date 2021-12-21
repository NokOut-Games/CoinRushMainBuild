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
    public int _buildingCurrentLevel;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int _coins;
    public int _energy = 25;
    public int _shield;
    public float _minutes;

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

    public int _maxEnergy = 50;
    private bool mIsFull = true;

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
        _buildingManagerRef = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        _buildingCount = _buildingManagerRef._buildingData.Count;
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
            _energy += 1;
        }
    }

    /// <summary>
    /// Converts the minutes given at Inspector into seconds and passes it to the coroutine function
    /// </summary>
    /// <param name="inMinutes"></param>
    /// <returns></returns>
    private float MinutesToSecondsConverter(float inMinutes) 
    {
        float seconds = inMinutes * 60;
        return seconds;
    }

}
