using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCardsManager : MonoBehaviour
{
    public MultiplayerPlayerData mMultiplayerPlayerData;
    public CardDeck _cardDeck;
    public int _otherPlayerCurrentLevel;
    public List<GameObject> mOtherPlayerBuildingPrefabPopulateList;
    public List<GameObject> _otherPlayerBuildings;
    [SerializeField] private GameObject mTransformPoint;
    public List<Transform> _otherPlayerBuildingsTransformList;
    public List<GameObject> _LevelHolder = new List<GameObject>();
    private GameManager mGameManager;


    [Space]
    [Header("OtherPlayerDetails")]
    public Text _otherPlayerName;
    public RawImage _otherPlayerDisplayPicture;
    [Space]

    private int mPositionNumber;
    public int _OpenCardNumberIndex;

    public int _openedCardIndex;
    public List<GameObject> dummyCards;
    public Transform[] OpenHandCardsPositions;

    public float _buildingSinkPositionAmount = -50;
    public float _buildingTiltRotationAmount = -25;
    public GameObject _destroyedSmokeEffectVFX;

    public List<GameObject> _otherBuildings;

    int mCardsOpened;

    private void Awake()
    {
        
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mMultiplayerPlayerData = GameObject.Find("MultiplayerManager").GetComponent<MultiplayerPlayerData>();

        //New change from Multiplayer manager

       // _otherPlayerDisplayPicture.texture = mMultiplayerPlayerData._enemyImageTexture;
       // _otherPlayerName.text = mMultiplayerPlayerData._enemyName+"'s" + " Island";
       // _otherPlayerCurrentLevel = mMultiplayerPlayerData._enemyPlayerLevel;

        _otherPlayerName.text = MultiplayerManager.Instance.mPlayerNameData + "'s" + " Island";
        _otherPlayerCurrentLevel = MultiplayerManager.Instance._enemyPlayerLevel;


        InstantiateLevelAndPopulateTransformPoints();
        

        Invoke(nameof(InstantiateBuildingBasedOnLevel), 0f);
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

    //    void InstantiateLevelAndPopulateBuildingPrefabsWithTheirTranformPoint()
    //    {
    //        Instantiate(_LevelHolder[_otherPlayerCurrentLevel - 1], Vector3.zero, Quaternion.identity);
    //        mTransformPoint = GameObject.Find("TransformPoints");

    //        for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
    //        {
    //            GameObject building;
    //            if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel <= 0)
    //            {
    //                 building = Resources.Load("Plunk_Main") as GameObject;

    //                building.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "0";

    //                Sprite BuildingImage = Resources.Load<Sprite>("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef
    //[i]._buildingName + "Image");

    //                building.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
    //                //mEnemyBuildingPrefabPopulateList.Add();
    //            }
    //            else
    //            {
    //                building = Resources.Load("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
    //            }
    //           // Debug.LogError(building.name);
    //           mOtherPlayerBuildingPrefabPopulateList.Add(building);
    //        }

    //        for (int i = 0; i < mTransformPoint.transform.childCount; i++)
    //        {
    //            _otherPlayerBuildingsTransformList.Add(mTransformPoint.transform.GetChild(i));
    //        }
    //    }

    //void InstantiateBuildingBasedOnLevel()
    //{
    //    for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
    //    {
    //        GameObject otherPlayerBuilding;

    //        if (!mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingDestroyed)
    //        {
    //            if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel != 0)
    //            {
    //                GameObject building = Resources.Load("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;

    //                otherPlayerBuilding = Instantiate(building, _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
    //                otherPlayerBuilding.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel;
    //            }
    //            else
    //            {
    //                GameObject building = Resources.Load("Plunk_Attack") as GameObject;

    //                otherPlayerBuilding = Instantiate(building, _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);
    //                Sprite BuildingImage = Resources.Load<Sprite>("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "Image");
    //                otherPlayerBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
    //                otherPlayerBuilding.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "0";
    //            }
    //            _otherPlayerBuildings.Add(otherPlayerBuilding);
    //        }
    //        else
    //        {
    //            if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel != 0)
    //            {
    //                GameObject building = Resources.Load("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
    //                otherPlayerBuilding = Instantiate(/*mGameManager._BuildingDetails*/ building, _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
    //                otherPlayerBuilding.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel;
    //            }
    //            else
    //            {
    //                GameObject building = Resources.Load("Plunk_Attack") as GameObject;
    //                otherPlayerBuilding = Instantiate(/*mGameManager._BuildingDetails*/building, _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);
    //                Sprite BuildingImage = Resources.Load<Sprite>("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "Image");
    //                otherPlayerBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
    //                otherPlayerBuilding.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "0";

    //            }
    //            otherPlayerBuilding.transform.position = new Vector3(otherPlayerBuilding.transform.position.x, _buildingSinkPositionAmount, otherPlayerBuilding.transform.position.z);
    //            otherPlayerBuilding.transform.rotation = Quaternion.Euler(otherPlayerBuilding.transform.eulerAngles.x, otherPlayerBuilding.transform.eulerAngles.y, _buildingTiltRotationAmount);
    //            Instantiate(_destroyedSmokeEffectVFX, otherPlayerBuilding.transform.position, Quaternion.identity, otherPlayerBuilding.transform);
    //            _otherPlayerBuildings.Add(otherPlayerBuilding);
    //        }
    //        // }
    //    }
    //}


    void InstantiateBuildingBasedOnLevel()
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
                    otherPlayerBuilding = Instantiate(/*mGameManager._BuildingDetails*/ building, _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
                    otherPlayerBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;
                    otherPlayerBuilding = Instantiate(/*mGameManager._BuildingDetails*/building, _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _otherPlayerCurrentLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "Image");
                    otherPlayerBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    otherPlayerBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "0";

                }
                otherPlayerBuilding.transform.position = new Vector3(otherPlayerBuilding.transform.position.x, _buildingSinkPositionAmount, otherPlayerBuilding.transform.position.z);
                otherPlayerBuilding.transform.rotation = Quaternion.Euler(otherPlayerBuilding.transform.eulerAngles.x, otherPlayerBuilding.transform.eulerAngles.y, _buildingTiltRotationAmount);
                Instantiate(_destroyedSmokeEffectVFX, otherPlayerBuilding.transform.position, Quaternion.identity, otherPlayerBuilding.transform);
                _otherPlayerBuildings.Add(otherPlayerBuilding);
            }
            // }
        }
    }

    private void Update()
    {
       // _OpenCardNumberIndex = mMultiplayerPlayerData._openCardInfo;
     //   mPositionNumber = mMultiplayerPlayerData._openCardInfo;

    }

    public void GoBackToGame()
    {
        FirebaseManager.Instance.ReadData(false);
        MultiplayerManager.Instance.BackToGame();
    }
}