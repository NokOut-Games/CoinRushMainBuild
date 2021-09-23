using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private Camera mCam;
    [SerializeField] private GameObject mDeck;
    [SerializeField] private Transform mDeckCardCamPosition;

    [SerializeField] LevelManagerUI levelManagerUI;

    public List<Transform> _cardSlotPoints;
    int clicks = 0;
    [SerializeField] Cards mCards;

    private void Start()
    {
           
    }

    /// <summary>
    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
    /// </summary>
    public void DrawCard()
    {
        ZoomCameraToPlayArea();
        GameObject card = Instantiate(mCards.boosterCards[Random.Range(0, mCards.boosterCards.Count)]._cardModel, _cardSlotPoints[clicks].position, Quaternion.Euler(180f, -90f, 0f));
        switch (card.tag)
        {
            case "5K Coins":
                levelManagerUI.FiveK.Add(card);
                break;
            case "25K Coins":
                levelManagerUI.TWENTYFIVEK.Add(card);
                break;
            case "100K Coins":
                levelManagerUI.HUNDREDK.Add(card);
                break;
            case "500K Coins":
                levelManagerUI.FIVEHUNDREDK.Add(card);
                break;
            case "1M Coins":
                levelManagerUI.ONEM.Add(card);
                break;
        }
        clicks += 1;
    }

    private void Update()
    {
        if (clicks > 7)
        {
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
