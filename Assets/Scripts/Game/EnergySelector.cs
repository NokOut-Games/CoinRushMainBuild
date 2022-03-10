using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySelector : MonoBehaviour
{
    [Header("Energy Attributes: ")]
    [SerializeField] private EnergyProbability mEnergyProbability;
    [SerializeField] private GameObject[] mEnergyChests;
    [SerializeField] private float CameraFocusSpeed;
    [SerializeField] private float gainedAcceleration;
    [SerializeField] private float dropSpeed;

    [SerializeField] private GameObject CloudBackRef;
    [SerializeField] private GameObject CloudFrontRef;
    public bool EnergyFalling = true;

    public IEnumerator energyCoroutine;

    [SerializeField] Vector3 CameraOffset;
    [SerializeField] Transform ground;
    [SerializeField] float groundOffset;
    [SerializeField] GameObject olCloud;
    [SerializeField] float cameraXOffset;

    // Start is called before the first frame update
    void Start()
    {
        EnergyFalling = true;
    }


    private void OnMouseDown()
    {
      //  int energyValue = mEnergyProbability.DisplayTheFinalElementBasedOnRandomValueGenerated();
        int energyValue =int.Parse(GameManager.Instance.minigameEconomy.EnergyReward[RNG.instance.GetRandom(RNG.instance.EnergySceneProbability)]);
        GameObject Chest = this.gameObject;
        Chest.GetComponent<ChestValue>()._value = energyValue;

        energyCoroutine = CameraZoomAndFollowEnergy(Chest);
        StartCoroutine(energyCoroutine);

        ground.position = new Vector3(groundOffset, ground.position.y, ground.position.z);
        olCloud.SetActive(false);
        for (int i = 0; i < mEnergyChests.Length; i++)
        {
            if (mEnergyChests[i].transform.name != Chest.name)
            {
                Destroy(mEnergyChests[i].transform.GetComponentInChildren<BoxCollider>());
                mEnergyChests[i].SetActive(false);
            }
            else mEnergyChests[i].GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public IEnumerator CameraZoomAndFollowEnergy(GameObject inChest)
    {
        while (true)
        {
            if (EnergyFalling)
            {
                Vector3 targetPosition = inChest.transform.position + CameraOffset- new Vector3(0,0, cameraXOffset);
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, CameraFocusSpeed * Time.deltaTime);
                Camera.main.transform.SetParent(inChest.transform);
                
                inChest.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isFalling?");
        
                dropSpeed += gainedAcceleration * Time.fixedDeltaTime ;
                inChest.GetComponent<Rigidbody>().AddForce(Vector3.down * dropSpeed * Time.deltaTime);
                inChest.transform.GetChild(0).Find("Wind_Effect").gameObject.SetActive(true);
            }

            CloudBackRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 150;
            CloudFrontRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 200;

            yield return null;

        }
    }
}


