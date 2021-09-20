using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "BoosterCards", order = 1)]
public class BoosterCards : ScriptableObject
{
    public CardType _cardType;
    public int _amount;
    [TextArea(10,14)] public string _description;

    public CardType Cards() { return _cardType; } 
    public int Amount() { return _amount; }

}
