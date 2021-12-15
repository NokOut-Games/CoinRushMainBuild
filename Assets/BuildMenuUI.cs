using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMenuUI : MonoBehaviour
{
    public GameObject buildPanelGameObject;
    public GameObject screenItemsUIPanel;
    public GameObject drawButtonUI;

    void Start()
    {
        
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }

    public void BuildButton()
    {
        buildPanelGameObject.SetActive(true);
        screenItemsUIPanel.SetActive(false);
        drawButtonUI.SetActive(false);
    }

    public void ReturnButton()
    {
        buildPanelGameObject.SetActive(false);
        screenItemsUIPanel.SetActive(true);
        drawButtonUI.SetActive(true);
    }
}
