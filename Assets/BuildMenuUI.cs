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

    public Transform ContentView;
    public BuildingManager buildingManagerRef;

    private GameObject BuildingItemTemplate;

    public List<GameObject> ButtonTemplatesHolder = new List<GameObject>();

    void Start()
    {
        BuildingItemTemplate = ContentView.GetChild(0).gameObject;
        for (int i = 0; i <  buildingManagerRef._buildingData.Length; i++)
        {
            GameObject buildingTemplateRef = Instantiate(BuildingItemTemplate, ContentView);
            ButtonTemplatesHolder.Add(buildingTemplateRef);
            //buildingTemplateRef.transform.GetChild(1).gameObject.AddComponent<Button>().onClick.AddListener(() => { buildingManagerRef.BuildingUpgrade(i); });
            buildingTemplateRef.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = buildingManagerRef._buildingData[i]._buildingName;
            buildingTemplateRef.transform.GetChild(3).GetComponent<Image>().sprite = buildingManagerRef._buildingData[i].NextUpgradeImages[buildingManagerRef._buildingData[i]._buildingLevel];
        }

        Destroy(BuildingItemTemplate);
        //AttachButton();
    }

    //void AttachButton()
    //{
    //    for (int i = 0; i < ButtonTemplatesHolder.Count; i++)
    //    {
    //        ButtonTemplatesHolder[i].gameObject.AddComponent<Button>().onClick.AddListener(() => { buildingManagerRef.BuildingUpgrade(i); });
    //    }
    //}

    void Update()
    {
        
    }

    //public void BuildButton()
    //{
    //    buildPanelGameObject.SetActive(true);
    //    screenItemsUIPanel.SetActive(false);
    //    drawButtonUI.SetActive(false);
    //}

    //public void ReturnButton()
    //{
    //    buildPanelGameObject.SetActive(false);
    //    screenItemsUIPanel.SetActive(true);
    //    drawButtonUI.SetActive(true);
    //}
}
