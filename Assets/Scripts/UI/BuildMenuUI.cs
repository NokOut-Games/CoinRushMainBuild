using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BuildMenuUI : MonoBehaviour
{
    //public GameObject buildPanelGameObject;
    //public GameObject screenItemsUIPanel;
    //public GameObject drawButtonUI;

    [SerializeField] private Transform ContentView;
    [SerializeField] private BuildingManager buildingManagerRef;

    [SerializeField] private GameObject BuildingItemTemplate;

    private GameManager mGameManager;
    private int generatedNumber;

    public Sprite[] buttonState;
    public Sprite[] starState;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //public List<GameObject> ButtonTemplatesHolder = new List<GameObject>();

    public void SetUpgradeButtons()
    {
        
        //BuildingItemTemplate = ContentView.GetChild(0).gameObject;
        for (int j = 0; j < ContentView.transform.childCount; j++)
        {
            Destroy(ContentView.GetChild(j).gameObject);
        }
        for (int i = 0; i < buildingManagerRef._buildingData.Count; i++)
        {
            //Instantiate a button and pass it to its respective building.
            GameObject buildingsButtonRef = Instantiate(BuildingItemTemplate, ContentView);
            buildingsButtonRef.name = buildingManagerRef._buildingData[i]._buildingDisplayName + " Button";
            buildingManagerRef._buildingData[i]._respectiveBuildingButtons = buildingsButtonRef;
            int ElementNumberOfBuildingToBeUpgradedOrRepaired = i;
            //if (generatedNumber < 1)
            {
                //Assigning the building upgrade cost.
                if (!buildingManagerRef._buildingData[i].isBuildingDamaged)
                {
                    if (buildingManagerRef._buildingData[i]._buildingLevel < buildingManagerRef._buildingData[i]._buildingMaxLevel)
                    {
                        buildingsButtonRef.transform.GetChild(1).GetComponent<Image>().sprite = buttonState[0];
                        buildingsButtonRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[i].UpgradeCosts[buildingManagerRef._buildingData[i]._buildingLevel].ToString();
                    }
                }
                else
                {
                    ButtonRepairState(buildingsButtonRef,ElementNumberOfBuildingToBeUpgradedOrRepaired);
                }
                //buildingTemplateRef.transform.GetChild(1).gameObject.AddComponent<Button>().onClick.RemoveAllListeners();
                //Assigning the button to the buildings
                buildingsButtonRef.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
                {
                    //Check if player has enough coins and then do the upgrade building function
                    if (!buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].isBuildingDamaged)
                    {
                        if (GameManager.Instance.HasEnoughCoins(buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel]))
                        {
                            buildingManagerRef.GrabElementNumberBasedOnButtonClick(ElementNumberOfBuildingToBeUpgradedOrRepaired);
                            mGameManager._coins -= buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel - 1];
                            UpdateBuildingImage(buildingsButtonRef, ElementNumberOfBuildingToBeUpgradedOrRepaired);
                        }
                        else
                        {
                            Debug.Log("Not Enough Coins");
                        }
                    }
                    else
                    {
                        if (GameManager.Instance.HasEnoughCoins(buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel]))
                        {
                            buildingManagerRef.GrabElementNumberBasedOnButtonClick(ElementNumberOfBuildingToBeUpgradedOrRepaired);
                            buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].isBuildingDamaged = false;
                            buildingsButtonRef.transform.GetChild(1).GetComponent<Image>().sprite = buttonState[0];
                            mGameManager._coins -= buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._repairCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel - 1];
                            UpdateBuildingImage(buildingsButtonRef, ElementNumberOfBuildingToBeUpgradedOrRepaired);
                            //buildingsButtonRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired].UpgradeCosts[buildingManagerRef._buildingData[ElementNumberOfBuildingToBeUpgradedOrRepaired]._buildingLevel].ToString();
                        }
                        else
                        {
                            Debug.Log("Not Enough Coins");

                        }
                    }
                });

                //Assign the Building Name
                buildingsButtonRef.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = buildingManagerRef._buildingData[i]._buildingDisplayName;

                //Update the UI Button building Image
                UpdateBuildingImage(buildingsButtonRef, ElementNumberOfBuildingToBeUpgradedOrRepaired);
            }
        }
        //Destroy(BuildingItemTemplate);
        generatedNumber += 1;
    }

    private void ButtonRepairState(GameObject inButton , int inBuildingElementNumber)
    {
        inButton.transform.GetChild(1).GetComponent<Image>().sprite = buttonState[1];
        inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[inBuildingElementNumber]._repairCosts[buildingManagerRef._buildingData[inBuildingElementNumber]._buildingLevel - 1].ToString();
    }

    private void UpdateBuildingImage(GameObject inButton,int inElementNumber)
    {
        if (buildingManagerRef._buildingData[inElementNumber]._buildingLevel < buildingManagerRef._buildingData[inElementNumber]._buildingMaxLevel) //4 < 5
        {
            for (int i = 0; i < buildingManagerRef._buildingData[inElementNumber]._buildingLevel; i++) // 0 < 4
            {
                inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true); // Star[0] = true
                inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = starState[0]; //Star[0] = perfect_Star
                
                //else
                //{
                //    inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true);
                //    inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = starState[0];
                //}
                //else
                //{
                //    inButton.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = starState[1];
                //}
            }
            if (buildingManagerRef._buildingData[inElementNumber].isBuildingDamaged) //true
            {
                inButton.transform.GetChild(4).GetChild(buildingManagerRef._buildingData[inElementNumber]._buildingLevel - 1).GetComponent<Image>().sprite = starState[1];
            }
            if (!buildingManagerRef._buildingData[inElementNumber].isBuildingDamaged)
            {
                StartCoroutine(InvokeNextCostForButton(inButton, inElementNumber));
            }
            inButton.transform.GetChild(3).GetComponent<Image>().sprite = buildingManagerRef._buildingData[inElementNumber].NextUpgradeImages[buildingManagerRef._buildingData[inElementNumber]._buildingLevel];
        }
        else
        {
            inButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Maxed";
            for (int i = 0; i < buildingManagerRef._buildingData[inElementNumber]._buildingLevel; i++)
            {
                inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true);
            }
            inButton.transform.GetChild(3).GetComponent<Image>().sprite = buildingManagerRef._buildingData[inElementNumber].NextUpgradeImages[buildingManagerRef._buildingData[inElementNumber]._buildingLevel - 1];

            inButton.transform.GetChild(1).gameObject.SetActive(false);
            inButton.transform.GetChild(5).gameObject.SetActive(true);
        }
    }

    private IEnumerator InvokeNextCostForButton(GameObject Button, int ElementNumber)
    {
        yield return new WaitForSeconds(1.50f);
        Button.transform.GetChild(1).GetComponent<Button>().interactable = true;
        Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        Button.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[ElementNumber].UpgradeCosts[buildingManagerRef._buildingData[ElementNumber]._buildingLevel].ToString();
    }
}

















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