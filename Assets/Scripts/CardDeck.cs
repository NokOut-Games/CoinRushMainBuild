using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private Camera mCam;
    [SerializeField] private GameObject mDeck;
    [SerializeField] private Transform mDeckCardCamPosition;

    public List<Transform> _cardSlotPoints;
    int clicks = 0;
    [SerializeField] Cards mCards;


    /// <summary>
    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
    /// </summary>
    public void DrawCard()
    {
        ZoomCameraToPlayArea();
        Instantiate(mCards.boosterCards[Random.Range(0, mCards.boosterCards.Count)]._cardModel, _cardSlotPoints[clicks].position, Quaternion.Euler(180f, -90f, 0f));
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
