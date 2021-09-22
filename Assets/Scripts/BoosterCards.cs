using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "BoosterCards", order = 1)]
public class BoosterCards : ScriptableObject
{
    public CardType _cardType;
    public GameObject _cardModel; //OR USE Sprite Later
    public int _amount;
    [Space]
    [TextArea(10,14)] public string _description;

    public CardType Cards() { return _cardType; } 

    public GameObject CardModel() { return _cardModel; }
    public int Amount() { return _amount; }

}
