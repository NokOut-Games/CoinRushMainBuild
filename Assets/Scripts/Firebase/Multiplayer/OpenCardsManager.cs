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

        _otherPlayerDisplayPicture.texture = mMultiplayerPlayerData._enemyImageTexture;
        _otherPlayerName.text = mMultiplayerPlayerData._enemyName+"'s" + " Island";
        _otherPlayerCurrentLevel = mMultiplayerPlayerData._enemyPlayerLevel;
        InstantiateLevelAndPopulateBuildingPrefabsWithTheirTranformPoint();

        Invoke(nameof(InstantiatePopulatedBuildingPrefabList), 0f);
    }

    void InstantiateLevelAndPopulateBuildingPrefabsWithTheirTranformPoint()
    {
        Instantiate(_LevelHolder[_otherPlayerCurrentLevel - 1], Vector3.zero, Quaternion.identity);
        mTransformPoint = GameObject.Find("TransformPoints");

        for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
        {
            GameObject building;
            if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel <= 0)
            {
                building = Resources.Load("Plunk_Main") as GameObject;
                building.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "0";
                Sprite BuildingImage = Resources.Load<Sprite>("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "Image");
                building.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                //mEnemyBuildingPrefabPopulateList.Add();
            }
            else
            {
                building = Resources.Load("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
            }
            mOtherPlayerBuildingPrefabPopulateList.Add(building);
        }

        for (int i = 0; i < mTransformPoint.transform.childCount; i++)
        {
            _otherPlayerBuildingsTransformList.Add(mTransformPoint.transform.GetChild(i));
        }
    }

    void InstantiatePopulatedBuildingPrefabList()
    {

        for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
        {
            GameObject otherBuildings;
            if (!mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingDestroyed)
            {
                if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel != 0)
                {
                    otherBuildings = Instantiate(mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
                }
                else
                {
                    otherBuildings = Instantiate(mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);
                }
                otherBuildings.name = mOtherPlayerBuildingPrefabPopulateList[i].name;
                otherBuildings.name = otherBuildings.name.Substring(0, otherBuildings.name.Length - 1);
                _otherBuildings.Add(otherBuildings);
            }
            else
            {
                if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel != 0)
                {
                    otherBuildings = Instantiate(mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
                }
                else
                {
                    otherBuildings = Instantiate(mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);

                }
                otherBuildings.transform.position = new Vector3(otherBuildings.transform.position.x, _buildingSinkPositionAmount, otherBuildings.transform.position.z);
                otherBuildings.transform.rotation = Quaternion.Euler(otherBuildings.transform.eulerAngles.x, otherBuildings.transform.eulerAngles.y, _buildingTiltRotationAmount);
                Instantiate(_destroyedSmokeEffectVFX, otherBuildings.transform.position, Quaternion.identity, otherBuildings.transform);
                otherBuildings.name = mOtherPlayerBuildingPrefabPopulateList[i].name;
                otherBuildings.name = otherBuildings.name.Substring(0, otherBuildings.name.Length - 1);
                _otherBuildings.Add(otherBuildings);
            }
            // }
        }
    }
    private void Update()
    {
        _OpenCardNumberIndex = mMultiplayerPlayerData._openCardInfo;
        mPositionNumber = mMultiplayerPlayerData._openCardInfo;

    }

    public void GoBackToGame()
    {
        MultiplayerManager.Instance.WriteOpenCardDataToFirebase();
    }
}