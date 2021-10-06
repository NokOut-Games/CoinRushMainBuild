using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
   
    [SerializeField] private GameManager mGameManager;
    private int clicks = 0;
    [SerializeField] private GameObject mCanvasRef;
    private int[] customSpawnOrder = new int[] { 3, 4, 2, 5, 1, 6, 0, 7 };

    [SerializeField] public List<ScriptedCards> mCards;
    public List<HandPoints> _playerHandPoints;
    //public GameObject _playerHandPoints;

    //try theta
    public float _minDegree = 0;
    public float _maxDegree = 180;
    public int maxNumberOfCards;
    public float spacingBetweenCards;
    public float _segments;

    public int numberOfCardsActive = 0;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spacingBetweenCards = _minDegree + _maxDegree / maxNumberOfCards;
        _segments = _minDegree + _maxDegree / spacingBetweenCards;
    }

    /// <summary>
    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
    /// 1.Reduce the Energy.
    /// 2.Zoom to the gameplay location
    /// 3.Have a way to access the card location and spawn card at their respective positions in an inverted U-Shape
    /// </summary>
    public void DrawCard()
    {
        mGameManager._energy -= 1;
        numberOfCardsActive += 1;
        Camera.main.GetComponent<CameraController>()._DrawButtonClicked = true;
        float rotationForCards = _minDegree + numberOfCardsActive * (_maxDegree / maxNumberOfCards);
        GameObject card = Instantiate(mCards[Random.Range(0,mCards.Count)]._cardModel,_playerHandPoints[clicks].transform.position + new Vector3(_segments,0,0),  Quaternion.Euler(0,0,-rotationForCards));
        card.transform.SetParent(mCanvasRef.transform);
        clicks += 1;
        spacingBetweenCards += 15f;
        //_segments += 22.5f;
    }

    private void Update()
    {
        if (clicks == 8)
        {
            clicks = 0;
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
