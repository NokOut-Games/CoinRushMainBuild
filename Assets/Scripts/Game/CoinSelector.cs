using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CoinSelector : MonoBehaviour
{
    [Header ("Coin Scene References: ")]
    [SerializeField] private CoinProbability mCoinProbability;
    [SerializeField] private GameObject[] mCoinChests; //Optional
    [SerializeField] private GameObject transparentBackgroundPlane;
    [SerializeField] private float mFadeTime;

    [Header ("Other References: ")]
    [SerializeField] private GameManager mGameManager;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private ResultPanelUI RewardDisplayPanel;
    [SerializeField] private float mTimeToOpenRewardPanel;

    [Header("Hammer Spawn And HammerHit Particles: ")]
    [SerializeField] private GameObject HammerPrefab;
    [SerializeField] private Transform HammerSpawnPoint;
    private Transform HammerHitPigSpawnPoint;    
    [SerializeField] private GameObject HammerHitPigParticle;
    [SerializeField] private GameObject HammerSpawnParticle;

    [Header("Pig Selection And Its Related Particles: ")]
    [SerializeField] private Transform PigSelectedPoint;
    [SerializeField] private float mTimeBetweenHammerSpawnAndBreakAnimation;
    [SerializeField] private float mTimeBetweenPigBreakAndCoinShower;
    [SerializeField] private GameObject GodRaysParticleSystem;
    private Transform GodRaysSpawnPoint;
    [SerializeField] private GameObject CoinShowerParticleEffect;
    private Transform CoinShowerSpawnPoint;

    private LevelLoadManager mlevelLoadManagerRef;

    [SerializeField] private float HammerImpactTime;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mCoinProbability = GameObject.Find("CoinProbabalizer").GetComponent<CoinProbability>();
        transparentBackgroundPlane = GameObject.Find("TransparentBackground");
        RewardDisplayPanel = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<ResultPanelUI>();
        HammerSpawnPoint = GameObject.Find("HammerSpawnPoint").transform;
        PigSelectedPoint = GameObject.Find("PigFocusedPoint").transform;
    }

    private void OnMouseDown()
    {
        int coinValue = mCoinProbability.DisplayTheFinalElementBasedOnRandomValueGenerated();

       
        RewardDisplayPanel.ShowMultiplierDetails(0, 0, " Multiplier", GameManager.Instance._MultiplierValue.ToString());
        RewardDisplayPanel.ShowMultiplierDetails(1, 1, "Cucu Multiplier", GameManager.Instance.cucuMultiplier.ToString());

        RewardDisplayPanel.ShowResultTotal(0, (coinValue * GameManager.Instance._MultiplierValue).ToString());

        mGameManager._coins +=(int) (coinValue* GameManager.Instance._MultiplierValue *GameManager.Instance.cucuMultiplier);
        //Assign it to chest which player clicks on and pass the values
        GameObject SelectedPig = this.gameObject;
        SelectedPig.GetComponent<ChestValue>()._value = coinValue;
        CoinShowerSpawnPoint = SelectedPig.transform.GetChild(0).transform;
        GodRaysSpawnPoint = SelectedPig.transform.GetChild(2).transform;
        HammerHitPigSpawnPoint = SelectedPig.transform.GetChild(1).transform;

        StartCoroutine(PiggyBankFocus(SelectedPig));

    }

    private IEnumerator PiggyBankFocus(GameObject inPigSelected)
    {
        Destroy(inPigSelected.GetComponent<BoxCollider>());
        Vector3 targetPosition = new Vector3(PigSelectedPoint.position.x,PigSelectedPoint.position.y,PigSelectedPoint.position.z);
        Vector3 targetRotation = new Vector3(PigSelectedPoint.eulerAngles.x, PigSelectedPoint.eulerAngles.y, PigSelectedPoint.eulerAngles.z);
        transparentBackgroundPlane.GetComponent<Renderer>().material.DOFloat(0.8f, "_alpha", mFadeTime);
        inPigSelected.transform.DORotate(targetRotation, 1, RotateMode.Fast);
        inPigSelected.transform.DOMove(targetPosition, 1, false)
        .OnComplete(() =>
        {
            PlayParticleEffects(GodRaysParticleSystem, GodRaysSpawnPoint,1f);
            GameObject HammerRef = Instantiate(HammerPrefab, HammerSpawnPoint.position, HammerSpawnPoint.rotation);
            Destroy(HammerRef, 10f);
        });
      
        transparentBackgroundPlane.GetComponent<BoxCollider>().enabled = true;

        yield return new WaitForSeconds(HammerImpactTime);
        PlayParticleEffects(HammerHitPigParticle, HammerHitPigSpawnPoint,100);
        yield return new WaitForSeconds(mTimeBetweenPigBreakAndCoinShower);
        inPigSelected.gameObject.SetActive(false);
        PlayParticleEffects(CoinShowerParticleEffect , CoinShowerSpawnPoint,2);

        Invoke(nameof(DisplayRewardAndInvokeScene), 3f);
    }

    

    void PlayParticleEffects(GameObject inParticleEffectGameObject, Transform inParticleSpawnPosition , float DestroySeconds)
    {
        GameObject ParticleRef = Instantiate(inParticleEffectGameObject, inParticleSpawnPosition.position, Quaternion.identity);
    }

    void DisplayRewardAndInvokeScene()
    {
        RewardDisplayPanel.gameObject.SetActive(true);
    }

   
}
