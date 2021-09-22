using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card" , menuName = "ActionCards" , order = 0)]
public class ActionCards : ScriptableObject
{
    public CardType _cardType;
    public Event _event;

    [Space]
    [TextArea(10, 14)] public string _description;
}
