using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySelector : MonoBehaviour
{
    [Header("Energy Attributes: ")]
    [SerializeField] private EnergyProbability mEnergyProbability;
    [SerializeField] private List<GameObject> mEnergyChests;
    [SerializeField] private float CameraFocusSpeed;
    [SerializeField] private float dropSpeed;


    [SerializeField] private GameObject BackgroundParentRef;
    [SerializeField] private GameObject CloudRef;
    public bool EnergyFalling = true;

    // Start is called before the first frame update
    void Start()
    {
        EnergyFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        int energyValue = mEnergyProbability.DisplayTheFinalElementBasedOnRandomValueGenerated();

        //Changing the Energy value in Gamemanager
        //mGameManager._energy += energyValue;

        //Assign it to chest which player clicks on and pass the values
        GameObject Chest = this.gameObject;
        Chest.GetComponent<ChestValue>()._value = energyValue;

        
        StartCoroutine(CameraZoomAndFollowEnergy(Chest));

        //Destroy other chests except the ones clicked
        for (int i = 0; i < mEnergyChests.Count; i++)
        {
            if (mEnergyChests[i].transform.GetChild(0).name != Chest.name)
            {
                //mEnergyChests[i].GetComponent<BoxCollider>().enabled = false;
                Destroy(mEnergyChests[i].transform.gameObject, 2f);
            }
            else
            {
                continue;
            }
        }
    }

    public IEnumerator CameraZoomAndFollowEnergy(GameObject inChest)
    {
        while (true)
        {
            //Make the chest rotation to zero
            inChest.transform.rotation = Quaternion.identity;

            //Make the camera to zoom-in
            Vector3 targetPosition = new Vector3(inChest.transform.position.x, inChest.transform.position.y + 10, inChest.transform.position.z - 50);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, CameraFocusSpeed * Time.deltaTime);
            Camera.main.transform.SetParent(inChest.transform);



            if (EnergyFalling)
            {
                //Change the Animator
                inChest.transform.parent.GetComponent<Animator>().SetTrigger("isFalling?");

                //Make the camera fall down
                inChest.transform.position += Vector3.down * dropSpeed * Time.deltaTime;

                //Play the falling particle Effect
                inChest.transform.GetChild(inChest.transform.childCount - 2).gameObject.SetActive(true);
            }

            //Change the Scrolling Speed to make it look faster
            BackgroundParentRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 100;
            CloudRef.GetComponent<BackgroundScrolling>().mScrollSpeed = 150;

            yield return null;
        }
    }
}


