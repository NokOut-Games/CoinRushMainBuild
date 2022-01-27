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
        //Instantiate(_LevelHolder[_otherPlayerCurrentLevel - 1], Vector3.zero, Quaternion.identity);
        //mTransformPoint = GameObject.Find("TransformPoints");

        //for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
        //{
        //    GameObject building = Resources.Load("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
        //    mOtherPlayerBuildingPrefabPopulateList.Add(building);
        //}
        //for (int i = 0; i < mTransformPoint.transform.childCount; i++)
        //{
        //    _otherPlayerBuildingsTransformList.Add(mTransformPoint.transform.GetChild(i));
        //}
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
            //if(building == null)
            //{

            //}
            mOtherPlayerBuildingPrefabPopulateList.Add(building);
            //bool shieldedEnemy = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingShielded;
            //_shieldedEnemyBuildings.Add(shieldedEnemy);
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
            //if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel <= 0)
            //{
            //    if (!mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingDestroyed)
            //    {

            //        //building.transform.position = _enemyBuildingsTransformList[i].position;
            //        //building.transform.rotation = Quaternion.identity;
            //        //_enemyBuildings.Add(building);
            //    }
            //    else
            //    {
            //        GameObject building = Resources.Load("Plunk_Main") as GameObject;
            //        building.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "0";
            //        Sprite BuildingImage = Resources.Load("Level" + _enemyPlayerLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "Image") as Sprite;
            //        //Debug.LogError(building.name);
            //        building.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
            //        building.transform.position = new Vector3(building.transform.position.x, _buildingSinkPositionAmount, building.transform.position.z);
            //        building.transform.rotation = Quaternion.Euler(building.transform.eulerAngles.x, building.transform.eulerAngles.y, -(_buildingTiltRotationAmount));
            //        Instantiate(_destroyedSmokeEffectVFX, _enemyBuildingsTransformList[i].position, Quaternion.identity, building.transform);
            //        _enemyBuildings.Add(building);
            //    }
            //}
            //else
            //{
            if (!mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingDestroyed)
            {
                if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel != 0)
                {
                    otherBuildings = Instantiate(/*mGameManager._BuildingDetails*/ mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
                }
                else
                {
                    otherBuildings = Instantiate(/*mGameManager._BuildingDetails*/ mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);
                }
                otherBuildings.name = mOtherPlayerBuildingPrefabPopulateList[i].name;
                otherBuildings.name = otherBuildings.name.Substring(0, otherBuildings.name.Length - 1);
                _otherBuildings.Add(otherBuildings);
            }
            else
            {
                if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel != 0)
                {
                    otherBuildings = Instantiate(/*mGameManager._BuildingDetails*/ mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
                }
                else
                {
                    otherBuildings = Instantiate(/*mGameManager._BuildingDetails*/ mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, Quaternion.identity);

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

        //for (int i = 0; i < mOtherPlayerBuildingPrefabPopulateList.Count; i++)
        //{
        //    GameObject enemyBuilding = Instantiate(/*mGameManager._BuildingDetails*/ mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
        //    enemyBuilding.name = mOtherPlayerBuildingPrefabPopulateList[i].name;
        //    enemyBuilding.name = enemyBuilding.name.Substring(0, enemyBuilding.name.Length - 1);
        //    _otherPlayerBuildings.Add(enemyBuilding);
        //}
    }
    private void Update()
    {
        _OpenCardNumberIndex = mMultiplayerPlayerData._openCardInfo;
        mPositionNumber = mMultiplayerPlayerData._openCardInfo;

    }
    //public void OpenCardAdder()
    //{
    //    if (mPositionNumber >= _OpenCardNumberIndex && mPositionNumber <= 4)
    //    {
    //        for (int i = 0; i < OpenHandCardsPositions.Length; i++)
    //        {
    //            if (OpenHandCardsPositions[mPositionNumber].GetComponent<OpenCardFilled>().isOpenCardSlotFilled == false && mCardsOpened < 1)
    //            {
    //                _openedCardIndex = Random.Range(0, dummyCards.Count);
    //                Debug.Log(_openedCardIndex);
    //                Instantiate(dummyCards[_openedCardIndex], OpenHandCardsPositions[mPositionNumber].position, OpenHandCardsPositions[mPositionNumber].rotation, OpenHandCardsPositions[mPositionNumber]);
    //                mCardsOpened += 1;
    //                break;
    //            }
    //        }
    //        mPositionNumber += 1;
    //    }
    //}
    public void GoBackToGame()
    {
        MultiplayerManager.Instance.WriteOpenCardDataToFirebase();
    }
}