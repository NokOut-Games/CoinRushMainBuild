using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Multiplier : MonoBehaviour
{
    [SerializeField] TMP_Text multiplierTxt;
    [SerializeField] Slider multiplierSlider;
    [SerializeField] Image timerImage;

    Tutorial tutorial;
    float waitTime = 3f;
   
    public void OnValueChange(float value)
    {
        if (tutorial != null) tutorial.RegisterUserAction();

        multiplierSlider.value = Mathf.RoundToInt(value);
        multiplierTxt.text = Mathf.RoundToInt(value).ToString()+"X";
        GameManager.Instance._MultiplierValue = Mathf.RoundToInt(value);
    }

    int GetMaxMultiplierValue()
    {
        for (int i = 0; i <= 10; i++)
        {
            if ((i * 3)-3 > GameManager.Instance._energy) return (i - 1);
        }
        return 10;
    }


    private void Update()
    {
        timerImage.fillAmount -= 1.0f / waitTime * Time.deltaTime;
    }
    public void InitiateMulitiplier()
    {
        if (tutorial != null) tutorial.RegisterUserAction();
        multiplierSlider.maxValue = GetMaxMultiplierValue();
        GameManager.Instance._MultiplierValue = 1;
        multiplierSlider.value = 1;
        Invoke(nameof(MultiplierEnergyReduse), 3f);
        timerImage.fillAmount = 1;
    }


    void MultiplierEnergyReduse()
    {
        GameManager.Instance._energy -= (GameManager.Instance._MultiplierValue * 3)-3;
    }

    public void AssignTutorial(Tutorial tutorial)
    {
        this.tutorial = tutorial;
    }

}
