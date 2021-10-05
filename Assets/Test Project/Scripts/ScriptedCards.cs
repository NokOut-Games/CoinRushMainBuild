using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptedCards", order = 1)]
public class ScriptedCards : ScriptableObject
{
    public CardType _cardType;
    public GameObject _cardModel; //OR USE Sprite Later
    public int _amount;
    //public event;
    [Space]
    [TextArea(10,14)] public string _description;
}
