using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StickerSet
{
    BARN_DIARIES_1,
    BARN_DIARIES_2
}

[CreateAssetMenu(menuName = "Card_Pack_Card", order = 1)]
public class CardPackCards : ScriptableObject
{
    public string _cardName;
    public int _starValue;
    public int _unlockLevel;
    public string _setName;
    public StickerSet set;
    public Sprite _cardPicture;
}
