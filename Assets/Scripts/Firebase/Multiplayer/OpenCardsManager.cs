
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    public string Name;
    public Sprite Picture;
    public int Level=1;
    public List<Building> Buildings = new List<Building>();
}

public class OpenCardsManager : MonoBehaviour
{
    public int _otherPlayerCurrentLevel;
    private GameObject mTransformPoint;

    public List<GameObject> _LevelHolder = new List<GameObject>();
    List<GameObject> _otherPlayerBuildings = new List<GameObject>();

    public float _buildingSinkPositionAmount = -50;
    public float _buildingTiltRotationAmount = -25;

    [SerializeField] GameObject _destroyedSmokeEffectVFX;
    List<Transform> _otherPlayerBuildingsTransformList = new List<Transform>();

    private void Awake()
    {
        _otherPlayerCurrentLevel = MultiplayerManager.Instance.Enemydata.UserDetails._playerCurrentLevel;

        InstantiateLevelAndPopulateTransformPoints();
        InstantiateBuildings();
    }

    void InstantiateLevelAndPopulateTransformPoints()
    {
        Instantiate(_LevelHolder[_otherPlayerCurrentLevel - 1], Vector3.zero, Quaternion.identity);
        mTransformPoint = GameObject.Find("TransformPoints");

        for (int i = 0; i < mTransformPoint.transform.childCount; i++)
        {
            _otherPlayerBuildingsTransformList.Add(mTransformPoint.transform.GetChild(i));
        }
    }

    void InstantiateBuildings()
    {
        for (int i = 0; i < MultiplayerManager.Instance.MultiplayerBuildingDetails.Count; i++)
        {
            GameObject otherPlayerBuilding;

            if (!MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._isBuildingDestroyed)
            {
                if (MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel != 0)
                {
                    GameObject building = Resources.Load("Level" + _otherPlayerCurrentLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel) as GameObject;

                    otherPlayerBuilding = Instantiate(building, _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
                    otherPlayerBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;

                    otherPlayerBuilding = Instantiate(building, _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _otherPlayerCurrentLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "Image");
                    otherPlayerBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    otherPlayerBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "0";
                }
                _otherPlayerBuildings.Add(otherPlayerBuilding);
            }
            else
            {
                if (MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel != 0)
                {
                    GameObject building = Resources.Load("Level" + _otherPlayerCurrentLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel) as GameObject;
                    otherPlayerBuilding = Instantiate( building, _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
                    otherPlayerBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;
                    otherPlayerBuilding = Instantiate(building, _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _otherPlayerCurrentLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "Image");
                    otherPlayerBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    otherPlayerBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "0";

                }
                otherPlayerBuilding.transform.position = new Vector3(otherPlayerBuilding.transform.position.x, _buildingSinkPositionAmount, otherPlayerBuilding.transform.position.z);
                otherPlayerBuilding.transform.rotation = Quaternion.Euler(otherPlayerBuilding.transform.eulerAngles.x, otherPlayerBuilding.transform.eulerAngles.y, _buildingTiltRotationAmount);
                Instantiate(_destroyedSmokeEffectVFX, otherPlayerBuilding.transform.position, Quaternion.identity, otherPlayerBuilding.transform);
                _otherPlayerBuildings.Add(otherPlayerBuilding);
            }
        }
    }
    public void GoBackToGame()
    {
        MultiplayerManager.Instance.BackToGame();
    }
}