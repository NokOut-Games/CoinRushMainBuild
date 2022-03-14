using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class BoxReactor : MonoBehaviour
{
    [SerializeField] private ResultPanelUI RewardDisplayPanel;

    [SerializeField] private GameObject EnergyCanSmall;

    [SerializeField] private GameObject EnergyCanMedium;
    [SerializeField] private GameObject EnergyCanLarge;

    [SerializeField] private GameObject EnergyCanSmallParticle;
    [SerializeField] private GameObject EnergyCanMediumParticle;
    [SerializeField] private GameObject EnergyCanLargeParticle;

    private GameObject canSpawnLocation;

    EnergySelector energySelector;

    [SerializeField] private float RewardDisplayInvokeTime;

    public GameObject can;

    [Header("Camera Shake Values: ")]
    [SerializeField] private float mDuration;
    [SerializeField] private int mStrength;
    [SerializeField] private int mVibration;
    [SerializeField] private int mRandomness;

    [Header("Can Values And Camera Zoom In Properties: ")]
    [SerializeField] private float mEndGameCanScaleValue;
    [SerializeField] private float mCanYHeight;
    [SerializeField] private float mCameraYHeight;
    [SerializeField] private float mCameraZoomAmount;

    [SerializeField] private float mCanMoveDuration;
    [SerializeField] private float mCameraMoveDuration;

    [SerializeField] private GameObject HitSmokeEffect;
    [SerializeField] private GameObject HitSmokeRingEffect;
    [SerializeField] int rewardValue;


    void PlayParticleEffects(GameObject inChest)
    {
        Transform CollisionSmokeSpawnPoint = inChest.transform.Find("CollisionSmokeSpawner").transform;
        Transform HitSmokeSpreadLocation = inChest.transform.Find("HitSmokeSpread").transform;

        GameObject Particle1 = Instantiate(HitSmokeEffect, CollisionSmokeSpawnPoint.position,Quaternion.identity);
        GameObject Particle2 = Instantiate(HitSmokeRingEffect, HitSmokeSpreadLocation.position, Quaternion.identity);
        Destroy(Particle1, 1f);
        Destroy(Particle2, 1f);
    }

    private void OnCollisionEnter(Collision other)
    {               
        if (other.gameObject.tag == "EnergyChestBox")
        {
            GetComponent<Animator>().Play("break");
            PlayParticleEffects(other.gameObject);
            energySelector = other.gameObject.GetComponent<EnergySelector>();
            energySelector.EnergyFalling = false;
            Camera.main.DOShakePosition(mDuration, mStrength, mVibration, mRandomness, true);
            other.transform.GetChild(0).Find("Wind_Effect").gameObject.SetActive(false);
            canSpawnLocation = other.transform.Find("CanSpawnLocation").gameObject;

            Animator crateAnimRef = other.transform.GetChild(0).gameObject.GetComponent<Animator>();
            ChestValue crateValueRef = other.gameObject.GetComponent<ChestValue>();    

            Instantiate(EnergyCanSmall, canSpawnLocation.transform.position, Quaternion.identity);
            StartCoroutine(SpawnParticleCoroutine(EnergyCanMediumParticle, 2, -10));
            crateAnimRef.SetTrigger("isBreaking?");
            rewardValue = crateValueRef._value;
            Invoke(nameof(SetRewardPanel), 2f);
        }
    }


    IEnumerator SpawnParticleCoroutine(GameObject g, float a,float z)
    {
        yield return new WaitForSeconds(a);
        Instantiate(g, canSpawnLocation.transform.position+new Vector3(0,10,z), Quaternion.identity);
    }

    public void BackToMainScene()
    {
        LevelLoadManager.instance.BacktoHome();
    }


    void SetRewardPanel()
    {
        RewardDisplayPanel.gameObject.SetActive(true);

        RewardDisplayPanel.ShowMultiplierDetails(0, 0, " Multiplier", GameManager.Instance._MultiplierValue.ToString());
        RewardDisplayPanel.ShowMultiplierDetails(1, 1, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
        RewardDisplayPanel.ShowResultTotal(1, Mathf.RoundToInt(rewardValue * GameManager.Instance._MultiplierValue* GameManager.Instance.cucuMultiplier).ToString());

        GameManager.Instance._energy += Mathf.RoundToInt(rewardValue * GameManager.Instance._MultiplierValue * GameManager.Instance.cucuMultiplier);
    }
}