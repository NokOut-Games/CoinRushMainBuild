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
    //public GameObject buildPanelGameObject;
    //public GameObject screenItemsUIPanel;
    //public GameObject drawButtonUI;

    [SerializeField] private Transform mContentView;
    [SerializeField] private BuildingManager mBuildingManagerRef;

    [SerializeField] private GameObject mBuildButtonTemplate;

    private GameManager mGameManager;

    // IMPORTANT: There are two images attached to the variables below where the one attached in 0 is the perfect_state and 1 is the broken_state
    public Sprite[] _BuyButtonStates;
    public Sprite[] _UpgradeStarStates;
    public Sprite[] _FirstStarState;

    [SerializeField] private float mWaitTimeToShowNextUpgradeCost;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //public List<GameObject> ButtonTemplatesHolder = new List<GameObject>();

    public void SetUpgradeButtons()
    {
        //BuildingItemTemplate = ContentView.GetChild(0).gameObject;
        for (int j = 0; j < mContentView.transform.childCount; j++)
        {
            Destroy(mContentView.GetChild(j).gameObject);
        }
        for (int i = 0; i < mBuildingManagerRef._buildingData.Count; i++)
        {
            //Instantiate a button and pass it to its respective building.
            GameObject buildingsButtonRef = Instantiate(mBuildButtonTemplate, mContentView);
            int BuildingElementNumber = i;

            buildingsButtonRef.name = mBuildingManagerRef._buildingData[BuildingElementNumber]._buildingDisplayName + " Button";
            mBuildingManagerRef._buildingData[BuildingElementNumber]._respectiveBuildingButtons = buildingsButtonRef;

            //ManageButtonStates(buildingsButtonRef, BuildingElementNumber);
            StartCoroutine(ButtonTextManagament(buildingsButtonRef, BuildingElementNumber,0,ButtonState.ButtonAssigning));

            AssignBuildingNameAndButtonFunction(buildingsButtonRef, BuildingElementNumber);

            //Update the UI Button building Image
            UpdateBuildingImage(buildingsButtonRef, BuildingElementNumber);
            UpdateStarsBasedOnBuildingState(buildingsButtonRef, BuildingElementNumber);

        }
    }

    /// <summary>
    /// Manages the button states based on is building damaged or no
    /// </summary>
    /// <param name="inbuildingsButtonRef"></param>
    /// <param name="inBuildingElementNumber"></param>
    private void ManageButtonStates(GameObject inbuildingsButtonRef, int inBuildingElementNumber)
    {
        //Assigning the building upgrade cost.
        if (!mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged) //If building is not damaged then have the button state this way
        {
            //if(buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel <= 0)
            //{

            //}
            if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingMaxLevel)
            {
                inbuildingsButtonRef.transform.GetChild(1).GetComponent<Image>().sprite = _BuyButtonStates[0];
                inbuildingsButtonRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel].ToString();
            }
        }
        else //If the building is damaged have the button state this way.
        {
            inbuildingsButtonRef.transform.GetChild(1).GetComponent<Image>().sprite = _BuyButtonStates[1];
            inbuildingsButtonRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = (mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel] / 2).ToString();
        }
    }

    /// <summary>
    /// Assigns the button with building Name & the buildingUpgrade Function
    /// </summary>
    /// <param name="buildingsButtonRef"></param>
    /// <param name="inBuildingElementNumber"></param>
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
                    mBuildingManagerRef.GrabElementNumberBasedOnButtonClick(inBuildingElementNumber);
                    mGameManager._coins -= mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1];
                    UpdateBuildingImage(buildingsButtonRef, inBuildingElementNumber);
                    UpdateStarsBasedOnBuildingState(buildingsButtonRef, inBuildingElementNumber);
                    StartCoroutine(ButtonTextManagament(buildingsButtonRef, inBuildingElementNumber,mWaitTimeToShowNextUpgradeCost,ButtonState.Building));
                }
                else
                {
                    Debug.Log("Not Enough Coins");
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

                        //buildingsButtonRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel].ToString();
                    }
                    else
                    {
                        Debug.Log("Not Enough Coins");
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

                        //buildingsButtonRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel].ToString();
                    }
                    else
                    {
                        Debug.Log("Not Enough Coins");
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


    /// <summary>
    /// Manages the text component in button making it show the next upgradecost or if its maxed and all things related
    /// </summary>
    /// <param name="inButton"></param>
    /// <param name="inBuildingElementNumber"></param>
    /// <returns></returns>
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
                    inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel].ToString();
                }
                else
                {
                    inButton.transform.GetChild(1).GetComponent<Image>().sprite = _BuyButtonStates[1];
                    inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = (mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel] / 2).ToString();
                }
            }
            else
            {
                inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel].ToString();
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
                inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = (mBuildingManagerRef._buildingData[inBuildingElementNumber].UpgradeCosts[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1] / 2).ToString();

                //AssignBuildingNameAndButtonFunction(inButton,inBuildingElementNumber);
            }

            //inButton.transform.GetChild(1).GetComponent<Button>().interactable = false;
            //inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "MAXED";
        }
    }
}


