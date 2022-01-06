using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public GameObject buildPanelGameObject;
    public GameObject screenItemsUIPanel;
    public GameObject DrawButtonPanelUI;

    public TextMeshProUGUI _coinText;
    public TextMeshProUGUI _energyText;
    public TextMeshProUGUI _extraEnergytext;

    private float mRegenerationTimer;
    private float mNextRegenTimer;
    private float mMinutes;
    private float mSeconds;

    private GameManager mGameManager;

    [SerializeField] BuildMenuUI mBuildMenuUI;

    void Start()
    {
        mGameManager = FindObjectOfType<GameManager>();
        mRegenerationTimer = mGameManager.MinutesToSecondsConverter(GameManager.Instance._minutes);
        mNextRegenTimer = mGameManager.MinutesToSecondsConverter(GameManager.Instance._minutes);
    }

    void Update()
    {
        //if (buildPanelGameObject.activeInHierarchy == true)
        //{
        //    //To restrict the camera moving when build panel is on
        //    Camera.main.GetComponent<CameraController>()._CameraFreeRoam = false;
        //}

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
        buildPanelGameObject.SetActive(true);
        screenItemsUIPanel.SetActive(false);
        DrawButtonPanelUI.SetActive(false);
        mBuildMenuUI = FindObjectOfType<BuildMenuUI>();
        mBuildMenuUI.SetUpgradeButtons();
    }

    public void ReturnButton()
    {
        buildPanelGameObject.SetActive(false);
        screenItemsUIPanel.SetActive(true);
        DrawButtonPanelUI.SetActive(true);
    }
    private void UpdateCoinAndEnergyTextFields()
    {
        _coinText.text = mGameManager._coins.ToString();
        _energyText.text = mGameManager._energy.ToString();

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
}
