using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerSet : MonoBehaviour
{
    [SerializeField] Sticker[] Stickers;
    public List<StickerDB> StickerCollections;
    [SerializeField] TMP_Text rewardText;
    [SerializeField] TMP_Text titleText;
    public int CurrentSetIndex;
    [SerializeField] Button nextBtn;
    [SerializeField] Button previousBtn;
    [SerializeField] Transform ContentTransform;
    [SerializeField] GameObject stickerSetPrefab;
    List<GameObject> listofSets = new List<GameObject>();
    [SerializeField] GameObject stickerScreen;
    [SerializeField] GameObject stickerSetScreen;

    private void Start()
    {
        nextBtn.onClick.AddListener(OnNextButtonPress);
        previousBtn.onClick.AddListener(OnPreviousButtonPress);
    }

    public void PopulateStickerSets()
    {
        foreach (var setGO in listofSets)
        {
            Destroy(setGO);
        }
        foreach (var stickerset in StickerCollections)
        {
            GameObject Set = Instantiate(stickerSetPrefab, ContentTransform);
            Set.transform.GetChild(0).GetComponent<TMP_Text>().text = stickerset.StickerSetName;
            Button setBtn = Set.GetComponent<Button>();
            setBtn.interactable = false;
            if (stickerset.isUnlocked)
            {
                setBtn.interactable = true;
                setBtn.onClick.AddListener(() =>
                {
                    CurrentSetIndex = stickerset.StickerSetIndex;
                    PopulateTheSetToScreen();
                    stickerSetScreen.SetActive(false);
                });
            }
            listofSets.Add(Set);
        }
    }

    public void PopulateTheSetToScreen()
    {
        stickerScreen.SetActive(true);
        int i = 0;
        foreach (var sticker in Stickers)
        {
            sticker.Stars = StickerCollections[CurrentSetIndex].StickersInset[i].Stars;
            sticker.stickerName = StickerCollections[CurrentSetIndex].StickersInset[i].StickerName;
            sticker.Count = GetCountOfSticker(i);
            i++;
        }
        titleText.text = StickerCollections[CurrentSetIndex].StickerSetName;
        rewardText.text = StickerCollections[CurrentSetIndex].Reward.ToString();
        nextBtn.gameObject.SetActive(true);
        previousBtn.gameObject.SetActive(true);

        if (CurrentSetIndex >= StickerCollections.Count - 1)
        {
            nextBtn.gameObject.SetActive(false);
        }
        if (CurrentSetIndex <= 0)
        { 
            previousBtn.gameObject.SetActive(false);
        }
    }
    public void OnNextButtonPress()
    {
        previousBtn.gameObject.SetActive(true);
        CurrentSetIndex++;
        if (CurrentSetIndex >= StickerCollections.Count - 1) nextBtn.gameObject.SetActive(false);
        PopulateTheSetToScreen();
    }
    public void OnPreviousButtonPress()
    {
        nextBtn.gameObject.SetActive(true);
        CurrentSetIndex--;
        PopulateTheSetToScreen();
        if (CurrentSetIndex <= 0) previousBtn.gameObject.SetActive(false);
    }
    int GetCountOfSticker(int i)
    {
        int Count = 0;
        foreach (var item in GameManager.Instance.UnLockedListAndIndex[CurrentSetIndex].StickerIndex)
        {
            if (i == item) Count++;
        }
        return Count;
    }
}
[System.Serializable]
public class StickerDB
{
    public string StickerSetName;
    public int StickerSetIndex;
    public int Reward;
    public bool isUnlocked;
    public StickerSO[] StickersInset;
}
