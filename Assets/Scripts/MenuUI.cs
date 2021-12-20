using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject buildPanelGameObject;
    public GameObject screenItemsUIPanel;
    public GameObject DrawButtonPanelUI;

    void Start()
    {
        
    }

    void Update()
    {
        //if (buildPanelGameObject.activeInHierarchy == true)
        //{
        //    //To restrict the camera moving when build panel is on
        //    Camera.main.GetComponent<CameraController>()._CameraFreeRoam = false;
        //}
    }

    public void BuildButton()
    {
        buildPanelGameObject.SetActive(true);
        screenItemsUIPanel.SetActive(false);
        DrawButtonPanelUI.SetActive(false);
    }

    public void ReturnButton()
    {
        buildPanelGameObject.SetActive(false);
        screenItemsUIPanel.SetActive(true);
        DrawButtonPanelUI.SetActive(true);
    }
}
