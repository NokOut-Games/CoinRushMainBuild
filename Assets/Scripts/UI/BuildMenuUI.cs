using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Button Order:
/// 0. Card Background
/// 1. Buy Button
/// 2. Building Name
/// 3. Building Image
/// 4. Stars
/// </summary>

enum ButtonState
{
    Building,
    ButtonAssigning
}

public class BuildMenuUI : MonoBehaviour
{

    [SerializeField] private Transform mContentView;
    [SerializeField] private BuildingManager mBuildingManagerRef;

    [SerializeField] private GameObject mBuildButtonTemplate;
    [SerializeField] Popup _PopUp;

    private GameManager mGameManager;


    public Sprite[] _BuyButtonStates;
    public Sprite[] _UpgradeStarStates;
    public Sprite[] _FirstStarState;

    [SerializeField] private float mWaitTimeToShowNextUpgradeCost;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

 

    public void SetUpgradeButtons()
    {
        for (int j = 0; j < mContentView.transform.childCount; j++)
        {
            Destroy(mContentView.GetChild(j).gameObject);
        }
        for (int i = 0; i < mBuildingManagerRef._buildingData.Count; i++)
        {
            GameObject buildingsButtonRef = Instantiate(mBuildButtonTemplate, mContentView);
            int BuildingElementNumber = i;

            buildingsButtonRef.name = mBuildingManagerRef._buildingData[BuildingElementNumber]._buildingDisplayName + " Button";
            mBuildingManagerRef._buildingData[BuildingElementNumber]._respectiveBuildingButtons = buildingsButtonRef;

            StartCoroutine(ButtonTextManagament(buildingsButtonRef, BuildingElementNumber,0,ButtonState.ButtonAssigning));

            AssignBuildingNameAndButtonFunction(buildingsButtonRef, BuildingElementNumber);

            UpdateBuildingImage(buildingsButtonRef, BuildingElementNumber);
            UpdateStarsBasedOnBuildingState(buildingsButtonRef, BuildingElementNumber);

        }
    }

