using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardDeck : MonoBehaviour
{

    [SerializeField] private GameManager mGameManager;
    [SerializeField] private GameObject mCardHolderParent;
    private int clicks = 0;
    int k;

    [SerializeField] public List<ScriptedCards> mScriptedCards;
    public List<Cards> _CardList = new List<Cards>();
    public List<Transform> _playerHandPoints;
    public List<Vector3> _PositionList = new List<Vector3>();
    public List<Quaternion> _RotationList = new List<Quaternion>();


    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
    /// 1.Reduce the Energy.
    /// 2.Zoom to the gameplay location
    /// 3.Have a way to access the card location and spawn card at their respective positions in an inverted U-Shape
    /// </summary>
    public void DrawCard()
    {
        if (_CardList.Count >= 8)
            return;

        mGameManager._energy -= 1;

        Camera.main.GetComponent<CameraController>().DrawButtonClicked();

        ScriptedCards cards = mScriptedCards[Random.Range(0, mScriptedCards.Count)];

        GameObject card = Instantiate(cards._cardModel, _playerHandPoints[clicks].localPosition, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
        Cards cardDetails = card.GetComponent<Cards>();

        cardDetails._cardType = cards._cardType;
        cardDetails._cardID = cards._cardID;
        cardDetails._Position = card.transform.position;

        clicks += 1;
        AddNewCard(card.GetComponent<Cards>());
        ReplacementOfCards();
       
    }

    private void Update()
    {
        if (clicks == 8)
        {
          
            clicks = 0;
        }
        if (_CardList.Count != 0)
        {
            CardCheckingFunction();
        }
    }

    /// <summary>
    /// 
    /// </This function is used to allign the picked cards in sorted order>
    public void AddNewCard(Cards inNewCard)
    {
        for (int i = 0; i < _CardList.Count; i++)
        {
            if (_CardList[i]._cardType == inNewCard._cardType)
            {
                _CardList.Insert(i, inNewCard);
                return;
            }
        }

        _CardList.Add(inNewCard);
    }

    public void ReplacementOfCards()
    {
        int medianIndex = _playerHandPoints.Count / 2;

        int incrementValue = 0;
        _PositionList.Clear();
        _RotationList.Clear();

        List<int> drawOrderArrange = new List<int>();

        for (int i = 0; i < _CardList.Count; i++)
        {
            if (i % 2 == 0 || i == 0)
            {
                drawOrderArrange.Add(medianIndex + incrementValue);
                incrementValue++;
            }
            else
            {
                drawOrderArrange.Add(medianIndex - incrementValue);
            }
        }

        drawOrderArrange.Sort();

        for (int i = 0; i < _CardList.Count; i++)
        {
            _PositionList.Add(_playerHandPoints[drawOrderArrange[i]].transform.position);
            _RotationList.Add(_playerHandPoints[drawOrderArrange[i]].transform.rotation);
        }

        for (int i = 0; i < _CardList.Count; i++)
        {
            _CardList[i]._Position = _PositionList[i];
            _CardList[i].transform.position = _PositionList[i];
            _CardList[i].transform.rotation = _RotationList[i];
            _CardList[i].transform.SetSiblingIndex(i + 1);
        }
    }

    /// <summary>
    /// In this function Checking that any of  three cards are same then its name will be displayed
    /// </summary>
    void CardCheckingFunction()
    {
        if (_CardList[0]._cardID == _CardList[1]._cardID && _CardList[0]._cardID == _CardList[2]._cardID)
        {
            k = 1;
            Invoke("CardTrigger", 1.5f);
        }
        if (_CardList[1]._cardID == _CardList[2]._cardID && _CardList[1]._cardID == _CardList[3]._cardID)
        {
            k = 2;
            Invoke("CardTrigger", 1.5f);
        }
        if (_CardList[2]._cardID == _CardList[3]._cardID && _CardList[2]._cardID == _CardList[4]._cardID)
        {
            k = 3;
            Invoke("CardTrigger", 1.5f);
        }
        if (_CardList[3]._cardID == _CardList[4]._cardID && _CardList[3]._cardID == _CardList[5]._cardID)
        {
            k = 4;
            Invoke("CardTrigger", 1.5f);
        }
        if (_CardList[4]._cardID == _CardList[5]._cardID && _CardList[4]._cardID == _CardList[6]._cardID)
        {
            k = 5;
            Invoke("CardTrigger", 1.5f);
        }
        if (_CardList[5]._cardID == _CardList[6]._cardID && _CardList[5]._cardID == _CardList[7]._cardID)
        {
            k = 6;
            Invoke("CardTrigger", 1.5f);
        }
        //if(_CardList >= _CardList.Count)
        //{
        //    return;
        //}
    }
    /// <summary>
    /// This Function will Trigger the Scene if 3 Cards Matches
    /// </summary>
    void CardTrigger()
    {

        switch (_CardList[k]._cardType)
        {
            case CardType.ATTACK:
                SceneManager.LoadScene(1);
                break;
            case CardType.STEAL:
                SceneManager.LoadScene(2);
                break;
            case CardType.COINS:
                SceneManager.LoadScene(3);
                break;
            case CardType.ENERGY:
                SceneManager.LoadScene(4);
                break;
            case CardType.FORTUNEWHEEL:
                SceneManager.LoadScene(5);
                break;
            case CardType.SLOTMACHINE:
                SceneManager.LoadScene(6);
                break;
            case CardType.JOKER:
                Debug.Log("Joker");
                break;
            case CardType.SHIELD:
                Debug.Log("Shield + 1");
                break;
        }


    }


}





























//void Residue()
//{
//[SerializeField] private GameObject mCam;
//[SerializeField] private Transform mDeckCardCamPosition;
//[SerializeField] private LevelManagerUI mlevelManagerUI;

//    [SerializeField] private Camera mCam;
//    [SerializeField] private Transform mDeckCardCamPosition;

//    /// <summary>
//    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
//    /// </summary>
//    public void DrawCard()
//    {
//        ZoomCameraToPlayArea();
//    }

//    private void ZoomCameraToPlayArea()
//    {
//        mCam.transform.position = mDeckCardCamPosition.position;
//        mCam.transform.rotation = mDeckCardCamPosition.transform.rotation;
//    }

//GameObject card = Instantiate(mCards.boosterCards[Random.Range(0, mCards.boosterCards.Count)]._cardModel, _handPoints[clicks].transform.position, Quaternion.Euler(0, 180f, 0f));
////_handPoints[clicks].isFilled = true;
//switch (card.tag)
//{
//    case "5K Coins":
//        mlevelManagerUI._fiveThousandCoinList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "25K Coins":
//        mlevelManagerUI._twentyFiveThousandCoinList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "100K Coins":
//        mlevelManagerUI._hunderThousandCoinList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "500K Coins":
//        mlevelManagerUI._fiveHundredThousandCoinList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "1M Coins":
//        mlevelManagerUI._OneMillionJackPotCardList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "10 EC":
//        mlevelManagerUI._TenEnergyCardList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "25 EC":
//        mlevelManagerUI._TwentyFiveEnergyCardList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "100 EC":
//        mlevelManagerUI._HundredEnergyCardList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "Attack":
//        mlevelManagerUI._AttackCardList.Add(card);
//        break;
//    case "Shield":
//        mlevelManagerUI._SheildCardList.Add(card);
//        break;
//    case "Steal":
//        mlevelManagerUI._StealCardList.Add(card);
//        break;
//}
//clicks += 1;
//}
