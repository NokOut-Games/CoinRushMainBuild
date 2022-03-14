using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSet : MonoBehaviour
{
    public GameObject selectedSetPanel;
    public string SetName;
    public string ButtonName;
    DisplayStickerCards displayStickerCards;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked()
    {
        selectedSetPanel = GameObject.Find("CanvasComponents/GameCanvas/StickersPanel/SelectedPanel");
        selectedSetPanel.SetActive(true);
        displayStickerCards = GetComponent<DisplayStickerCards>();
        if (ButtonName == SetName)
        {
            displayStickerCards.makeSet1Active = true;
            displayStickerCards.makeSet2Active = false;
        }
        if(ButtonName != SetName)
        {
            displayStickerCards.makeSet1Active = false;
            displayStickerCards.makeSet2Active = true;
        }
    }

    public void BackToStickerSetPanel()
    {
        selectedSetPanel.SetActive(false);
    }
}