    private void AssignBuildingNameAndButtonFunction(GameObject buildingsButtonRef, int inBuildingElementNumber)
    {
        //Assign the Building Name
        buildingsButtonRef.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingDisplayName;

        //Assigning the button to the buildings
        buildingsButtonRef.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            //Check if player has enough coins and then do the upgrade building function
            if (!mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged) //If building is not damaged do this
            {
                if (GameManager.Instance.HasEnoughCoins(mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel]))
                {
                    GameManager.Instance.AddStars();
                    mBuildingManagerRef.GrabElementNumberBasedOnButtonClick(inBuildingElementNumber);
                    mGameManager._coins -= mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1];
                    UpdateBuildingImage(buildingsButtonRef, inBuildingElementNumber);
                    UpdateStarsBasedOnBuildingState(buildingsButtonRef, inBuildingElementNumber);
                    StartCoroutine(ButtonTextManagament(buildingsButtonRef, inBuildingElementNumber, mWaitTimeToShowNextUpgradeCost, ButtonState.Building));

                }
                else
                {
                    Debug.Log("Not Enough Coins");
                    int neededCoin = mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel]- GameManager.Instance._coins;
                    _PopUp.AwakePopUp(Popup.PopUp.Coin, neededCoin);
                }
            }
            else //If building is damaged do this
            {
                if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel != 5)
                {
                    if (GameManager.Instance.HasEnoughCoins(mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel]))
                    {
                        mBuildingManagerRef.GrabElementNumberBasedOnButtonClick(inBuildingElementNumber);
                        mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged = false;
                        buildingsButtonRef.transform.GetChild(1).GetComponent<Image>().sprite = _BuyButtonStates[0];
                        mGameManager._coins -= mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel] / 2;
                        UpdateBuildingImage(buildingsButtonRef, inBuildingElementNumber);
                        UpdateStarsBasedOnBuildingState(buildingsButtonRef, inBuildingElementNumber);
                        StartCoroutine(ButtonTextManagament(buildingsButtonRef, inBuildingElementNumber, mWaitTimeToShowNextUpgradeCost, ButtonState.Building));
                    }
                    else
                    {
                        Debug.Log("Not Enough Coins");
                        int neededCoin = mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel] / 2 - GameManager.Instance._coins;
                        _PopUp.AwakePopUp(Popup.PopUp.Coin, neededCoin);
                    }
                }
                else
                {
                    if (GameManager.Instance.HasEnoughCoins(mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1]))
                    {
                        mBuildingManagerRef.GrabElementNumberBasedOnButtonClick(inBuildingElementNumber);
                        mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged = false;
                        buildingsButtonRef.transform.GetChild(1).GetComponent<Image>().sprite = _BuyButtonStates[0];
                        mGameManager._coins -= mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1] / 2;
                        UpdateBuildingImage(buildingsButtonRef, inBuildingElementNumber);
                        UpdateStarsBasedOnBuildingState(buildingsButtonRef, inBuildingElementNumber);
                        StartCoroutine(ButtonTextManagament(buildingsButtonRef, inBuildingElementNumber, mWaitTimeToShowNextUpgradeCost, ButtonState.Building));
                    }
                    else
                    {
                        Debug.Log("Not Enough Coins");
                        int neededCoin = mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1] / 2 - GameManager.Instance._coins;
                        _PopUp.AwakePopUp(Popup.PopUp.Coin, neededCoin);
                    }
                }
            }
        });
    }

    

    private void UpdateBuildingImage(GameObject inButton, int inBuildingElementNumber)
    {
        if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingMaxLevel)
        {
            inButton.transform.GetChild(3).GetComponent<Image>().sprite = mBuildingManagerRef._buildingData[inBuildingElementNumber].NextUpgradeImages[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel];
        }
        else
        {
            inButton.transform.GetChild(3).GetComponent<Image>().sprite = mBuildingManagerRef._buildingData[inBuildingElementNumber].NextUpgradeImages[mBuildingManagerRef._buildingData[inBuildingElementNumber].NextUpgradeImages.Length - 1];
        }
    }

    private void UpdateStarsBasedOnBuildingState(GameObject inButton, int inBuildingElementNumber)
    {
        if(mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel <= 0)
        {
            if(mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
            {
                inButton.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = _FirstStarState[1];
            }
            else
            {
                inButton.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = _FirstStarState[0];
            }
        }
        else
        {
            if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingMaxLevel) //4 < 5
            {
                for (int i = 0; i < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel; i++) // 0 < 4
                {
                    inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true); // Star[0] = true
                    inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = _UpgradeStarStates[0]; //Star[0] = perfect_Star
                }
                if (mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
                {
                    inButton.transform.GetChild(4).GetChild(mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1).GetComponent<Image>().sprite = _UpgradeStarStates[1];
                }
            }
            else
            {
                for (int i = 0; i < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel; i++) // 0 < 4
                {
                    inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true); // Star[0] = true
                    inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = _UpgradeStarStates[0]; //Star[0] = perfect_Star
                }
                if (mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
                {
                    inButton.transform.GetChild(4).GetChild(mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1).GetComponent<Image>().sprite = _UpgradeStarStates[1];
                }
            }

        }
    }

    private IEnumerator ButtonTextManagament(GameObject inButton, int inBuildingElementNumber, float inWaitTime ,ButtonState inState)
    {
        yield return new WaitForSeconds(inWaitTime);
        if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingMaxLevel)
        {
            inButton.transform.GetChild(1).GetComponent<Button>().interactable = true;
            inButton.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (inState == ButtonState.ButtonAssigning)
            {
                if (!mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
                {
                    inButton.transform.GetChild(1).GetComponent<Image>().sprite = _BuyButtonStates[0];
                    inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "   " + ConvertToText(mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel]);//.ToString();
                }
                else
                {
                    inButton.transform.GetChild(1).GetComponent<Image>().sprite = _BuyButtonStates[1];
                    inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "   " + ConvertToText((mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel] / 2));//.ToString();
                }
            }
            else
            {
                inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "   " + ConvertToText(mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel]);//.ToString();
            }
        }
        else
        {
            if (!mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
            {
                inButton.transform.GetChild(1).gameObject.SetActive(false);
                inButton.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                inButton.transform.GetChild(1).gameObject.SetActive(true);
                inButton.transform.GetChild(5).gameObject.SetActive(false);
                inButton.transform.GetChild(1).GetComponent<Image>().sprite = _BuyButtonStates[1];
                inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "   " + ConvertToText((mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1] / 2));             
            }
        }
    }
    string ConvertToText(int score)
    {
        float scoreF = (float)score;
        if (scoreF >= 1000000000) return (scoreF / 1000000000).ToString("F1") + "B";
        else if (scoreF >= 1000000) return (scoreF / 1000000).ToString("F1") + "M";
        else if (scoreF >= 1000) return (scoreF / 1000).ToString("F1") + "K";
        else return scoreF.ToString();
    }
}