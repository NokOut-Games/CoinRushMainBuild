using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SET
{
    public string _setName;
    public List<CardPackCards> _cards;
    public int _unlockLevel;
}


public class CardPackBookManager : MonoBehaviour
{
    public List<SET> _cardPackSets;

   public StickersTest mStickersTest;

    
    void Start()
    {
        for(int i=0; i< mStickersTest._RewardCards.Count; i++)
        {
            if (_cardPackSets[0]._cards.Contains(mStickersTest._RewardCards[i]))
            {
              Debug.LogError("Set 1 Cards : " + mStickersTest._RewardCards[i]);
            }
            if (_cardPackSets[1]._cards.Contains(mStickersTest._RewardCards[i]))
            {
                Debug.LogError("Set 2 Cards : " + mStickersTest._RewardCards[i]);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnStickerSetClicked()
    {

    }
}
