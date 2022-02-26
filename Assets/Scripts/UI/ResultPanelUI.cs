using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelUI : MonoBehaviour
{
    [SerializeField] Sprite[] multiplierIcons;
    [SerializeField] Sprite[]rewardIcons;
    [SerializeField] TMP_Text titleText;
    [SerializeField] GameObject[] multipliersDetailsGameObject = new GameObject[3];
    [SerializeField] GameObject[] resultsElement = new GameObject[3];



    public void ShowMultiplierDetails(int multiplierIndex,int multiplierIconIndex,string multiplierName,string multiplierValue)
    {
        multipliersDetailsGameObject[multiplierIndex].transform.GetChild(0).GetComponent<Image>().sprite = multiplierIcons[multiplierIconIndex];
        multipliersDetailsGameObject[multiplierIndex].transform.GetChild(1).GetComponent<TMP_Text>().text = multiplierName;
        multipliersDetailsGameObject[multiplierIndex].transform.GetChild(2).GetComponent<TMP_Text>().text = "X"+multiplierValue;
        multipliersDetailsGameObject[multiplierIndex].SetActive(true);
    }

    public void ShowResultTotal(int[] IconIndex,string[] Value)
    {
        for (int i = 0; i < IconIndex.Length; i++)
        {
            resultsElement[i].transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = rewardIcons[IconIndex[i]];
            int currency = int.Parse( Value[i]);
            resultsElement[i].transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = currency.ToString();
            resultsElement[i].SetActive(true);
        }
    }
    public void ShowResultTotal(int IconIndex, string Value)
    {
       
            resultsElement[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = rewardIcons[IconIndex];
            int currency = int.Parse(Value);

            resultsElement[0].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = currency.ToString();
            resultsElement[0].SetActive(true);
       
    }
    public void SetTitle(string title)
    {
        titleText.text = title;


    }
}
