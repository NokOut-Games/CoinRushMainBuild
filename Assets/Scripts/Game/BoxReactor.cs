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

    private bool isCanInstantiated;
    private bool isCollided;

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


    private void Start()
    {
       // mLevelLoadManagerRef = GameObject.Find("GameManager").GetComponent<LevelLoadManager>();
        isCollided = false; 
        isCanInstantiated = false;
    }

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
        
        if (!isCollided)
        {
            if (other.gameObject.tag == "EnergyChestBox")
            {
                isCollided = true;
                //Destroy(other.gameObject.GetComponent<BoxCollider>());
                

                PlayParticleEffects(other.gameObject);

                energySelector = other.gameObject.GetComponent<EnergySelector>();
                //Stopping Coroutine Just in Case
                energySelector.EnergyFalling = false;
                Camera.main.DOShakePosition(mDuration, mStrength, mVibration, mRandomness, true);

                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                //Disabling the particle Effect
                other.transform.Find("Wind_Effect").gameObject.SetActive(false);

                //Getting the can spawn location to spawn the can
                canSpawnLocation = other.transform.Find("CanSpawnLocation").gameObject;

                //Getting references
                Animator crateAnimRef = other.transform.parent.gameObject.GetComponent<Animator>();
                ChestValue crateValueRef = other.gameObject.GetComponent<ChestValue>();

                if (!isCanInstantiated)
                    //Instantiating cans based on chest value
                    switch (other.gameObject.GetComponent<ChestValue>()._value)
                    {
                        case 15:
                            can = Instantiate(EnergyCanSmall, canSpawnLocation.transform.position, Quaternion.identity);
                            StartCoroutine(SpawnParticleCoroutine(EnergyCanMediumParticle, 2,-10));

                            crateAnimRef.SetTrigger("isBreaking?");
                            isCanInstantiated = true;
                            SetRewardPanel(crateValueRef);

                            Invoke("InvokeKinematic", .75f);
                            break;
                        case 30:
                            can = Instantiate(EnergyCanMedium, canSpawnLocation.transform.position, Quaternion.identity);
                            StartCoroutine(SpawnParticleCoroutine(EnergyCanMediumParticle, 2,-10));

                            crateAnimRef.SetTrigger("isBreaking?");
                            isCanInstantiated = true;
                            SetRewardPanel(crateValueRef);
                            Invoke("InvokeKinematic", .75f);
                            break;
                        case 75:
                            can = Instantiate(EnergyCanLarge, canSpawnLocation.transform.position, Quaternion.identity);
                            StartCoroutine(SpawnParticleCoroutine(EnergyCanLargeParticle,2,-10));
                            crateAnimRef.SetTrigger("isBreaking?");
                            isCanInstantiated = true;
                            SetRewardPanel(crateValueRef);
                            Invoke("InvokeKinematic", .75f);
                            break;
                    }
                StartCoroutine(CanGameObjectZoomIn(can));

                //Starting to activate Reward Panel
                //Invoke("ActiveRewardPanel", 3f);
            }
        }
        StopCoroutine(energySelector.energyCoroutine);
    }

    IEnumerator CanGameObjectZoomIn(GameObject inCan)
    {
        yield return new WaitForSeconds(.5f);
        //while (true)
        //{
        Vector3 canHeightTargetPosition = new Vector3(inCan.transform.position.x, inCan.transform.position.y + mCanYHeight, inCan.transform.position.z);
        Vector3 cameraTargetPosition = new Vector3(inCan.transform.position.x, inCan.transform.position.y + mCameraYHeight, inCan.transform.position.z - mCameraZoomAmount);
        //inCan.transform.position = Vector3.Lerp(inCan.transform.position, canHeightTargetPosition, 1 * Time.deltaTime);
        inCan.transform.DOMove(canHeightTargetPosition, mCanMoveDuration, false)/*.OnUpdate(()=> can.transform.GetChild(0).gameObject.SetActive(true))*/.OnComplete(() => can.transform.GetChild(0).gameObject.SetActive(true));

        yield return new WaitForSeconds(.3f);
        inCan.transform.DOScale(mEndGameCanScaleValue, 1);
        Camera.main.transform.DOMove(cameraTargetPosition, mCameraMoveDuration, false);

        // Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, 2 * Time.deltaTime);
        Invoke("ActiveRewardPanel", 2f);

        yield return null;
        //}
    }

    void InvokeKinematic()
    {
        can.GetComponent<Rigidbody>().isKinematic = true;
    }


    IEnumerator SpawnParticleCoroutine(GameObject g, float a,float z)
    {
        yield return new WaitForSeconds(a);
        Instantiate(g, canSpawnLocation.transform.position+new Vector3(0,10,z), Quaternion.identity);
    }

    void ActiveRewardPanel()
    {
        RewardDisplayPanel.gameObject.SetActive(true);
    }

    public void BackToMainScene()
    {
        LevelLoadManager.instance.BacktoHome(); //Need to change it from zero to some other value. Will be doing that when scene save system is Done.
    }


    void SetRewardPanel(ChestValue crateValueRef)
    {

        RewardDisplayPanel.ShowMultiplierDetails(0, 0, " Multiplier", GameManager.Instance._MultiplierValue.ToString());
        RewardDisplayPanel.ShowMultiplierDetails(1, 1, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
        RewardDisplayPanel.ShowResultTotal(1, (crateValueRef._value * GameManager.Instance._MultiplierValue).ToString());
        GameManager.Instance._energy += (int)(crateValueRef._value * GameManager.Instance._MultiplierValue * GameManager.Instance.cucuMultiplier);
    }
}



//other.transform.parent.GetComponent<Animator>().SetTrigger("isBreaking?");
//rewardText.text = other.gameObject.GetComponent<ChestValue>()._value.ToString() + "Energy";