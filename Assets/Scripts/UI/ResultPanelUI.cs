using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelUI : MonoBehaviour
{
    public int CountFPS = 30;
    public float Duration = .5f;
    public string NumberFormat = "N0";
    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
           // UpdateText(value);
            _value = value;
        }
    }
    private Coroutine CountingCoroutine;
    [SerializeField] Sprite[] multiplierIcons;
    [SerializeField] Sprite[]rewardIcons;
    [SerializeField] TMP_Text titleText;
    [SerializeField] GameObject[] multipliersDetailsGameObject = new GameObject[3];
    [SerializeField] GameObject[] resultsElement = new GameObject[3];



    public void ShowMultiplierDetails(int multiplierIndex,int multiplierIconIndex,string multiplierName,string multiplierValue)
    {
        multipliersDetailsGameObject[multiplierIndex].transform.GetChild(0).GetComponent<Image>().sprite = multiplierIcons[multiplierIconIndex];
        multipliersDetailsGameObject[multiplierIndex].transform.GetChild(1).GetComponent<TMP_Text>().text = multiplierName;
        multipliersDetailsGameObject[multiplierIndex].transform.GetChild(2).GetComponent<TMP_Text>().text = "x"+multiplierValue;
        multipliersDetailsGameObject[multiplierIndex].SetActive(true);
    }

    public void ShowResultTotal(int[] IconIndex,string[] Value)
    {
        for (int i = 0; i < IconIndex.Length; i++)
        {
            resultsElement[IconIndex.Length-1].transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = rewardIcons[IconIndex[i]];
            //resultsElement[IconIndex.Length - 1].transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = Value[i].ConvertToText("F0");
            UpdateText(int.Parse(Value[i]), resultsElement[IconIndex.Length - 1].transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>());
            resultsElement[IconIndex.Length - 1].SetActive(true);
        }
    }
    public void ShowResultTotal(int IconIndex, string Value)
    {
       
            resultsElement[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = rewardIcons[IconIndex];
        //resultsElement[0].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = Value.ConvertToText("F0");
        UpdateText(int.Parse(Value), resultsElement[0].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>());
        resultsElement[0].SetActive(true);
       
    }
    public void SetTitle(string title)
    {
        titleText.text = title;
    }


    private void UpdateText(int newValue,TMP_Text mText)
    {
        if (CountingCoroutine != null)
        {
            StopCoroutine(CountingCoroutine);
        }

        CountingCoroutine = StartCoroutine(CountText(newValue, mText));
    }

    private IEnumerator CountText(int newValue, TMP_Text mText)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
        int previousValue = _value;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20- 0) / (30*1) // 0.66667 (floortoint)-> 0
        }

        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }

                mText.text=previousValue.ToString(NumberFormat);

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount; // (-20 - 0) / (30 * 1) = -0.66667 -> -1              0 + -1 = -1
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                mText.text=previousValue.ToString(NumberFormat);

                yield return Wait;
            }
        }
    }
}
