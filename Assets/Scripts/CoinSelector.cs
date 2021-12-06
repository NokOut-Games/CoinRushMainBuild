using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinSelector : MonoBehaviour
{
    [SerializeField] private CoinProbability mCoinProbability;
    [SerializeField] private List<GameObject> mCoinChests;
    [SerializeField] private float ItemFocusSpeed;
    [SerializeField] private GameObject transparentBackgroundPlane;

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
        //rewardText.text = coinValue.ToString();
        //RewardDisplayPanel.SetActive(true);

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
        float t = 0;
        while (t <= 1.0)
        {
            t += Time.deltaTime / easing;

            Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - .25f, Camera.main.transform.position.z + 5);
            inChest.transform.position = Vector3.Lerp(inChest.transform.position, targetPosition, ItemFocusSpeed * Time.deltaTime);

            transparentBackgroundPlane.GetComponent<Renderer>().material.SetFloat("_alpha", v = Mathf.Lerp(v, 0.8f, Mathf.SmoothStep(0f, 1f, t)));
            Invoke("DisplayRewardAndInvokeScene", 1.5f);
            yield return null;
        }
    }

    void DisplayRewardAndInvokeScene()
    {
        RewardDisplayPanel.SetActive(true);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
