using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType 
{ 
    ATTACK,
    STEAL,
    SHIELD = 1,
    JOKER,
    SMALLENERGY = 10,
    MIDENERGY = 25,
    HIGHENERGY = 100,
    FIVETHOUSANDCOINS = 5000,
    TWENTYFIVETHOUSANDCOINS = 25000,
    HUNDREDTHOUSANDCOINS = 100000,
    FIVEHUNDREDTHOUSANDCOINS = 500000,
    ONEMILLIONCOINS = 1000000
}

public class CardDeck : MonoBehaviour
{
    void Start()
    {
        
    }

    
    void Update()
    {
        DrawCard();
    }
    
    /// <summary>
    /// This function is responsible for Draw a random card out from the enum above & this will be assigned to the draw button of canves
    /// </summary>
    public void DrawCard()
    {
        //Draw a card out and store it to a variable

        

        
    }
}
