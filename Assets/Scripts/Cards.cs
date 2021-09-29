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

public class Cards : MonoBehaviour
{
    public List<ScriptedCards> boosterCards;
    void Start()
    {
        //mEnergy += mGameManager._energy;
    }

    private void Update()
    {
    }
}
