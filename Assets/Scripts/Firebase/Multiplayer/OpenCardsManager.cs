using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCardsManager : MonoBehaviour
{
    public MultiplayerPlayerData mMultiplayerPlayerData;
    public int _otherPlayerCurrentLevel;
    public List<GameObject> mOtherPlayerBuildingPrefabPopulateList;
    public List<GameObject> _otherPlayerBuildings;
    [SerializeField] private GameObject mTransformPoint;
    public List<Transform> _otherPlayerBuildingsTransformList;
    public List<GameObject> _LevelHolder = new List<GameObject>();
    private GameManager mGameManager;

    private int mPositionNumber;
    public int _OpenCardNumberIndex;

    public int _openedCardIndex;
    public List<GameObject> dummyCards;
    public Transform[] OpenHandCardsPositions;
    int mCardsOpened;
    private void Awake()
    {
        
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mMultiplayerPlayerData = GameObject.Find("MultiplayerManager").GetComponent<MultiplayerPlayerData>();
        _otherPlayerCurrentLevel = mMultiplayerPlayerData._enemyPlayerLevel;
        Instantiate(_LevelHolder[_otherPlayerCurrentLevel - 1], Vector3.zero, Quaternion.identity);
        mTransformPoint = GameObject.Find("TransformPoints");

        for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
        {
            GameObject building = Resources.Load("Level" + _otherPlayerCurrentLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
            mOtherPlayerBuildingPrefabPopulateList.Add(building);
        }
        for (int i = 0; i < mTransformPoint.transform.childCount; i++)
        {
            _otherPlayerBuildingsTransformList.Add(mTransformPoint.transform.GetChild(i));
        }


        Invoke(nameof(InstantiatePopulatedBuildingPrefabList), 0f);
    }
    
    void InstantiatePopulatedBuildingPrefabList()
    {

        for (int i = 0; i < mOtherPlayerBuildingPrefabPopulateList.Count; i++)
        {
            GameObject enemyBuilding = Instantiate(/*mGameManager._BuildingDetails*/ mOtherPlayerBuildingPrefabPopulateList[i], _otherPlayerBuildingsTransformList[i].position, _otherPlayerBuildingsTransformList[i].rotation);
            enemyBuilding.name = mOtherPlayerBuildingPrefabPopulateList[i].name;
            enemyBuilding.name = enemyBuilding.name.Substring(0, enemyBuilding.name.Length - 1);
            _otherPlayerBuildings.Add(enemyBuilding);
        }
    }
    private void Update()
    {
        _OpenCardNumberIndex = mMultiplayerPlayerData._openCardInfo;
        mPositionNumber = mMultiplayerPlayerData._openCardInfo;

    }
    public void OpenCardAdder()
    {
        if (mPositionNumber >= _OpenCardNumberIndex && mPositionNumber <= 4)
        {
            for (int i = 0; i < OpenHandCardsPositions.Length; i++)
            {
                if (OpenHandCardsPositions[mPositionNumber].GetComponent<OpenCardFilled>().isOpenCardSlotFilled == false && mCardsOpened < 1)
                {
                    _openedCardIndex = Random.Range(0, dummyCards.Count);
                    Debug.Log(_openedCardIndex);
                    Instantiate(dummyCards[_openedCardIndex], OpenHandCardsPositions[mPositionNumber].position, OpenHandCardsPositions[mPositionNumber].rotation, OpenHandCardsPositions[mPositionNumber]);
                    mCardsOpened += 1;
                    break;
                }
            }
            mPositionNumber += 1;
        }
    }
    public void GoBackToGame()
    {
        MultiplayerManager.Instance.CheckDetailsForOpenCard();
        MultiplayerManager.Instance.WriteOpenCardDataToFirebase();
        Invoke("ReadPlayerData", 1f);
    }
    void ReadPlayerData()
    {
        MultiplayerManager.Instance.ReadMyData();
    }
}