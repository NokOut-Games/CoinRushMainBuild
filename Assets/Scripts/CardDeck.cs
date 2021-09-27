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
        ZoomCameraToPlayArea();
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
                mlevelManagerUI._fiveThousandCoinList.Clear();
                Destroy(card);
            }
            foreach (GameObject card in mlevelManagerUI._twentyFiveThousandCoinList)
            {
                mlevelManagerUI._twentyFiveThousandCoinList.Clear();
                Destroy(card);
            }
            foreach (GameObject card in mlevelManagerUI._hunderThousandCoinList)
            {
                mlevelManagerUI._hunderThousandCoinList.Clear();
                Destroy(card);
            }
            foreach (GameObject card in mlevelManagerUI._fiveHundredThousandCoinList)
            {
                mlevelManagerUI._fiveHundredThousandCoinList.Clear();
                Destroy(card);
            }
            foreach (GameObject card in mlevelManagerUI._OneMillionJackPotCardList)
            {
                mlevelManagerUI._OneMillionJackPotCardList.Clear();
                Destroy(card);
            }

            clicks = 0;
            return;
        }
    }

    private void ZoomCameraToPlayArea()
    {
        mCam.transform.position = mDeckCardCamPosition.position;
        mCam.transform.rotation = mDeckCardCamPosition.transform.rotation;
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