////if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel > 0)
//{
//    if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingMaxLevel) //4 < 5
//    {
//        for (int i = 0; i < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel; i++) // 0 < 4
//        {
//            inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true); // Star[0] = true
//            inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = _UpgradeStarStates[0]; //Star[0] = perfect_Star
//        }
//        if (mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged) //true
//        {
//            inButton.transform.GetChild(4).GetChild(mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1).GetComponent<Image>().sprite = _UpgradeStarStates[1];
//        }
//        //if (!mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
//        //{
//        //    StartCoroutine(InvokeNextCostForButton(inButton, inBuildingElementNumber));
//        //}
//        inButton.transform.GetChild(3).GetComponent<Image>().sprite = mBuildingManagerRef._buildingData[inBuildingElementNumber].NextUpgradeImages[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel];
//    }
//    else
//    {
//        inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Maxed";
//        for (int i = 0; i < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel; i++)
//        {
//            inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true);
//            inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = _UpgradeStarStates[0];
//        }
//        if (mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged) //true
//        {
//            inButton.transform.GetChild(4).GetChild(mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1).GetComponent<Image>().sprite = _UpgradeStarStates[1];
//            inButton.transform.GetChild(1).gameObject.SetActive(true);
//            inButton.transform.GetChild(5).gameObject.SetActive(false);
//        }
//        else
//        {
//            inButton.transform.GetChild(1).gameObject.SetActive(false);
//            inButton.transform.GetChild(5).gameObject.SetActive(true);
//        }

//    }
//}
//else if(mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel == 0 && mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
//{
//    inButton.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = _FirstStarState[1]; //Broken Silohette
//}
//else
//{
//    inButton.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = _FirstStarState[0]; //Un_Broken Silohette
//}

//inButton.transform.GetChild(3).GetComponent<Image>().sprite = mBuildingManagerRef._buildingData[inBuildingElementNumber].NextUpgradeImages[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel];

////if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel > 0)
//{
//    if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingMaxLevel) //4 < 5
//    {
//        for (int i = 0; i < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel; i++) // 0 < 4
//        {
//            inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true); // Star[0] = true
//            inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = _UpgradeStarStates[0]; //Star[0] = perfect_Star
//        }
//        if (mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged) //true
//        {
//            inButton.transform.GetChild(4).GetChild(mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1).GetComponent<Image>().sprite = _UpgradeStarStates[1];
//        }
//        if (!mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
//        {
//            StartCoroutine(InvokeNextCostForButton(inButton, inBuildingElementNumber));
//        }
//        inButton.transform.GetChild(3).GetComponent<Image>().sprite = mBuildingManagerRef._buildingData[inBuildingElementNumber].NextUpgradeImages[mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel];
//    }
//    else
//    {
//        inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Maxed";
//        for (int i = 0; i < mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel; i++)
//        {
//            inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true);
//            inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = _UpgradeStarStates[0];
//        }
//        if (mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged) //true
//        {
//            inButton.transform.GetChild(4).GetChild(mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1).GetComponent<Image>().sprite = _UpgradeStarStates[1];
//            inButton.transform.GetChild(1).gameObject.SetActive(true);
//            inButton.transform.GetChild(5).gameObject.SetActive(false);
//        }
//        else
//        {
//            inButton.transform.GetChild(1).gameObject.SetActive(false);
//            inButton.transform.GetChild(5).gameObject.SetActive(true);
//        }

