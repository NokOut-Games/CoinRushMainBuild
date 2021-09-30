using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarFillerUI : MonoBehaviour
{
    public Image _energyBarFillerImage;
    public GameManager _gameManager;
    

    public float mEnergy, mMaxEnergy;
    public float lerpSpeed;

    private void Start()
    {
        
    }

    
    private void Update()
    {
        mEnergy = _gameManager._energy;
        mMaxEnergy = _gameManager._maxEnergy;
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
    }

    private void HealthBarFiller()
    {
        _energyBarFillerImage.fillAmount = Mathf.Lerp(_energyBarFillerImage.fillAmount, mEnergy / mMaxEnergy, lerpSpeed);
    }


}
