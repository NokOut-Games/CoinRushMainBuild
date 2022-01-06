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

    private GameObject BuildingItemTemplate;

    private GameManager mGameManager;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //public List<GameObject> ButtonTemplatesHolder = new List<GameObject>();

    public void SetUpgradeButtons()
    {
        BuildingItemTemplate = ContentView.GetChild(0).gameObject;
        for (int i = 0; i < buildingManagerRef._buildingData.Count; i++)
        {
            GameObject buildingTemplateRef = Instantiate(BuildingItemTemplate, ContentView);
            buildingTemplateRef.name = buildingManagerRef._buildingData[i]._buildingName + " Button";
            //ButtonTemplatesHolder.Add(buildingTemplateRef);
            int BuildingUpgradeNumber = i;
            buildingManagerRef._buildingData[i]._respectiveBuildingButtons = buildingTemplateRef;
            if(buildingManagerRef._buildingData[i]._buildingLevel<buildingManagerRef._buildingData[i]._buildingMaxLevel)
                buildingTemplateRef.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[i].UpgradeCosts[buildingManagerRef._buildingData[i]._buildingLevel].ToString();

            buildingTemplateRef.transform.GetChild(1).gameObject.AddComponent<Button>().onClick.AddListener(() =>
            {
                //if (!buildingManagerRef._isAnotherBuildingInConstruction)
                //{
                //if (buildingManagerRef._buildingData[BuildingUpgradeNumber]._buildingLevel != 0)
                //{
                    if (GameManager.Instance.HasEnoughCoins(buildingManagerRef._buildingData[BuildingUpgradeNumber].UpgradeCosts[buildingManagerRef._buildingData[BuildingUpgradeNumber]._buildingLevel]))
                    {
                        buildingManagerRef.GrabElementNumberBasedOnButtonClick(BuildingUpgradeNumber);
                        mGameManager._coins -= buildingManagerRef._buildingData[BuildingUpgradeNumber].UpgradeCosts[buildingManagerRef._buildingData[BuildingUpgradeNumber]._buildingLevel - 1];
                        UpdateBuildingImage(buildingTemplateRef, BuildingUpgradeNumber);
                    }
                    else
                    {
                        Debug.Log("Not Enough Coins");
                    }
                //}
                //else
                //{
                //    buildingManagerRef.GrabElementNumberBasedOnButtonClick(BuildingUpgradeNumber);
                //    mGameManager._coins -= buildingManagerRef._buildingData[BuildingUpgradeNumber].UpgradeCosts[buildingManagerRef._buildingData[BuildingUpgradeNumber]._buildingLevel - 1];
                //    UpdateBuildingImage(buildingTemplateRef, BuildingUpgradeNumber);
                //}
                //}
            });
            
            UpdateBuildingImage(buildingTemplateRef, BuildingUpgradeNumber);
            buildingTemplateRef.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = buildingManagerRef._buildingData[i]._buildingName;

        }

        Destroy(BuildingItemTemplate);
        
        //AttachButton();
    }

    private void UpdateBuildingImage(GameObject inButton,int inElementNumber)
    {
        if (buildingManagerRef._buildingData[inElementNumber]._buildingLevel < buildingManagerRef._buildingData[inElementNumber]._buildingMaxLevel)
        {
            for (int i = 0; i < buildingManagerRef._buildingData[inElementNumber]._buildingLevel; i++)
            {

                inButton.transform.GetChild(4).GetChild(i).gameObject.SetActive(true);
            }
            StartCoroutine(InvokeNextCostForButton(inButton, inElementNumber));
            inButton.transform.GetChild(3).GetComponent<Image>().sprite = buildingManagerRef._buildingData[inElementNumber].NextUpgradeImages[buildingManagerRef._buildingData[inElementNumber]._buildingLevel];
        }
        
        else {
            
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
        yield return new WaitForSeconds(1.01f);
        Button.transform.GetChild(1).GetComponent<Button>().interactable = true;
        Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        Button.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = buildingManagerRef._buildingData[ElementNumber].UpgradeCosts[buildingManagerRef._buildingData[ElementNumber]._buildingLevel].ToString();
    }
}