//    }
//}
//        else if (mBuildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel == 0 && mBuildingManagerRef._buildingData[inBuildingElementNumber].isBuildingDamaged)
//{
//    inButton.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = _FirstStarState[1]; //Broken Silohette
//}
//else
//{
//    inButton.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = _FirstStarState[0]; //Un_Broken Silohette
//}

#region BuildMenuUI
//buildingManagerRef._buildingData[i]._respectiveBuildingButtons = buildingsButtonRef;


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using TMPro;

//public class BuildMenuUI : MonoBehaviour
//{
//    //public GameObject buildPanelGameObject;
//    //public GameObject screenItemsUIPanel;
//    //public GameObject drawButtonUI;

//    [SerializeField] private Transform ContentView;
//    [SerializeField] private BuildingManager buildingManagerRef;

//    [SerializeField] private GameObject BuildingItemTemplate;

//    private GameManager mGameManager;
//    private int generatedNumber;

//    public Sprite[] buttonState;
//    public Sprite[] starState;

//    private void Start()
//    {
//        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//    }

//    //public List<GameObject> ButtonTemplatesHolder = new List<GameObject>();

//    public void SetUpgradeButtons()
//    {

//        //BuildingItemTemplate = ContentView.GetChild(0).gameObject;
//        for (int j = 0; j < ContentView.transform.childCount; j++)
//        {
//            Destroy(ContentView.GetChild(j).gameObject);
//        }
//        for (int i = 0; i < buildingManagerRef._buildingData.Count; i++)
//        {
//            //Instantiate a button and pass it to its respective building.
//            GameObject buildingsButtonRef = Instantiate(BuildingItemTemplate, ContentView);
//            buildingsButtonRef.name = buildingManagerRef._buildingData[i]._buildingDisplayName + " Button";
//            buildingManagerRef._buildingData[i]._respectiveBuildingButtons = buildingsButtonRef;
//            int ElementNumberOfBuildingToBeUpgradedOrRepaired = i;
//            //if (generatedNumber < 1)
//            {
//                //Assigning the building upgrade cost.
//                if (!buildingManagerRef._buildingData[i].isBuildingDamaged)
//                {
//                    if (buildingManagerRef._buildingData[i]._buildingLevel < buildingManagerRef._buildingData[i]._buildingMaxLevel)
//                    {
//                        buildingsButtonRef.transform.GetChild(1).GetComponent<Image>().sprite = buttonState[0];
//                        buildingsButtonRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[i].UpgradeCosts[buildingManagerRef._buildingData[i]._buildingLevel].ToString();
//                    }
//                }
//                else
//                {
//                    ButtonRepairState(buildingsButtonRef,ElementNumberOfBuildingToBeUpgradedOrRepaired);
//                }
//                //buildingTemplateRef.transform.GetChild(1).gameObject.AddComponent<Button>().onClick.RemoveAllListeners();
//                //Assigning the button to the buildings
//                buildingsButtonRef.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
//                {
//                    //Check if player has enough coins and then do the upgrade building function
//                    if (!buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].isBuildingDamaged)
//                    {
//                        if (GameManager.Instance.HasEnoughCoins(buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel]))
//                        {
//                            buildingManagerRef.GrabElementNumberBasedOnButtonClick(ElementNumberOfBuildingToBeUpgradedOrRepaired);
//                            mGameManager._coins -= buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel - 1];
//                            UpdateBuildingImage(buildingsButtonRef, ElementNumberOfBuildingToBeUpgradedOrRepaired);
//                        }
//                        else
//                        {
//                            Debug.Log("Not Enough Coins");
//                        }
//                    }
//                    else
//                    {
//                        if (GameManager.Instance.HasEnoughCoins(buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel]))
//                        {
//                            buildingManagerRef.GrabElementNumberBasedOnButtonClick(ElementNumberOfBuildingToBeUpgradedOrRepaired);
//                            buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].isBuildingDamaged = false;
//                            buildingsButtonRef.transform.GetChild(1).GetComponent<Image>().sprite = buttonState[0];
//                            mGameManager._coins -= buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._repairCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel - 1];
//                            UpdateBuildingImage(buildingsButtonRef, ElementNumberOfBuildingToBeUpgradedOrRepaired);
//                            //buildingsButtonRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel].ToString();
//                        }
//                        else
//                        {
//                            Debug.Log("Not Enough Coins");

