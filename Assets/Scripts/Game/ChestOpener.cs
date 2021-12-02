using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Sirenix.OdinInspector;

public class ChestOpener : MonoBehaviour
{
    [BoxGroup("Energy Properties")]
    [SerializeField] private EnergyProbability mEnergyProbability;
    [BoxGroup("Energy Properties")]
    [SerializeField] private List<GameObject> mEnergyChests;
    [BoxGroup("Energy Properties")]
    [SerializeField] private float CameraFocusSpeed;
    [BoxGroup("Energy Properties")]
    [SerializeField] private float dropSpeed;

    [BoxGroup("Coin Properties")]
    [SerializeField] private CoinProbability mCoinProbability;
    [BoxGroup("Coin Properties")]
    [SerializeField] private List<GameObject> mCoinChests;
    [BoxGroup("Coin Properties")]
    [SerializeField] private float ItemFocusSpeed;
    [BoxGroup("Coin Properties")]
    [SerializeField] private GameObject transparentBackgroundPlane;

    [BoxGroup("Other References")]
    [SerializeField] private GameManager mGameManager;
    [BoxGroup("Other References")]
    [SerializeField] private TextMeshProUGUI rewardText;
    [BoxGroup("Other References")]
    [SerializeField] private GameObject RewardDisplayPanel;
    [BoxGroup("Other References")]
    [SerializeField] private GameObject BackgroundParentRef;
    [BoxGroup("Other References")]
    [SerializeField] private GameObject CloudRef;

    public Coroutine EnergyFallingCoroutine;
    public bool EnergyFalling;


    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out raycastHit))
            {
                if (raycastHit.transform.gameObject.tag == "EnergyChestBox")
                {
                    //Get the Energy value of probability
                    int energyValue = mEnergyProbability.DisplayTheFinalElementBasedOnRandomValueGenerated();

                    //Changing the Energy value in Gamemanager
                    mGameManager._energy += energyValue;

                    //rewardText.text = energyValue.ToString();
                    //RewardDisplayPanel.SetActive(true);

                    //Assign it to chest which player clicks on and pass the values
                    GameObject Chest = raycastHit.transform.gameObject;
                    Chest.GetComponent<ChestValue>()._value = energyValue;

                    //EnergyListShuffle(mEnergyProbability._energies);
                    //Chest.transform.Find("WindTrialEffect").gameObject.SetActive(true);
                    StartCoroutine(CameraZoomAndFollowEnergy(Chest));
                    
                    //Destroy other chests except the ones clicked
                    for (int i = 0; i < mEnergyChests.Count; i++)
                    {
                        if (mEnergyChests[i].transform.GetChild(0).name != Chest.name)
                        {
                            Destroy(mEnergyChests[i].transform.gameObject);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //Invoke(nameof(BackToMainScene), 1f);
                }
                if (raycastHit.transform.gameObject.tag == "CoinChestBox")
                {
                    //Get the Coin value of probability
                    int coinValue = mCoinProbability.DisplayTheFinalElementBasedOnRandomValueGenerated();
                    //rewardText.text = coinValue.ToString();
                    //RewardDisplayPanel.SetActive(true);

                    //Changing the Coinvalue
                    mGameManager._coins += coinValue;

                    //Assign it to chest which player clicks on and pass the values
                    GameObject Chest = raycastHit.transform.gameObject;
                    Chest.GetComponent<ChestValue>()._value = coinValue;

                    StartCoroutine(PiggyBankFocus(Chest));
                    
                }
            }
        }
    }

    public IEnumerator CameraZoomAndFollowEnergy(GameObject inChest)
    {
        while (true)
        {
            //Destroy(inChest.transform.parent.GetComponent<Animator>());
            inChest.transform.GetChild(inChest.transform.childCount - 2).gameObject.SetActive(true);
            inChest.transform.rotation = Quaternion.identity;
            BackgroundParentRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 100;
            CloudRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 150;
            inChest.transform.parent.GetComponent<Animator>().SetTrigger("isFalling?");
            Vector3 targetPosition = new Vector3(inChest.transform.position.x, inChest.transform.position.y + 10, inChest.transform.position.z - 50);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, CameraFocusSpeed * Time.deltaTime);
            Camera.main.transform.SetParent(inChest.transform);
            inChest.GetComponent<Rigidbody>().velocity = new Vector3(0,-(dropSpeed),0);
            
            yield return null;
        }
    }

    private IEnumerator PiggyBankFocus(GameObject inChest)
    {
        while (true)
        {
            Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 1.5f, Camera.main.transform.position.z + 5);
            inChest.transform.position = Vector3.Lerp(inChest.transform.position, targetPosition, ItemFocusSpeed * Time.deltaTime);

            transparentBackgroundPlane.GetComponent<Renderer>().material.SetFloat("_alpha", 0.8f);
            
            yield return null;
        }
    }

    void UpdateBackground()
    {
        
    }

    /// <summary>
    /// Loads back to active level
    /// </summary>
    public void BackToMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); //Need to change it from zero to some other value. Will be doing that when scene save system is Done.
    }
}


//Coin
//CoinListShuffle(mCoinProbability._coins);

//Assign other values from Coin's Array to Chest Value apart from the Value that we got above
//for (int i = 0; i < mCoinChests.Count; i++)
//{
//    if (mCoinChests[i].GetComponent<ChestValue>()._value == coinValue)
//    {
//        mCoinChests.Remove(mCoinChests[i]);
//    }
//    if (mCoinProbability._coins[i]._coinAmount == coinValue)
//    {
//        mCoinProbability._coins.Remove(mCoinProbability._coins[i]);
//    }
//    mCoinChests[i].GetComponent<ChestValue>()._value = mCoinProbability._coins[i]._coinAmount;
//}
//Invoke(nameof(BackToMainScene), 1f);
//Camera.main.transform.SetParent(inChest.transform);
//inChest.GetComponent<Rigidbody>().velocity = new Vector3(0, dropSpeed * Time.deltaTime, 0);
//Destroy(inChest.transform.parent.GetComponent<Animator>());

//inChest.transform.rotation = Quaternion.identity;


//Energy
//Provides remaining energy to other boxes except the one that was selected by probability

//if (mEnergyChests[i].GetComponent<ChestValue>()._value == energyValue)
//{
//    mEnergyChests.Remove(mEnergyChests[i]);
//}
//if (mEnergyProbability._energies[i]._energyAmount == energyValue)
//{
//    mEnergyProbability._energies.Remove(mEnergyProbability._energies[i]);
//}
//mEnergyChests[i].GetComponent<ChestValue>()._value = mEnergyProbability._energies[i]._energyAmount;

/// <summary>
/// Below both the methods are done for natural looking purposes after clicking it shuffles the list randomly which sometimes makes the higher values to be beside what
/// we clicked creating the anticipation factor.
/// </summary>
/// <param name="inEnergyList"></param>
//private void EnergyListShuffle(List<Energy> inEnergyList)
//{
//    for (int i = 0; i < inEnergyList.Count; i++)
//    {
//        Energy temp = inEnergyList[i];
//        int randomIndex = Random.Range(i, inEnergyList.Count);
//        inEnergyList[i] = inEnergyList[randomIndex];
//        inEnergyList[randomIndex] = temp;
//    }
//}
//private void CoinListShuffle(List<Coins> inCoinList)
//{
//    for (int i = 0; i < inCoinList.Count; i++)
//    {
//        Coins temp = inCoinList[i];
//        int randomIndex = Random.Range(i, inCoinList.Count);
//        inCoinList[i] = inCoinList[randomIndex];
//        inCoinList[randomIndex] = temp;
//    }
//}