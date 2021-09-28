using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private GameObject mCam;
    [SerializeField] private Transform mDeckCardCamPosition;
    [SerializeField] private LevelManagerUI mlevelManagerUI;
    [SerializeField] private GameManager mGameManager;
    private int clicks = 0;
    [SerializeField] private Cards mCards;

    public List<HandPoints> _handPoints;

    //public CameraController _cameraController;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
    /// </summary>
    public void DrawCard()
    {
        mGameManager._energy -= 1;
        FindObjectOfType<CameraController>()._DrawButtonClicked = true;
        //ZoomCameraToPlayArea();
        //if(!_handPoints[clicks].isFilled)
        //{
            GameObject card = Instantiate(mCards.boosterCards[Random.Range(0, mCards.boosterCards.Count)]._cardModel, _handPoints[clicks].transform.position, Quaternion.Euler(0,180f,0f));
            //_handPoints[clicks].isFilled = true;
            switch (card.tag)
            {
                case "5K Coins":
                    mlevelManagerUI._fiveThousandCoinList.Add(card);
                    //levelManagerUI.OverAllCards.Add(card);
                    break;
                case "25K Coins":
                    mlevelManagerUI._twentyFiveThousandCoinList.Add(card);
                    //levelManagerUI.OverAllCards.Add(card);
                    break;
                case "100K Coins":
                    mlevelManagerUI._hunderThousandCoinList.Add(card);
                    //levelManagerUI.OverAllCards.Add(card);
                    break;
                case "500K Coins":
                    mlevelManagerUI._fiveHundredThousandCoinList.Add(card);
                    //levelManagerUI.OverAllCards.Add(card);
                    break;
                case "1M Coins":
                    mlevelManagerUI._OneMillionJackPotCardList.Add(card);
                    //levelManagerUI.OverAllCards.Add(card);
                    break;
                case "Attack":
                    mlevelManagerUI._AttackCardList.Add(card);
                    break;
                case "Shield":
                    mlevelManagerUI._SheildCardList.Add(card);
                    break;
                case "Steal":
                    mlevelManagerUI._StealCardList.Add(card);
                    break;
            }
        clicks += 1;
        //}
    }

    private void Update()
    {
        if (clicks == 8)
        {
            //levelManagerUI.OverAllCards.Clear();
            foreach (GameObject card in mlevelManagerUI._fiveThousandCoinList)
            {
                Destroy(card);
                mlevelManagerUI._fiveThousandCoinList.Clear();
            }
            foreach (GameObject card in mlevelManagerUI._twentyFiveThousandCoinList)
            {
                Destroy(card);
                mlevelManagerUI._twentyFiveThousandCoinList.Clear();
            }
            foreach (GameObject card in mlevelManagerUI._hunderThousandCoinList)
            {
                Destroy(card);
                mlevelManagerUI._hunderThousandCoinList.Clear();
            }
            foreach (GameObject card in mlevelManagerUI._fiveHundredThousandCoinList)
            {
                Destroy(card);
                mlevelManagerUI._fiveHundredThousandCoinList.Clear();
            }
            foreach (GameObject card in mlevelManagerUI._OneMillionJackPotCardList)
            {
                Destroy(card);
                mlevelManagerUI._OneMillionJackPotCardList.Clear();
            }

            clicks = 0;
            return;
        }
    }

    private void ZoomCameraToPlayArea()
    {
         
    }

}

//void Residue()
//{
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
//}
