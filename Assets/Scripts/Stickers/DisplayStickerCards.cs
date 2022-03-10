using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayStickerCards : MonoBehaviour
{
    StickersTest mStickersTest;
    public List<CardPackCards> Card1;
    public List<CardPackCards> Card2;
    public List<CardPackCards> Card3;
    public List<CardPackCards> Card4;
    public List<CardPackCards> Card5;
    public List<CardPackCards> Card6;
    public List<CardPackCards> Card7;
    public List<CardPackCards> Card8;
    public List<CardPackCards> Card9;
    public List<GameObject> CardSpawnPoint;

    public CardPackCards StickerCard1;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        mStickersTest = GetComponent<StickersTest>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CardPackCards stickers in mStickersTest._RewardCards)
        {
            if (stickers._cardName == Card1[0]._cardName)
            {
        CardSpawnPoint[0].transform.GetChild(1).GetComponent<Text>().text = Card1[0]._cardName;
        CardSpawnPoint[0].transform.GetChild(0).GetComponent<Image>().sprite = Card1[0]._cardPicture;

            }
            else
            {
                i++;
                //continue;
            }
        }

        //for (int i = 0; i < mStickersTest.SET_1_CardSticker.Count; i++)
        //{
        //        Debug.Log(mStickersTest.SET_1_CardSticker[i].name);

        //    //if (Card1.Contains(mStickersTest.SET_1_CardSticker[i]))
        //    //{
        //    //    StickerCard1 = mStickersTest.SET_1_CardSticker[i];
        //    //}
        //    //    continue;
        //    if (mStickersTest.SET_1_CardSticker.Contains(Card1[i]) && !mStickersTest.SET_2_CardSticker.Contains(Card1[i]))
        //    {

        //        continue;
        //    }
        //else
        //{
        //    StickerCard1 = Card1[i];
        //}
        // }
    }
}
