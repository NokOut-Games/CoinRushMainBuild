using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType
{
    ATTACK,
    STEAL,
    SHIELD,
    JOKER,
    TENENERGY,
    TWENTYFIVEENERGY,
    HUNDREDENERGY,
    FIVETHOUSANDCOINS,
    TWENTYFIVETHOUSANDCOINS,
    HUNDREDTHOUSANDCOINS,
    FIVEHUNDREDTHOUSANDCOINS,
    ONEMILLIONCOINS
}


public class CardDeck : MonoBehaviour
{
    private Camera mCam;
    [SerializeField] private Transform mDeckCardCamPosition;
    //private CardType cardType;

    void Start()
    {
        mCam = Camera.main;
        
    }

    /// <summary>
    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
    /// </summary>
    public void DrawCard()
    {
        //Zoom in the camera so the deck spot is visible 
        ZoomCameraToPlayArea();

        //Draw a card out

        //Check what type of card is it
        //switch (cardType)
        //{
        //    case CardType.ATTACK:
        //        //Open a Scene to Attack
        //        break;
        //    case CardType.STEAL:
        //        //Opens a Scene to Steal
        //        break;
        //    case CardType.SHIELD:
        //        //Makes shield +1
        //        break;
        //    case CardType.JOKER:
        //        //Joker Thing
        //        break;
        //    case CardType.TENENERGY:
        //        //grants _10 Energy Cards 
        //        break;
        //    case CardType.TWENTYFIVEENERGY:
        //        //grants _25 Energy Cards 
        //        break;
        //    case CardType.HUNDREDENERGY:
        //        //grants _100 Energy Cards 
        //        break;
        //    case CardType.FIVETHOUSANDCOINS:
        //        //grants _500 coins
        //        break;
        //    case CardType.TWENTYFIVETHOUSANDCOINS:
        //        //grants _25000 coins
        //        break;
        //    case CardType.HUNDREDTHOUSANDCOINS:
        //        //grants _100,000 Energy Cards 
        //        break;
        //    case CardType.FIVEHUNDREDTHOUSANDCOINS:
        //        //grants _500,000 Energy Cards 
        //        break;
        //    case CardType.ONEMILLIONCOINS:
        //        //grants _10,00,000 Energy Cards 
        //        break;
        //    default:
        //        Debug.Log("DoNothing");
        //        break;
        //}



    }

    private void ZoomCameraToPlayArea()
    {
        mCam.transform.position = mDeckCardCamPosition.position;
        mCam.transform.rotation = mDeckCardCamPosition.transform.rotation;
    }


}
