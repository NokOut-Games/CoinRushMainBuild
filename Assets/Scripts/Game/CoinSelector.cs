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
    [SerializeField] private GameObject RewardDisplayPanel;
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
        mlevelLoadManagerRef = mGameManager.gameObject.GetComponent<LevelLoadManager>();
        mCoinProbability = GameObject.Find("CoinProbabalizer").GetComponent<CoinProbability>();
        transparentBackgroundPlane = GameObject.Find("TransparentBackground");
        RewardDisplayPanel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        rewardText = RewardDisplayPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        HammerSpawnPoint = GameObject.Find("HammerSpawnPoint").transform;
        PigSelectedPoint = GameObject.Find("PigFocusedPoint").transform;
    }

    private void OnMouseDown()
    {
        int coinValue = mCoinProbability.DisplayTheFinalElementBasedOnRandomValueGenerated();
        
        //Changing the Coinvalue
        /*rewardText.text = coinValue.ToString();
        mGameManager._coins += coinValue;*/
        if (GameManager.Instance._MultiplierValue <= 1)
        {
            rewardText.text = coinValue.ToString();
            mGameManager._coins += coinValue;
        }
        else
        {
            rewardText.text = "Bet Multiplier " + GameManager.Instance._MultiplierValue + "X" + "\n" + (coinValue * GameManager.Instance._MultiplierValue).ToString();
            mGameManager._coins += coinValue* GameManager.Instance._MultiplierValue;
        }
        //Assign it to chest which player clicks on and pass the values
        GameObject SelectedPig = this.gameObject;
        Debug.Log(SelectedPig);
        SelectedPig.GetComponent<ChestValue>()._value = coinValue;
        CoinShowerSpawnPoint = SelectedPig.transform.GetChild(0).transform;
        GodRaysSpawnPoint = SelectedPig.transform.GetChild(2).transform;
        HammerHitPigSpawnPoint = SelectedPig.transform.GetChild(1).transform;

        StartCoroutine(PiggyBankFocus(SelectedPig));

        //Optional code for disabling Animators
        //for (int i = 0; i < mCoinChests.Length; i++)
        //{
        //    if (mCoinChests[i].transform.name != SelectedPig.name)
        //    {
        //        Destroy(mCoinChests[i].transform.GetComponent<Animator>());
        //    }
        //}
    }

    private IEnumerator PiggyBankFocus(GameObject inPigSelected)
    {
        Debug.LogError("1");
        //On Pig Selected
        Destroy(inPigSelected.GetComponent<BoxCollider>());
        //inPigSelected.GetComponent<Animator>().SetTrigger("isSelected?"); //This 
        //Movement to target position
        Vector3 targetPosition = new Vector3(PigSelectedPoint.position.x,PigSelectedPoint.position.y,PigSelectedPoint.position.z);
        Vector3 targetRotation = new Vector3(PigSelectedPoint.eulerAngles.x, PigSelectedPoint.eulerAngles.y, PigSelectedPoint.eulerAngles.z);
        transparentBackgroundPlane.GetComponent<Renderer>().material.DOFloat(0.8f, "_alpha", mFadeTime);
        inPigSelected.transform.DORotate(targetRotation, 1, RotateMode.Fast);
        inPigSelected.transform.DOMove(targetPosition, 1, false)
        .OnComplete(() =>
        {
            PlayParticleEffects(GodRaysParticleSystem, GodRaysSpawnPoint,1f);
            //PlayParticleEffects(HammerSpawnParticle , HammerSpawnPoint,1f);
            GameObject HammerRef = Instantiate(HammerPrefab, HammerSpawnPoint.position, HammerSpawnPoint.rotation);
            //Debug.Log(HammerRef);
            Destroy(HammerRef, 10f);
        });
      
        //To Prevent Clicking Other Pigs in the background
        transparentBackgroundPlane.GetComponent<BoxCollider>().enabled = true;

        //yield return new WaitForSeconds(mTimeBetweenHammerSpawnAndBreakAnimation);  //Yield to Wait for the Hammer
        //inPigSelected.GetComponent<Animator>().SetTrigger("isBreaking?"); //This
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
        //Destroy(ParticleRef, DestroySeconds);
        //Play the particles
    }

    void DisplayRewardAndInvokeScene()
    {
        RewardDisplayPanel.SetActive(true);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

   
}


//void Residue()
//{
//float v = 0.0f;
//public float easing = 1f;

//rewardText.text = coinValue.ToString();
//RewardDisplayPanel.SetActive(true);


//    //float t = 0;
//    //while (t <= 1.0)
//    //{
//    //    t += Time.deltaTime / easing;
//    //    transparentBackgroundPlane.GetComponent<Renderer>().material.SetFloat("_alpha", v = Mathf.Lerp(v, 0.8f, Mathf.SmoothStep(0f, 1f, t)));
//    //    //Invoke("DisplayRewardAndInvokeScene", 1.5f);
//    //}
//    //yield return null;
//}