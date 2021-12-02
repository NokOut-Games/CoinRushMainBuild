using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxReactor : MonoBehaviour
{
    [SerializeField] private GameObject RewardDisplayPanel;
    [SerializeField] private TextMeshProUGUI rewardText;

    [SerializeField] private GameObject EnergyCanSmall;
    [SerializeField] private GameObject EnergyCanMedium;
    [SerializeField] private GameObject EnergyCanLarge;

    private GameObject canSpawnLocation;

    ChestOpener Opener;

    [SerializeField] private float RewardDisplayInvokeTime;

    private void Start()
    {
        Opener = Camera.main.GetComponent<ChestOpener>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "EnergyChestBox")
        {
            //Camera.main.GetComponent<ChestOpener>().EnergyFalling = false;
            //Stopping Coroutine Just in Case
            //StopCoroutine(Opener.EnergyFallingCoroutine);

            //Disabling the particle Effect
            //Destroy(other.gameObject.transform.Find("WindTrialEffect").gameObject);
            other.transform.GetChild(other.transform.childCount - 2).gameObject.SetActive(false);

            //Getting the can spawn location to spawn the can
            //canSpawnLocation = other.transform.GetChild(other.transform.childCount - 3).gameObject;
            //canSpawnLocation = other.gameObject.transform.Find("CanSpawnPoint").gameObject;
            canSpawnLocation = other.transform.GetChild(other.transform.childCount - 4).gameObject;

            //Getting references
            Animator crateAnimRef = other.transform.parent.gameObject.GetComponent<Animator>();
            ChestValue crateValueRef = other.gameObject.GetComponent<ChestValue>();

            //Instantiating cans based on chest value
            switch (other.gameObject.GetComponent<ChestValue>()._value)
            {
                case 10:
                    GameObject can = Instantiate(EnergyCanSmall, canSpawnLocation.transform.position, Quaternion.identity);
                    //can.GetComponent<Rigidbody>().isKinematic = true;
                    crateAnimRef.SetTrigger("isBreaking?");
                    rewardText.text = crateValueRef._value.ToString() + " Energies";
                    break;
                case 25:
                    Instantiate(EnergyCanMedium, canSpawnLocation.transform.position, Quaternion.identity);
                    crateAnimRef.SetTrigger("isBreaking?");
                    rewardText.text = crateValueRef._value.ToString() + " Energies";
                    break;
                case 100:
                    Instantiate(EnergyCanLarge, canSpawnLocation.transform.position, Quaternion.identity);
                    crateAnimRef.SetTrigger("isBreaking?");
                    rewardText.text = crateValueRef._value.ToString() + " Energies";
                    break;
            }
            
            //Starting to activate Reward Panel
            Invoke("ActiveRewardPanel", 3f);
        }
    }

    void ActiveRewardPanel()
    {
        RewardDisplayPanel.SetActive(true);
    }
}



//other.transform.parent.GetComponent<Animator>().SetTrigger("isBreaking?");
//rewardText.text = other.gameObject.GetComponent<ChestValue>()._value.ToString() + "Energy";