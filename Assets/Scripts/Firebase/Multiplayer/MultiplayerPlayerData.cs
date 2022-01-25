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
    public string _enemyName;
    public string _enemyPhotoURL;
    public int _enemyPlayerLevel = 1;
    public int _openCardInfo;
    public List<MultiplayerBuildingData> _buildingMultiplayerDataRef;
    public BuildingManager _buildingManagerRef;
    public int _buildingCount;

    public List<GameObject> _LevelHolder = new List<GameObject>();

    public bool onceDone = false;

    public List<GameObject> _enemyBuildingDetails;

    public List<GameObject> mEnemyBuildingPrefabPopulateList;
    public List<GameObject> _enemyBuildings;

    [SerializeField] private GameObject mTransformPoint;
    public List<Transform> _enemyBuildingsTransformList;

    
    public void Start()
    {
        onceDone = false;
        //InvokeRepeating(nameof(PopulateEnemyBuildingPrefabs), 1,1);
    }

    //void PopulateEnemyBuildingPrefabs()
    //{
    //    Debug.LogError("I'm also coming here");
    //    //Instantiate(_LevelHolder[_enemyPlayerLevel - 1], Vector3.zero, Quaternion.identity);
    //    //mTransformPoint = GameObject.Find("TransformPoints");
    //    //Debug.LogError(mMultiplayerPlayerData._enemyBuildingDetails.Count + " extra " + mMultiplayerPlayerData._buildingMultiplayerDataRef.Count);
    //    Debug.LogError(_buildingMultiplayerDataRef[0]._buildingName + _buildingMultiplayerDataRef[0]._buildingCurrentLevel);
    //        GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + _buildingMultiplayerDataRef[0]._buildingName + _buildingMultiplayerDataRef[0]._buildingCurrentLevel) as GameObject;
    //        mEnemyBuildingPrefabPopulateList.Add(building);
    //    //for (int i = 0; i < _buildingMultiplayerDataRef.Count; i++)
    //    //{
    //    //    Debug.LogError("HI");
    //    //}
    //    Debug.LogError("But not past this");

    //    //Debug.LogError(mTransformPoint.transform.childCount);
    //    //for (int i = 0; i < mTransformPoint.transform.childCount; i++)
    //    //{
    //    //    _enemyBuildingsTransformList.Add(mTransformPoint.transform.GetChild(i));
    //    //}
    //    //for (int i = 0; i < _buildingMultiplayerDataRef.Count; i++)
    //    //{
    //    //    Debug.LogError("HI");
    //    //    GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + _buildingMultiplayerDataRef[i]._buildingName + _buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
    //    //    _enemyBuildingDetails.Add(building);
    //    //}
    //}

    public void Update()
    {
        //GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + _buildingMultiplayerDataRef[0]._buildingName + _buildingMultiplayerDataRef[0]._buildingCurrentLevel) as GameObject;
        //mEnemyBuildingPrefabPopulateList.Add(building);
        //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ATTACK" && !onceDone)
        //{

        //    //for (int i = 0; i < _buildingMultiplayerDataRef.Count; i++)
        //    //{
        //    //    Debug.LogError("HI");
        //    //    GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + _buildingMultiplayerDataRef[i]._buildingName + _buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
        //    //    _enemyBuildingDetails.Add(building);
        //    //}
        //    onceDone = true;
        //}
        //else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "ATTACK")
        //{
        //    _enemyBuildingDetails.Clear();
        //    onceDone = false;
        //}
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

    public void UpdateUserDetails(List<MultiplayerBuildingData> inBuildingData, int inCurrentLevel, int inOpenCardData, string inPlayerName, string inPlayerPhotoURL)
    {
        //Debug.LogError("I Reached Till Here 1");
        _buildingMultiplayerDataRef = inBuildingData;
        _enemyPlayerLevel = inCurrentLevel;
        _openCardInfo = inOpenCardData;
        _enemyName = inPlayerName;
        _enemyPhotoURL = inPlayerPhotoURL;
        //Debug.LogError("I Reached Till Here 2");
        //PopulateEnemyBuildingPrefabs();
        //chumma();
        //Debug.LogError("I Reached Till Here 3");



        //Instantiate(_LevelHolder[_enemyPlayerLevel - 1], Vector3.zero, Quaternion.identity);


    }

    //void chumma()
    //{
    //    Debug.LogError("HI");
    //}

    public void AddShieldToBuilding(int inBuildingIndex)
    {
        _buildingMultiplayerDataRef[inBuildingIndex]._isBuildingShielded = true;
    }

}
