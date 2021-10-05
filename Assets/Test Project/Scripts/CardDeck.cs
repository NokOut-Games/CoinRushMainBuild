using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
   
    [SerializeField] private GameManager mGameManager;
    private int clicks = 0;
    [SerializeField] public List<ScriptedCards> mCards;
    [SerializeField] private GameObject mCanvasRef;

    public List<HandPoints> _playerHandPoints;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
    /// </summary>
    public void DrawCard()
    {
        // 1.Reduce the Energy.
        mGameManager._energy -= 1;

        // 2.Zoom to the gameplay location
        Camera.main.GetComponent<CameraController>()._DrawButtonClicked = true;

        // 3. Have a way to access the card location and spawn card at their respective positions in an inverted U-Shape
        // 3.1 We can use layout group and Horizontal padding to adjust sprites accordingly but how it affects rotation is still an unknown fact
        // 1st & worst iteration can be putting a empty gameObject around with its own rotation and instatiate according to its rotation.
        // #Problem 1 that can occur is: Finding the nearest neighbour or a point. Becuase few cards need to know if there are any other cards in that location.

        GameObject card = Instantiate(mCards[Random.Range(0,mCards.Count)]._cardModel, _playerHandPoints[clicks].transform.position, Quaternion.identity);
        card.transform.parent = mCanvasRef.transform;
        //Debug.Log(card);
        clicks += 1;
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
