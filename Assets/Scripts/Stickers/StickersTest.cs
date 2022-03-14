using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StickersTest : MonoBehaviour
{
    public List<CardPackCards> _stickerCardTypes;
    public List<CardPackCards> _RewardCards;

    public bool CommonPack = false, RarePack = false, EpicPack = false, LegendaryPack = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickStickers()
    {
        var RandomCard1 = _stickerCardTypes[Random.Range(0, _stickerCardTypes.Count)];
        var RandomCard2 = _stickerCardTypes[Random.Range(0, _stickerCardTypes.Count)];
        var RandomCard3 = _stickerCardTypes[Random.Range(0, _stickerCardTypes.Count)];
        var RandomCard4 = _stickerCardTypes[Random.Range(0, _stickerCardTypes.Count)];
        var RandomCard5 = _stickerCardTypes[Random.Range(0, _stickerCardTypes.Count)];
        var RandomCard6 = _stickerCardTypes[Random.Range(0, _stickerCardTypes.Count)];
        var RandomCard7 = _stickerCardTypes[Random.Range(0, _stickerCardTypes.Count)];
        var RandomCard8 = _stickerCardTypes[Random.Range(0, _stickerCardTypes.Count)];

        if (CommonPack)
        {
            Debug.LogError(" Card1: " + RandomCard1._cardName + " Card2: " + RandomCard2._cardName);
            _RewardCards.Add(RandomCard1);
            _RewardCards.Add(RandomCard2);

        }
        else if (RarePack)
        {
            Debug.LogError(" Card1: " + RandomCard1._cardName + " Card2: " + RandomCard2._cardName + " Card3: " + RandomCard3._cardName + " Card4: " + RandomCard4._cardName);
            _RewardCards.Add(RandomCard1);
            _RewardCards.Add(RandomCard2);
            _RewardCards.Add(RandomCard3);
            _RewardCards.Add(RandomCard4);
        }
        else if (EpicPack)
        {
            _RewardCards.Add(RandomCard1);
            _RewardCards.Add(RandomCard2);
            _RewardCards.Add(RandomCard3);
            _RewardCards.Add(RandomCard4);
            _RewardCards.Add(RandomCard5);
            _RewardCards.Add(RandomCard6);
            Debug.LogError(" Card1: " + RandomCard1._cardName + " Card2: " + RandomCard2._cardName + " Card3: " + RandomCard3._cardName + " Card4: " + RandomCard4._cardName + " Card5: " + RandomCard5._cardName + " Card6: " + RandomCard6._cardName);
        }
        else if (LegendaryPack)
        {
            _RewardCards.Add(RandomCard1);
            _RewardCards.Add(RandomCard2);
            _RewardCards.Add(RandomCard3);
            _RewardCards.Add(RandomCard4);
            _RewardCards.Add(RandomCard5);
            _RewardCards.Add(RandomCard6);
            _RewardCards.Add(RandomCard7);
            _RewardCards.Add(RandomCard8);
            Debug.LogError(" Card1: " + RandomCard1._cardName + " Card2: " + RandomCard2._cardName + " Card3: " + RandomCard3._cardName + " Card4: " + RandomCard4._cardName + " Card5: " + RandomCard5._cardName + " Card6: " + RandomCard6._cardName + " Card7: " + RandomCard7._cardName + " Card8: " + RandomCard8._cardName);
        }
    }

}