//                        }
//                    }
//                });

//                //Assign the Building Name
//                buildingsButtonRef.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = buildingManagerRef._buildingData[i]._buildingDisplayName;

//                //Update the UI Button building Image
//                UpdateBuildingImage(buildingsButtonRef, ElementNumberOfBuildingToBeUpgradedOrRepaired);
//            }
//        }
//        //Destroy(BuildingItemTemplate);
//        generatedNumber += 1;
//    }

//    private void ButtonRepairState(GameObject inButton , int inBuildingElementNumber)
//    {
//        inButton.transform.GetChild(1).GetComponent<Image>().sprite = buttonState[1];
//        inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[inBuildingElementNumber]._repairCosts[buildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1].ToString();
//    }

//    private void UpdateBuildingImage(GameObject inButton,int inElementNumber)
//    {
//        if (buildingManagerRef._buildingData[inElementNumber]._buildingLevel < buildingManagerRef._buildingData[inElementNumber]._buildingMaxLevel) //4 < 5
//        {
//            for (int i = 0; i < buildingManagerRef._buildingData[inElementNumber]._buildingLevel; i++) // 0 < 4
//            {
//                inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true); // Star[0] = true
//                inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = starState[0]; //Star[0] = perfect_Star

//                //else
//                //{
//                //    inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true);
//                //    inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = starState[0];
//                //}
//                //else
//                //{
//                //    inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = starState[1];
//                //}
//            }
//            if (buildingManagerRef._buildingData[inElementNumber].isBuildingDamaged) //true
//            {
//                inButton.transform.GetChild(4).GetChild(buildingManagerRef._buildingData[inElementNumber]._buildingLevel - 1).GetComponent<Image>().sprite = starState[1];
//            }
//            if (!buildingManagerRef._buildingData[inElementNumber].isBuildingDamaged)
//            {
//                StartCoroutine(InvokeNextCostForButton(inButton, inElementNumber));
//            }
//            inButton.transform.GetChild(3).GetComponent<Image>().sprite = buildingManagerRef._buildingData[inElementNumber].NextUpgradeImages[buildingManagerRef._buildingData[inElementNumber]._buildingLevel];
//        }
//        else
//        {
//            inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Maxed";
//            for (int i = 0; i < buildingManagerRef._buildingData[inElementNumber]._buildingLevel; i++)
//            {
//                inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true);
//            }
//            inButton.transform.GetChild(3).GetComponent<Image>().sprite = buildingManagerRef._buildingData[inElementNumber].NextUpgradeImages[buildingManagerRef._buildingData[inElementNumber]._buildingLevel - 1];

//            inButton.transform.GetChild(1).gameObject.SetActive(false);
//            inButton.transform.GetChild(5).gameObject.SetActive(true);
//        }
//    }

//    private IEnumerator InvokeNextCostForButton(GameObject Button, int ElementNumber)
//    {
//        yield return new WaitForSeconds(1.50f);
//        Button.transform.GetChild(1).GetComponent<Button>().interactable = true;
//        Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
//        Button.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[ElementNumber].UpgradeCosts[buildingManagerRef._buildingData[ElementNumber]._buildingLevel].ToString();
//    }
//}
#endregion

//void residue()
//{
//    //if (!buildingManagerRef._isAnotherBuildingInConstruction)
//    //{
//    //if (buildingManagerRef._buildingData[BuildingUpgradeNumber]._buildingLevel != 0)
//    //{



//    //}
//    //else
//    //{
//    //    buildingManagerRef.GrabElementNumberBasedOnButtonClick(BuildingUpgradeNumber);
//    //    mGameManager._coins -= buildingManagerRef._buildingData[BuildingUpgradeNumber].UpgradeCosts[buildingManagerRef._buildingData[BuildingUpgradeNumber]._buildingLevel - 1];
//    //    UpdateBuildingImage(buildingTemplateRef, BuildingUpgradeNumber);
//    //}
//    //}
//}