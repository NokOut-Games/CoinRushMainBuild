using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
   
    [SerializeField] private GameManager mGameManager;
    [SerializeField] private GameObject mCanvasRef;
    private int clicks = 0;

    public List<int> _CardList = new List<int>();
    [SerializeField] public List<ScriptedCards> mScriptedCards; 
    public List<HandPoints> _playerHandPoints;
    
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
        mGameManager._energy -= 1;
        
        Camera.main.GetComponent<CameraController>()._DrawButtonClicked = true;
        
        ScriptedCards cards = mScriptedCards[Random.Range(0, mScriptedCards.Count)]; 

        GameObject card = Instantiate(cards._cardModel,_playerHandPoints[clicks].transform.position, _playerHandPoints[clicks].transform.rotation);
        card.GetComponent<Cards>()._cardType = cards._cardType;
        card.GetComponent<Cards>()._cardID = cards._cardID;

        card.transform.SetParent(mCanvasRef.transform);

        clicks += 1;
        AddNewCard(card.GetComponent<Cards>()._cardID);
    }

    private void Update()
    {
        if (clicks == 8)
        {
            clicks = 0;
        }
    }

    /// <summary>
    /// 
    /// </This function is used to allign the picked cards in sorted order>
    public void AddNewCard(int inNewCard)
    {
        _CardList.Sort();

        for (int i = 0; i < _CardList.Count; i++)
        {
            if (_CardList[i] == inNewCard)
            {
                _CardList.Insert(i, inNewCard);
                return;
            }
        }

        _CardList.Add(inNewCard);
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
