using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Trying this
[System.Serializable]
public class BuildingData
{
    [Header ("Building Name And Level: ")]
    public string _buildingName;
    public int _buildingLevel = 0;
    public int _buildingMaxLevel;
    public Transform _buildingSpawnPoint;
    
    [Header ("Building's GameObject: ")]
    public GameObject _initialBuildingGameObject;
    public GameObject[] UpgradeLevels;
    public GameObject[] destroyedVersions; //Just in Case for Future

    [Header ("State Checkers: ")]
    public bool isBuildingSpawnedAndActive; //Just in case for Attack
    public bool isBuildingDamaged; //Just in case to check if building is damaged or not.
    public bool didBuildingReachMaxLevel;
}

public class BuildingManager : MonoBehaviour
{
    public BuildingData[] _buildingData;
    public List<GameObject> _buildingsList;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _buildingData.Length; i++)
        {
            GameObject GORef = Instantiate(_buildingData[i]._initialBuildingGameObject, _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
            GORef.name = _buildingData[i]._buildingName;

            _buildingsList.Add(GORef);
        }
    }

    /// <summary>
    /// Upgrades the building by finding its appropriate name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="inBuildingNumber"></param>
    /// <param name="inLevel"></param>
    /// <param name="inCurrentLevelsMesh"></param>
    public void UpgradeBuilding(string name , int inBuildingNumber , int inLevel , GameObject inCurrentLevelsMesh)
    {
        GameObject goRef = GameObject.Find(name);
        Debug.Log(goRef);
        Destroy(goRef);
        //if (inLevel != 0)
        //{
        //    GameObject oldMesh = inCurrentLevelsMesh;
        //    Destroy(oldMesh);   
        //}
        GameObject newGoRef = Instantiate(_buildingData[inBuildingNumber].UpgradeLevels[inLevel], _buildingData[inBuildingNumber]._buildingSpawnPoint.position, _buildingData[inBuildingNumber]._buildingSpawnPoint.rotation);
        newGoRef.name = _buildingData[inBuildingNumber]._buildingName;
        newGoRef.AddComponent<BuildingDetails>();
        BuildingDetails buildingDetailRef = newGoRef.GetComponent<BuildingDetails>();
        buildingDetailRef._buildingLevel = inLevel;
        buildingDetailRef._buildMeshBasedOnCurrentLevel = inCurrentLevelsMesh;
    }

    public void Building1Upgrade()
    {
        if (_buildingData[0]._buildingLevel < _buildingData[0].UpgradeLevels.Length && _buildingData[0]._buildingLevel < _buildingData[0]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[0]._buildingName, 0, _buildingData[0]._buildingLevel,_buildingData[0].UpgradeLevels[_buildingData[0]._buildingLevel]);
            _buildingData[0]._buildingLevel += 1;
        }                                   
        else
        {
            _buildingData[0].didBuildingReachMaxLevel = true;
        }
    }

    public void Building2Upgrade()
    {
        if (_buildingData[1]._buildingLevel < _buildingData[1].UpgradeLevels.Length && _buildingData[1]._buildingLevel < _buildingData[1]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[1]._buildingName, 1, _buildingData[1]._buildingLevel, _buildingData[1].UpgradeLevels[_buildingData[1]._buildingLevel]);
            _buildingData[1]._buildingLevel += 1;
        }
        else
        {
            _buildingData[1].didBuildingReachMaxLevel = true;
        }
    }

    public void Building3Upgrade()
    {
        if (_buildingData[2]._buildingLevel < _buildingData[2].UpgradeLevels.Length && _buildingData[2]._buildingLevel < _buildingData[2]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[2]._buildingName, 2, _buildingData[2]._buildingLevel, _buildingData[2].UpgradeLevels[_buildingData[2]._buildingLevel]);
            _buildingData[2]._buildingLevel += 1;
        }
        else
        {
            _buildingData[2].didBuildingReachMaxLevel = true;
        }
    }

    public void Building4Upgrade()
    {
        if (_buildingData[3]._buildingLevel < _buildingData[3].UpgradeLevels.Length && _buildingData[3]._buildingLevel < _buildingData[3]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[3]._buildingName, 3, _buildingData[3]._buildingLevel, _buildingData[3].UpgradeLevels[_buildingData[3]._buildingLevel]);
            _buildingData[3]._buildingLevel += 1;
        }
        else
        {
            _buildingData[3].didBuildingReachMaxLevel = true;
        }
    }

    public void Building5Upgrade()
    {
        if (_buildingData[4]._buildingLevel < _buildingData[4].UpgradeLevels.Length && _buildingData[4]._buildingLevel < _buildingData[4]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[4]._buildingName, 4, _buildingData[4]._buildingLevel, _buildingData[4].UpgradeLevels[_buildingData[4]._buildingLevel]);
            _buildingData[4]._buildingLevel += 1;
        }
        else
        {
            _buildingData[4].didBuildingReachMaxLevel = true;
        }
    }

    public void Building6Upgrade()
    {
        if (_buildingData[5]._buildingLevel < _buildingData[5].UpgradeLevels.Length && _buildingData[5]._buildingLevel < _buildingData[5]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[5]._buildingName, 5, _buildingData[5]._buildingLevel, _buildingData[5].UpgradeLevels[_buildingData[5]._buildingLevel]);
            _buildingData[5]._buildingLevel += 1;
        }
        else
        {
            _buildingData[5].didBuildingReachMaxLevel = true;
        }
    }

    public void Building7Upgrade()
    {
        if (_buildingData[6]._buildingLevel < _buildingData[6].UpgradeLevels.Length && _buildingData[6]._buildingLevel < _buildingData[6]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[6]._buildingName, 6, _buildingData[6]._buildingLevel, _buildingData[6].UpgradeLevels[_buildingData[6]._buildingLevel]);
            _buildingData[6]._buildingLevel += 1;
        }
        else
        {
            _buildingData[6].didBuildingReachMaxLevel = true;
        }
    }

    public void Building8Upgrade()
    {
        if (_buildingData[7]._buildingLevel < _buildingData[7].UpgradeLevels.Length && _buildingData[7]._buildingLevel < _buildingData[7]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[7]._buildingName, 7, _buildingData[7]._buildingLevel, _buildingData[7].UpgradeLevels[_buildingData[7]._buildingLevel]);
            _buildingData[7]._buildingLevel += 1;
        }
        else
        {
            _buildingData[7].didBuildingReachMaxLevel = true;
        }
    }
}



