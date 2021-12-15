using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trying this
[System.Serializable]
public class BuildingDetails
{
    public Mesh _buildingMesh;
    public Mesh[] UpgradeLevels;
    public string _name;
    public int _materialCount; //
    public Material _material;
    public Transform transformPoint;
}

public class BuildingManager : MonoBehaviour
{
    public BuildingDetails[] _building;
    public List<GameObject> _buildings;

    int i = 0;
    int j = 0;
    int k = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _building.Length; i++)
        {
            GameObject GO = new GameObject();
            GO.AddComponent<MeshFilter>().mesh = _building[i]._buildingMesh;
            GO.AddComponent<MeshRenderer>().material = _building[i]._material;
            GO.AddComponent<TestScript>();
            GO.transform.position = _building[i].transformPoint.position;
            GO.name = _building[i]._name;
            _buildings.Add(GO);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeBuilding(string name , int inBuildingNumber , int inLevel)
    {
        GameObject goRef = GameObject.Find(name);
        goRef.GetComponent<MeshFilter>().mesh = _building[inBuildingNumber].UpgradeLevels[inLevel];
        //Access the building script & keepChaning inLevel Number to Level
        goRef.GetComponent<TestScript>().BuildingLevel = inLevel;
    }

    public void CubeUpgrade()
    {
        UpgradeBuilding("Cube", 0 , i);
        i++;
    }

    public void CylinderUpgrade()
    {
        UpgradeBuilding("Cylinder", 1 , j);
        j++;
    }

    public void CircleUpgrade()
    {
        UpgradeBuilding("Circle", 2 , k);
        k++;
    }
}
