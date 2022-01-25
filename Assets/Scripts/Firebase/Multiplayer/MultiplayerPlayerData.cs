using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
    public Texture _enemyImageTexture;
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
    }


    public void Update()
    {
        if (_enemyPhotoURL != null)
        {
            StartCoroutine(DownloadFacebookImage(_enemyPhotoURL));
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
    public void AddShieldToBuilding(int inBuildingIndex)
    {
        _buildingMultiplayerDataRef[inBuildingIndex]._isBuildingShielded = true;
    }
    public void UpdateUserDetails(List<MultiplayerBuildingData> inBuildingData, int inCurrentLevel, int inOpenCardData, string inPlayerName, string inPlayerPhotoURL)
    {
        _buildingMultiplayerDataRef = inBuildingData;
        _enemyPlayerLevel = inCurrentLevel;
        _openCardInfo = inOpenCardData;
        _enemyName = inPlayerName;
        _enemyPhotoURL = inPlayerPhotoURL;
    }
    IEnumerator DownloadFacebookImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
            Debug.Log(request.error);
        else
        {
            _enemyImageTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Debug.Log("Getting texture");
        }
    }
}
