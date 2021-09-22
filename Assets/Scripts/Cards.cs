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
    public List<BoosterCards> boosterCards;
    public List<ActionCards> actionCards;

    [SerializeField] private Transform mDeckSpawnPosition;
    [SerializeField] private GameManager mGameManager;
    [SerializeField] private int mEnergy;
    private int mCoins;
    void Start()
    {
        mEnergy += mGameManager._energy;
    }

    private void Update()
    {
        float j = 0;
        //Debug.Log(boosterCards[0]._amount);
        for (int i = 0; i < mEnergy; i++, j += 0.3f)
        {
            
        }
    }
}
