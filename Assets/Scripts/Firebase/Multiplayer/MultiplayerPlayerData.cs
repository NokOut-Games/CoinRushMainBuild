using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class MultiplayerPlayerData : MonoBehaviour
{
    public string _enemyName;
    public int _enemyPlayerLevel = 1;
    public int _attackInfo;
    public List<Building> _buildingMultiplayerDataRef;
    public BuildingManager _buildingManagerRef;
    public int _buildingCount;

    public List<GameObject> _LevelHolder = new List<GameObject>();

    public bool onceDone = false;

    public List<GameObject> _enemyBuildingDetails;

    public List<GameObject> mEnemyBuildingPrefabPopulateList;
    public List<GameObject> _enemyBuildings;

    public List<Transform> _enemyBuildingsTransformList;

    public List<OpenCard> OpenCardDetails;
    public List<int> OpenedCardSlot = new List<int>();


    public void Start()
    {
        onceDone = false;
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
    public void UpdateUserDetails(List<Building> inBuildingData, int inCurrentLevel, int inOpenCardData, string inPlayerName)
    {
        _buildingMultiplayerDataRef = inBuildingData;
        _enemyPlayerLevel = inCurrentLevel;
        _attackInfo = inOpenCardData;
        _enemyName = inPlayerName;
    }

    public void UpdateOpenCardDetails(List<OpenCard> inOpenCardDetails, List<int> inOpenCardSlot)
    {
        OpenCardDetails = inOpenCardDetails;
        OpenedCardSlot = inOpenCardSlot;
    }
}

