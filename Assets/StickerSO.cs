using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Stickers/Sticker")]
public class StickerSO : ScriptableObject
{
    public string StickerName;
    public Sprite StickerSprite;
    public int Stars;
    public int SetIndex;
}
