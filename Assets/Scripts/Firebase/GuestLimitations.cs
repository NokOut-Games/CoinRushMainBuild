using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestLimitations : MonoBehaviour
{
    public GameObject OpenCardButton;
    //public GameObject GuestUpgradeButton;

    // Start is called before the first frame update
    void Start()
    {
        if(FirebaseManager.Instance.userTitle == "Guest Users")
        {
            OpenCardButton.SetActive(false);
            //GuestUpgradeButton.SetActive(true);
        }
    }

}
