using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSet : MonoBehaviour
{
    public GameObject selectedSetPanel;
    public string SetName;
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
    }

    public void BackToStickerSetPanel()
    {
        selectedSetPanel.SetActive(false);
    }
}
