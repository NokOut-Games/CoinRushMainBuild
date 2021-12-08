using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CoinSelector : MonoBehaviour
{
    [SerializeField] private CoinProbability mCoinProbability;
    [SerializeField] private List<GameObject> mCoinChests;
    [SerializeField] private float ItemFocusSpeed;
    [SerializeField] private GameObject transparentBackgroundPlane;
    [SerializeField] private Transform HammerSpawnPoint;
    [SerializeField] private GameObject HammerPrefab;


    [Header("Other References: ")]
    [SerializeField] private GameManager mGameManager;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private GameObject RewardDisplayPanel;

    float v = 0.0f;
    public float easing = 1f;

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        int coinValue = mCoinProbability.DisplayTheFinalElementBasedOnRandomValueGenerated();
        
        //Changing the Coinvalue
        rewardText.text = coinValue.ToString();
        mGameManager._coins += coinValue;

        //Assign it to chest which player clicks on and pass the values
        GameObject Chest = this.gameObject;
        Chest.GetComponent<ChestValue>()._value = coinValue;

        StartCoroutine(PiggyBankFocus(Chest));
    }

    private IEnumerator PiggyBankFocus(GameObject inChest)
    {
        inChest.GetComponent<Animator>().SetTrigger("isSelected?");
        Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 20f, Camera.main.transform.position.z + 90);
        transparentBackgroundPlane.GetComponent<Renderer>().material.DOFloat(0.8f, "_alpha", 3);
        inChest.transform.DOMove(targetPosition, 1, false).OnComplete(()=> Instantiate(HammerPrefab, HammerSpawnPoint.position, HammerPrefab.transform.rotation));
        
        //Hammer.GetComponent<Animator>().

        yield return new WaitForSeconds(1);  //Yield to Wait for the Hammer
        inChest.GetComponent<Animator>().SetTrigger("isBreaking?");
    }

    void DisplayRewardAndInvokeScene()
    {
        RewardDisplayPanel.SetActive(true);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}


//void Residue()
//{
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