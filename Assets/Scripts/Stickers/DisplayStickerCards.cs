using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayStickerCards : MonoBehaviour
{
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
    CardPackBookManager mCardBookManager;
    
    public bool makeSet1Active=false, makeSet2Active = false;
    void Start()
    {
        mCardBookManager = FindObjectOfType<CardPackBookManager>().GetComponent<CardPackBookManager>();

        makeSet1Active = true;
    }

    public void BackToGameScene()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (makeSet1Active)
        { 
        for (int i = 0; i < mCardBookManager.Set1Rewards.Count; i++)
        {
            if (Card1.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[0].transform.GetChild(1).GetComponent<Text>().text = Card1[0]._cardName;
                CardSpawnPoint[0].transform.GetChild(0).GetComponent<Image>().sprite = Card1[0]._cardPicture;
            }

            if (Card2.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[1].transform.GetChild(1).GetComponent<Text>().text = Card2[0]._cardName;
                CardSpawnPoint[1].transform.GetChild(0).GetComponent<Image>().sprite = Card2[0]._cardPicture;
            }

            if (Card3.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[2].transform.GetChild(1).GetComponent<Text>().text = Card3[0]._cardName;
                CardSpawnPoint[2].transform.GetChild(0).GetComponent<Image>().sprite = Card3[0]._cardPicture;
            }

            if (Card4.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[3].transform.GetChild(1).GetComponent<Text>().text = Card4[0]._cardName;
                CardSpawnPoint[3].transform.GetChild(0).GetComponent<Image>().sprite = Card4[0]._cardPicture;
            }

            if (Card5.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[4].transform.GetChild(1).GetComponent<Text>().text = Card5[0]._cardName;
                CardSpawnPoint[4].transform.GetChild(0).GetComponent<Image>().sprite = Card5[0]._cardPicture;
            }

            if (Card6.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[5].transform.GetChild(1).GetComponent<Text>().text = Card6[0]._cardName;
                CardSpawnPoint[5].transform.GetChild(0).GetComponent<Image>().sprite = Card6[0]._cardPicture;
            }

            if (Card7.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[6].transform.GetChild(1).GetComponent<Text>().text = Card7[0]._cardName;
                CardSpawnPoint[6].transform.GetChild(0).GetComponent<Image>().sprite = Card7[0]._cardPicture;
            }

            if (Card8.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[7].transform.GetChild(1).GetComponent<Text>().text = Card8[0]._cardName;
                CardSpawnPoint[7].transform.GetChild(0).GetComponent<Image>().sprite = Card8[0]._cardPicture;
            }

            if (Card9.Contains(mCardBookManager.Set1Rewards[i]))
            {
                CardSpawnPoint[8].transform.GetChild(1).GetComponent<Text>().text = Card9[0]._cardName;
                CardSpawnPoint[8].transform.GetChild(0).GetComponent<Image>().sprite = Card9[0]._cardPicture;
            }
        }
    }

        if (makeSet2Active)
        {
            for (int i = 0; i < mCardBookManager.Set2Rewards.Count; i++)
            {
                if (Card1.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[0].transform.GetChild(1).GetComponent<Text>().text = Card1[1]._cardName;
                    CardSpawnPoint[0].transform.GetChild(0).GetComponent<Image>().sprite = Card1[1]._cardPicture;
                }

                if (Card2.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[1].transform.GetChild(1).GetComponent<Text>().text = Card2[1]._cardName;
                    CardSpawnPoint[1].transform.GetChild(0).GetComponent<Image>().sprite = Card2[1]._cardPicture;
                }

                if (Card3.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[2].transform.GetChild(1).GetComponent<Text>().text = Card3[1]._cardName;
                    CardSpawnPoint[2].transform.GetChild(0).GetComponent<Image>().sprite = Card3[1]._cardPicture;
                }

                if (Card4.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[3].transform.GetChild(1).GetComponent<Text>().text = Card4[1]._cardName;
                    CardSpawnPoint[3].transform.GetChild(0).GetComponent<Image>().sprite = Card4[1]._cardPicture;
                }

                if (Card5.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[4].transform.GetChild(1).GetComponent<Text>().text = Card5[1]._cardName;
                    CardSpawnPoint[4].transform.GetChild(0).GetComponent<Image>().sprite = Card5[1]._cardPicture;
                }

                if (Card6.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[5].transform.GetChild(1).GetComponent<Text>().text = Card6[1]._cardName;
                    CardSpawnPoint[5].transform.GetChild(0).GetComponent<Image>().sprite = Card6[1]._cardPicture;
                }

                if (Card7.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[6].transform.GetChild(1).GetComponent<Text>().text = Card7[1]._cardName;
                    CardSpawnPoint[6].transform.GetChild(0).GetComponent<Image>().sprite = Card7[1]._cardPicture;
                }

                if (Card8.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[7].transform.GetChild(1).GetComponent<Text>().text = Card8[1]._cardName;
                    CardSpawnPoint[7].transform.GetChild(0).GetComponent<Image>().sprite = Card8[1]._cardPicture;
                }

                if (Card9.Contains(mCardBookManager.Set2Rewards[i]))
                {
                    CardSpawnPoint[8].transform.GetChild(1).GetComponent<Text>().text = Card9[1]._cardName;
                    CardSpawnPoint[8].transform.GetChild(0).GetComponent<Image>().sprite = Card9[1]._cardPicture;
                }
            }
        }

    }
}
