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


    [Header("Other References: ")]
    [SerializeField] private GameManager mGameManager;
    [SerializeField] Vector3 CameraOffset;
    [SerializeField] Transform ground;
    [SerializeField] float groundOffset;
    [SerializeField] GameObject olCloud;
    [SerializeField] float cameraXOffset;

    // Start is called before the first frame update
    void Start()
    {
        EnergyFalling = true;
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnMouseDown()
    {
        int energyValue = mEnergyProbability.DisplayTheFinalElementBasedOnRandomValueGenerated();

        //Changing the Energy value in Gamemanager
        mGameManager._energy += energyValue;

        //Assign it to chest which player clicks on and pass the values
        GameObject Chest = this.gameObject;
        Chest.GetComponent<ChestValue>()._value = energyValue;

        energyCoroutine = CameraZoomAndFollowEnergy(Chest);
        StartCoroutine(energyCoroutine);

        ground.position = new Vector3(groundOffset, ground.position.y, ground.position.z);
        olCloud.SetActive(false);
        //Destroy other chests Colliders except the ones clicked
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
        //Destroy the parachute
       // Destroy(inChest.transform.Find("Parachut").gameObject , 1f);
        while (true)
        {
            //Make the chest rotation to zero
           // inChest.transform.rotation = Quaternion.identity;


            if (EnergyFalling)
            {
                //Make the camera to zoom-in
                Vector3 targetPosition = inChest.transform.position + CameraOffset- new Vector3(0,0, cameraXOffset);
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, CameraFocusSpeed * Time.deltaTime);
                Camera.main.transform.SetParent(inChest.transform);
                
                //Change the Animator
                inChest.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isFalling?");

                //Make the camera fall down
                //inChest.transform.position += Vector3.down * dropSpeed * Time.fixedDeltaTime;
                dropSpeed += gainedAcceleration * Time.fixedDeltaTime ;
                inChest.GetComponent<Rigidbody>().AddForce(Vector3.down * dropSpeed * Time.deltaTime);


                //Play the falling particle Effect
                inChest.transform.GetChild(0).Find("Wind_Effect").gameObject.SetActive(true);
            }

            //Change the Scrolling Speed to make it look faster
            //BackgroundParentRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 100;
            CloudBackRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 150;
            CloudFrontRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 200;

            yield return null;

        }
    }
}


