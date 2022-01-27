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
    [SerializeField] private GameObject levelCompletedPanel;

    public GameObject OpenCards;
    public GameObject QuitButton;

    public TextMeshProUGUI _coinText;
    public TextMeshProUGUI _energyText;
    public TextMeshProUGUI _extraEnergytext;
    public TMP_Text _Shield;

    private float mRegenerationTimer;
    private float mNextRegenTimer;
    private float mMinutes;
    private float mSeconds;

    private GameManager mGameManager;

    [SerializeField] private BuildMenuUI mBuildMenuUI;

    private CameraController mCameraController;
    public bool BuildModeOn;

    public bool isButtonGenerated = false;
    Animator mCanvasAnimator;
    [SerializeField] Animator Optionanimator;

    private void Start()
    {
        isButtonGenerated = false;
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mCanvasAnimator = GetComponent<Animator>();
        mCameraController = Camera.main.GetComponent<CameraController>();

        mRegenerationTimer = mGameManager.MinutesToSecondsConverter(GameManager.Instance._minutes);
        mNextRegenTimer = mGameManager.MinutesToSecondsConverter(GameManager.Instance._minutes);
    }

    private void Update()
    {
        UpdateCoinAndEnergyTextFields();
    }

    void DisplayTime(float inTimeToDisplay)
    {
        if (mRegenerationTimer > 0)
        {
            mRegenerationTimer -= Time.deltaTime;
        }
        if (mRegenerationTimer < 0 && inTimeToDisplay < 0)
        {
            inTimeToDisplay = 0;
            mRegenerationTimer = mNextRegenTimer;
        }
        mMinutes = Mathf.FloorToInt(inTimeToDisplay / 60);
        mSeconds = Mathf.FloorToInt(inTimeToDisplay % 60);
    }


    public void BuildButton()
    {
        mCanvasAnimator.SetBool("GetOut", true);
        buildPanelGameObject.SetActive(true);
        buildPanelGameObject.GetComponent<Animator>().SetBool("Show", true);
        Camera.main.GetComponent<CameraController>().BuildButtonClicked();
        //Camera.main.GetComponent<TestScript>().BuildButtonClicked();
        screenItemsUIPanel.SetActive(false);
        DrawButtonPanelUI.SetActive(false);
        OpenCards.SetActive(false);
        QuitButton.SetActive(false);

        if (isButtonGenerated == false)
        {
            mBuildMenuUI = FindObjectOfType<BuildMenuUI>();
            mBuildMenuUI.SetUpgradeButtons();
            isButtonGenerated = true;
        }
    }

    public void CloseBuildButton()
    {
        mCanvasAnimator.SetBool("GetOut", false);
        buildPanelGameObject.GetComponent<Animator>().SetBool("Show", false);


       //buildPanelGameObject.SetActive(false);
        screenItemsUIPanel.SetActive(true);
        DrawButtonPanelUI.SetActive(true);
        OpenCards.SetActive(true);
        QuitButton.SetActive(true);
    }

    public void OnOptionButtonPress(bool inActivate)
    {
        Optionanimator.SetBool("Show", inActivate);
    }


    public void OnMapButtonPress()
    {
        LevelLoadManager.instance.GoToMapScreen();
    }


    private void UpdateCoinAndEnergyTextFields()
    {
        var currentCoin = mGameManager._coins.ToString("N1", System.Globalization.CultureInfo.InvariantCulture);
        _coinText.text = currentCoin.Substring(0, currentCoin.Length - 2);

        var energyBarMax = Mathf.Clamp(mGameManager._energy, 0, 50);
        _energyText.text = energyBarMax.ToString("D2");
        _Shield.text =""+ GameManager.Instance._shield;

        if (mGameManager._energy > mGameManager._maxEnergy)
        {
            int newEnergy = mGameManager._energy - mGameManager._maxEnergy;
            _extraEnergytext.text = "+ " + newEnergy + " extra";
        }
        else
        {
            DisplayTime(mRegenerationTimer);
            _extraEnergytext.text = "+ " + mGameManager._regenerationEnergy + " in " + string.Format("{0:00}:{1:00}", mMinutes, mSeconds) + " mins ";
        }
    }


    public  IEnumerator ShowLevelCompletedParticlesCoroutine()
    {
        buildPanelGameObject.GetComponent<Animator>().SetBool("Show", false);
        mCanvasAnimator.SetBool("AllOut", true);
        Camera.main.GetComponent<CameraController>().BuildButtonClicked();
        yield return new WaitForSeconds(1f);
        levelCompletedPanel.SetActive(true);   
    }


}
