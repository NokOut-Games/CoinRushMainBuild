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
    [SerializeField] Sprite[] multiplieImages;

    [SerializeField] Image sliderHandle;

    [SerializeField] TMP_Text energyTxt;
    [SerializeField] Image energyFiller;

    Tutorial tutorial;
    float waitTime = 5f;
    float crntEnergy=> (GameManager.Instance._energy - ((GameManager.Instance._MultiplierValue * 3) - 3));
    [SerializeField]bool timerIsRunning;
    [SerializeField]float timeRemaining = 2;
    CardType cardType;
    [SerializeField] Animator energyReduseAnimator;

    bool sceneLoaded;
  
    public void OnValueChange(float value)
    {
        if (tutorial != null) tutorial.RegisterUserAction();
        GameManager.Instance._MultiplierValue = Mathf.RoundToInt(value);
        sliderHandle.sprite = multiplieImages[GameManager.Instance._MultiplierValue-1];
        energyTxt.text = ""+ crntEnergy;
        energyFiller.fillAmount = crntEnergy>50?50:crntEnergy /50;
        timerImage.gameObject.SetActive(false);
        timerIsRunning = false;
        timeRemaining = 2f;
        timerIsRunning = true;
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

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {  
                timeRemaining = 0;
                if (cardType == CardType.ATTACK) MultiplayerManager.Instance.OnGettingAttackCard();
                else LevelLoadManager.instance.LoadLevelASyncOf(cardType.ToString(), 3000, cardType.ToString());
                MultiplierEnergyReduse();
                timerIsRunning = false;
                sceneLoaded = true;
            }
        }
        else if (timerImage.fillAmount <= 0 && !sceneLoaded)
        {
            sceneLoaded = true;
            if (cardType == CardType.ATTACK) MultiplayerManager.Instance.OnGettingAttackCard();
            else LevelLoadManager.instance.LoadLevelASyncOf(cardType.ToString(), 3000, cardType.ToString());
            MultiplierEnergyReduse();
        }
    }

    public void InitiateMulitiplier(CardType card)
    {
        GameManager.Instance._PauseGame = true;
        if (tutorial != null) tutorial.RegisterUserAction();
        multiplierSlider.maxValue = GetMaxMultiplierValue();
        GameManager.Instance._MultiplierValue = 1;
        multiplierSlider.value = 1;
        timerImage.fillAmount = 1;
        energyTxt.text =""+GameManager.Instance._energy;
        energyFiller.fillAmount = crntEnergy > 50 ? 50 : crntEnergy / 50;
        cardType = card;
    }


    void MultiplierEnergyReduse()
    {
        energyReduseAnimator.GetComponent<TMP_Text>().text = ((GameManager.Instance._MultiplierValue * 3) - 3).ToString() + " Energy Redused";
        energyReduseAnimator.Play("reduseEnergy");
        GameManager.Instance._energy -= (GameManager.Instance._MultiplierValue * 3)-3;
    }

    public void AssignTutorial(Tutorial tutorial)
    {
        this.tutorial = tutorial;
    }

}
