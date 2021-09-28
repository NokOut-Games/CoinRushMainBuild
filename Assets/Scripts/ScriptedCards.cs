using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

//void residue()
//{
//    enum cardType
//    {
//        Action, // Action for opening scene and all
//        Booster //booster deals with increasing coins and energy / sheilds
//    };
//}