//void residue()
//{
//    public BuildingDetails[] _building;
//public List<GameObject> _buildings;

//int i = 0;
//int j = 0;
//int k = 0;
//// Start is called before the first frame update
//void Start()
//{

//    for (int i = 0; i < _building.Length; i++)
//    {
//        GameObject GO = new GameObject();
//        GO.AddComponent<MeshFilter>().mesh = _building[i]._initialBuildingMesh;
//        GO.AddComponent<MeshRenderer>().material = _building[i]._material;
//        GO.AddComponent<TestScript>();
//        GO.transform.position = _building[i].transformPoint.position;
//        GO.name = _building[i]._name;
//        _buildings.Add(GO);
//    }
//}

//// Update is called once per frame
//void Update()
//{

//}

//public void UpgradeBuilding(string name, int inBuildingNumber, int inLevel)
//{
//    GameObject goRef = GameObject.Find(name);
//    goRef.GetComponent<MeshFilter>().mesh = _building[inBuildingNumber].UpgradeLevels[inLevel];
//    //Access the building script & keepChaning inLevel Number to Level
//    goRef.GetComponent<TestScript>().BuildingLevel = inLevel;
//}

//public void CubeUpgrade()
//{
//    UpgradeBuilding("Cube", 0, i);
//    i++;
//}

//public void CylinderUpgrade()
//{
//    UpgradeBuilding("Cylinder", 1, j);
//    j++;
//}

//public void CircleUpgrade()
//{
//    UpgradeBuilding("Circle", 2, k);
//    k++;
//}
