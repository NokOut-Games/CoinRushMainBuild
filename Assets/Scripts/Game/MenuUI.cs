using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject buildPanelGameObject;
    [SerializeField] private GameObject screenItemsUIPanel;
    [SerializeField] private GameObject DrawButtonPanelUI;

    public TextMeshProUGUI _coinText;
    public TextMeshProUGUI _energyText;

    private GameManager mGameManager;

    [SerializeField] private BuildMenuUI mBuildMenuUI;

    private CameraController mCameraController;
    public bool BuildModeOn;

    public bool isButtonGenerated = false;

    private void Start()
    {
        isButtonGenerated = false;
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mCameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        //if (buildPanelGameObject.activeInHierarchy == true)
        //{
        //    //To restrict the camera moving when build panel is on
        //    Camera.main.GetComponent<CameraController>()._CameraFreeRoam = false;
        //}
        UpdateCoinAndEnergyTextFields();
    }

    public void BuildButton()
    {
        buildPanelGameObject.SetActive(true);
        Camera.main.GetComponent<CameraController>().BuildButtonClicked();
        screenItemsUIPanel.SetActive(false);
        DrawButtonPanelUI.SetActive(false);

        if (isButtonGenerated == false)
        {
            mBuildMenuUI = FindObjectOfType<BuildMenuUI>();
            mBuildMenuUI.SetUpgradeButtons();
            isButtonGenerated = true;
        }
    }

    public void CloseBuildButton()
    {
        buildPanelGameObject.SetActive(false);
        screenItemsUIPanel.SetActive(true);
        DrawButtonPanelUI.SetActive(true);
    }

    private void UpdateCoinAndEnergyTextFields()
    {
        var currentCoin = mGameManager._coins.ToString("N1", System.Globalization.CultureInfo.InvariantCulture);
        _coinText.text = currentCoin.Substring(0, currentCoin.Length - 2);

        _energyText.text = mGameManager._energy.ToString();
    }
}
