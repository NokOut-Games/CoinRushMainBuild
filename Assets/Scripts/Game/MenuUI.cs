using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject buildPanelGameObject;
    [SerializeField] private GameObject screenItemsUIPanel;
    [SerializeField] private GameObject DrawButtonPanelUI;
    [SerializeField] private GameObject levelCompletedPanel;

    public GameObject OpenCards;

    public TextMeshProUGUI _coinText;
    public TextMeshProUGUI _energyText;
    public TextMeshProUGUI _extraEnergytext;
    public TMP_Text _Shield;
    public GameObject shieldMaxMark;

    private float mRegenerationTimer;
    private float mNextRegenTimer;
    private float mMinutes;
    private float mSeconds;

    private GameManager mGameManager;

    [SerializeField] private BuildMenuUI mBuildMenuUI;

    [SerializeField] CameraController mCameraController;
    public bool BuildModeOn;

    public bool isButtonGenerated = false;
    Animator mCanvasAnimator;
    [SerializeField] Animator Optionanimator;
    [SerializeField] GameObject[] UIElements;
    [SerializeField] Animator shieldToEnergyAnim;



    [Header("Option Screen")]
    [SerializeField]Image profileImage;
    [SerializeField]TMP_Text profileName;
    [SerializeField] RectTransform optionScreen;
    bool isOptionactivate;

    [Header("Friends Screen")]
    [SerializeField] GameObject friendScreen;
    [SerializeField] Transform content;
    [SerializeField] Camera uIcam;
    [SerializeField] private GameObject mcucuLevelPanel;
    bool isInoptionScreen => RectTransformUtility.RectangleContainsScreenPoint(optionScreen, Input.mousePosition, uIcam);



    private void OnEnable()
    {
        FacebookManager.UserProfileName += UpdateName;
        FacebookManager.UserProfilePic += UpdateProfilePic;
    }
    private void Start()
    {
        isButtonGenerated = false;
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mCanvasAnimator = GetComponent<Animator>();
        mCameraController = Camera.main.GetComponent<CameraController>();

        mRegenerationTimer = mGameManager.MinutesToSecondsConverter(GameManager.Instance._minutes);
        mNextRegenTimer = mGameManager.MinutesToSecondsConverter(GameManager.Instance._minutes);

        profileImage.sprite = FirebaseManager.Instance.CurrentPlayerPhotoSprite;
        profileName.text = FirebaseManager.Instance._PlayerName;
    }

     void UpdateName(string name)
     { 
         profileName.text = name; 
     }
    void UpdateProfilePic(Sprite pic)
    {
        profileImage.sprite = pic;
    }

    public void OnStarClicked()
    {
        mcucuLevelPanel.GetComponent<Animator>().SetTrigger("StarButtonClicked?");
        UpdateCUCULevelAndMultiplierText();

    }

    void UpdateCUCULevelAndMultiplierText()
    {
        GameObject PanelReference = mcucuLevelPanel.transform.Find("Panel Background").gameObject;
        PanelReference.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.Instance._cucuLevel.ToString();
        PanelReference.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.cucuMultiplier.ToString();
    }

    public void OnCUCUReturn()
    {
        //mcucuLevelPanel.SetActive(false);
        mcucuLevelPanel.GetComponent<Animator>().SetTrigger("BackButtonClicked?");

    }

    private void Update()
    {
        UpdateCoinAndEnergyTextFields();


        if (Input.touchCount > 0&& !isInoptionScreen && isOptionactivate && optionScreen.gameObject.activeInHierarchy)
        {
            OnOptionButtonPress(false);
        }
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
        if (GameManager.Instance._PauseGame) return;
        mCanvasAnimator.SetBool("GetOut", true);
        buildPanelGameObject.SetActive(true);
        mCameraController.BuildButtonClicked();
        //Camera.main.GetComponent<TestScript>().BuildButtonClicked();
        screenItemsUIPanel.SetActive(false);
        DrawButtonPanelUI.SetActive(false);
        OpenCards.SetActive(false);
        //QuitButton.SetActive(false);

       // mBuildMenuUI = FindObjectOfType<BuildMenuUI>();
        mBuildMenuUI.SetUpgradeButtons();
    }

    public void CloseBuildButton()
    {
        mCanvasAnimator.SetBool("GetOut", false);

        buildPanelGameObject.SetActive(false);
        screenItemsUIPanel.SetActive(true);
        DrawButtonPanelUI.SetActive(true);
    }


    public void OnOptionButtonPress(bool inActivate)
    {
        isOptionactivate = inActivate;
        Optionanimator.SetBool("Show", inActivate);
        GameManager.Instance._PauseGame = inActivate;
    }


    public void OnMapButtonPress()
    {
        LevelLoadManager.instance.GoToMapScreen();
    }
    public void BackToHome()
    {
        LevelLoadManager.instance.BacktoHome();

    }
    public void MapScreen()
    {
        GameManager.Instance.CurrentLevelCompleted();
        GameManager.Instance._PauseGame = false;
    }

    private void UpdateCoinAndEnergyTextFields()
    {
        var currentCoin = mGameManager._coins.ToString("N1", System.Globalization.CultureInfo.InvariantCulture);
        _coinText.text = currentCoin.Substring(0, currentCoin.Length - 2);

        //var energyBarMax = Mathf.Clamp(mGameManager._energy, 0, 50);
        _energyText.text = mGameManager.energyBarMax.ToString("D2");


        if (mGameManager._energy > mGameManager._maxEnergy)
        {
            int newEnergy = mGameManager._energy - mGameManager._maxEnergy;
            _extraEnergytext.text = "+ " + newEnergy + " extra";
        }
        else
        {
            //GameManager.Instance.DisplayTime(mRegenerationTimer);
            _extraEnergytext.text = "+ " + mGameManager._regenerationEnergy + " in " + string.Format("{0:00}:{1:00}", mGameManager.mMinutes, mGameManager.mSeconds) + " mins ";
        }
    }
    public IEnumerator ShowLevelCompletedParticlesCoroutine()
    {
        buildPanelGameObject.GetComponent<Animator>().SetBool("Show", false);
        mCanvasAnimator.SetBool("AllOut", true);
        Camera.main.GetComponent<CameraController>().BuildButtonClicked();
        yield return new WaitForSeconds(1f);
        levelCompletedPanel.SetActive(true);
    }

    public void MakeCanvasScreenIn(bool inActive) => mCanvasAnimator.SetBool("AllOut", !inActive);
    public void UIElementActivate(int Index, bool activate) => UIElements[Index].SetActive(activate);


    public IEnumerator UpDateShieldInUICoroutine(float delay,bool startCall=false)
    {
        yield return new WaitForSeconds(delay);
        bool isMax = GameManager.Instance._shield >= GameManager.Instance._maxShield;

        _Shield.gameObject.SetActive(!isMax);
        shieldMaxMark.SetActive(isMax);

        _Shield.text = "" + GameManager.Instance._shield;
       if(GameManager.Instance._shield > GameManager.Instance._maxShield && !startCall) shieldToEnergyAnim.Play("Anim");
    }

    public void OnViewFriendsButtonClick(bool inActivate)
    {
        friendScreen.SetActive(inActivate);
        if (inActivate) FacebookManager.Instance.GetFriends(content);

    }
    private void OnDisable()
    {
        FacebookManager.UserProfileName -= UpdateName;
        FacebookManager.UserProfilePic -= UpdateProfilePic;
    }
}
