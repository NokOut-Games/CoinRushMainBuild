using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType
{
    ATTACK,
    STEAL,
    SHIELD,
    JOKER,
    ENERGY,
    COINS
}

public class Cards : MonoBehaviour
{
    public int _cardID;
    public CardType _cardType;
    public Vector3 _Position;
}
