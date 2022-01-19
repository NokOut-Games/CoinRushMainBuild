using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiplayerBuildingData
{
    public string _buildingName;
    public int _buildingNo;
    public int _buildingCurrentLevel;
    public bool _isBuildingSpawned;
    public bool _isBuildingShielded;
    public bool _isBuildingDestroyed;
}
public class MultiplayerPlayerData : MonoBehaviour
{
    public int _enemyPlayerLevel = 1;
    public List<MultiplayerBuildingData> _buildingMultiplayerDataRef;
    public BuildingManager _buildingManagerRef;
    public int _buildingCount;

    public List<GameObject> _LevelHolder = new List<GameObject>();

    public bool onceDone = false;

    public List<GameObject> _enemyBuildingDetails;

    public void Start()
    {
        onceDone = false;
        //InvokeRepeating(nameof(PopulateEnemyBuildingPrefabs), 1,1);
    }

    void PopulateEnemyBuildingPrefabs()
    {
        for (int i = 0; i < _buildingMultiplayerDataRef.Count; i++)
        {
            Debug.LogError("HI");
            GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + _buildingMultiplayerDataRef[i]._buildingName + _buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
            _enemyBuildingDetails.Add(building);
        }
    }

    public void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ATTACK" && !onceDone)
        {
            Instantiate(_LevelHolder[_enemyPlayerLevel - 1], Vector3.zero, Quaternion.identity);
            for (int i = 0; i < _buildingMultiplayerDataRef.Count; i++)
            {
                Debug.LogError("HI");
                GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + _buildingMultiplayerDataRef[i]._buildingName + _buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
                _enemyBuildingDetails.Add(building);
            }
            onceDone = true;
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "ATTACK")
        {
            _enemyBuildingDetails.Clear();
            onceDone = false;
        }
    }

    public void GetBuildingManagerDetails()
    {
        for (int i = 0; i < _buildingCount; i++)
        {
            _buildingMultiplayerDataRef[i]._buildingName = _buildingManagerRef._buildingData[i]._buildingName;
            _buildingMultiplayerDataRef[i]._buildingCurrentLevel = _buildingManagerRef._buildingData[i]._buildingLevel;
        }
    }

    //public void UpdateMultiplayerBuildingData(string inBuildingName, int inBuildingIndex, int inLevel, bool inIsbuildingSpawn, bool inIsBuildingDestroyed)
    //{
    //    _buildingMultiplayerDataRef[inBuildingIndex]._buildingNo = inBuildingIndex;
    //    _buildingMultiplayerDataRef[inBuildingIndex]._buildingName = inBuildingName;
    //    _buildingMultiplayerDataRef[inBuildingIndex]._buildingCurrentLevel = inLevel;
    //    _buildingMultiplayerDataRef[inBuildingIndex]._isBuildingSpawned = inIsbuildingSpawn;
    //    _buildingMultiplayerDataRef[inBuildingIndex]._isBuildingDestroyed = inIsBuildingDestroyed;

    //    //FirebaseManager.Instance.WriteBuildingDataToFirebase();
    //}

    public void UpdateUserDetails(List<MultiplayerBuildingData> inBuildingData, int inCurrentLevel)
    {
        _buildingMultiplayerDataRef = inBuildingData;
        _enemyPlayerLevel = inCurrentLevel;
        //Invoke(nameof(PopulateEnemyBuildingPrefabs), 3);
        //Instantiate(_LevelHolder[_enemyPlayerLevel - 1], Vector3.zero, Quaternion.identity);
    }

    public void AddShieldToBuilding(int inBuildingIndex)
    {
        _buildingMultiplayerDataRef[inBuildingIndex]._isBuildingShielded = true;
    }

}
