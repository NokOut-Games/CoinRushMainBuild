using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Trying this
[System.Serializable]
public class BuildingData
{
    [Header("Building Name And Level: ")]
    public string _buildingName;
    public int _buildingLevel = 0;
    public int _buildingMaxLevel;
    public Transform _buildingSpawnPoint;

    [Header("Building's GameObject: ")]
    public GameObject _initialBuildingGameObject;
    public GameObject currentLevelGameObject;
    public Sprite[] NextUpgradeImages; //Future
    public GameObject[] UpgradeLevels;
    public GameObject[] destroyedVersions; //Just in Case for Future

    [Header("State Checkers: ")]
    public bool isBuildingSpawnedAndActive; //Just in case for Attack
    public bool isBuildingDamaged; //Just in case to check if building is damaged or not.
    public bool didBuildingReachMaxLevel;
}

public class BuildingManager : MonoBehaviour
{
    public List<BuildingData> _buildingData;
    public List<GameObject> _buildingsList;

    public delegate void BuildingMethod();

    public List<BuildingMethod> _buildingMethod = new List<BuildingMethod>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _buildingData.Count; i++)
        {
            if (!_buildingData[i].isBuildingSpawnedAndActive)
            {
                GameObject GORef = Instantiate(_buildingData[i]._initialBuildingGameObject, _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
                GORef.name = _buildingData[i]._buildingName;
                _buildingData[i].isBuildingSpawnedAndActive = true;

                _buildingsList.Add(GORef);
            }
            else
            {
                //Do a loading probably
            }
        }
    }

    /// <summary>
    /// Upgrades the building by finding its appropriate name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="inBuildingNumber"></param>
    /// <param name="inLevel"></param>
    /// <param name="inCurrentLevelsMesh"></param>
    public void UpgradeBuilding(string name, int inBuildingNumber, int inLevel, GameObject inCurrentLevelsMesh)
    {
        GameObject goRef = GameObject.Find(name);
        Destroy(goRef);

        GameObject newGoRef = Instantiate(_buildingData[inBuildingNumber].UpgradeLevels[inLevel], _buildingData[inBuildingNumber]._buildingSpawnPoint.position, _buildingData[inBuildingNumber]._buildingSpawnPoint.rotation);
        newGoRef.name = _buildingData[inBuildingNumber]._buildingName;

        _buildingData[inBuildingNumber].currentLevelGameObject = inCurrentLevelsMesh;

        //Just in case if these data's are required
        newGoRef.AddComponent<BuildingDetails>();
        BuildingDetails buildingDetailRef = newGoRef.GetComponent<BuildingDetails>();
        buildingDetailRef._buildingLevel = inLevel;
        buildingDetailRef._buildMeshBasedOnCurrentLevel = inCurrentLevelsMesh;
        //yield return null;
    }

    public void GrabElementNumberBasedOnButtonClick(int inElementNumber)
    {
        if (_buildingData[inElementNumber]._buildingLevel < _buildingData[inElementNumber]._buildingMaxLevel)
        {
            UpgradeBuilding(_buildingData[inElementNumber]._buildingName, inElementNumber, _buildingData[inElementNumber]._buildingLevel, _buildingData[inElementNumber].UpgradeLevels[_buildingData[inElementNumber]._buildingLevel]);
            _buildingData[inElementNumber]._buildingLevel += 1;
        }
        if (_buildingData[inElementNumber]._buildingLevel == _buildingData[inElementNumber]._buildingMaxLevel)
        {
            _buildingData[inElementNumber].didBuildingReachMaxLevel = true;
        }
    }

    #region Manual Button Functions
    /// <summary>
    /// Function for Button-1
    /// </summary>
    public void Building1Upgrade()
    {
        GrabElementNumberBasedOnButtonClick(0);
    }

    /// <summary>
    /// Function for Button-2
    /// </summary>
    public void Building2Upgrade()
    {
        GrabElementNumberBasedOnButtonClick(1);
    }

    /// <summary>
    /// Function for Button-3
    /// </summary>
    public void Building3Upgrade()
    {
        GrabElementNumberBasedOnButtonClick(2);
    }

    /// <summary>
    /// Function for Button-4
    /// </summary>
    public void Building4Upgrade()
    {
        GrabElementNumberBasedOnButtonClick(3);
    }

    /// <summary>
    /// Function for Button-5
    /// </summary>
    public void Building5Upgrade()
    {
        GrabElementNumberBasedOnButtonClick(4);
    }

    /// <summary>
    /// Function for Button-6
    /// </summary>
    public void Building6Upgrade()
    {
        GrabElementNumberBasedOnButtonClick(5);
    }

    /// <summary>
    /// Function for Button-7
    /// </summary>
    public void Building7Upgrade()
    {
        GrabElementNumberBasedOnButtonClick(6);
    }

    /// <summary>
    /// Function for Button-8
    /// </summary>
    public void Building8Upgrade()
    {
        GrabElementNumberBasedOnButtonClick(7);
    }

    #endregion
}



//if (inLevel != 0)
//{
//    GameObject oldMesh = inCurrentLevelsMesh;
//    Destroy(oldMesh);   
//}

/*_buildingData[inElementNumber]._buildingLevel < _buildingData[inElementNumber].UpgradeLevels.Length &&*/

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


