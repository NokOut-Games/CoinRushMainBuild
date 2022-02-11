using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestLimitations : MonoBehaviour
{
    public GameObject OpenCardButton;
    //public GameObject GuestUpgradeButton;

    void Start()
    {
       if(FirebaseManager.Instance.userTitle == "Guest Users")
        {
            OpenCardButton.SetActive(false);
            //GuestUpgradeButton.SetActive(true);
        }
    }

}
