using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestLimitations : MonoBehaviour
{
    public GameObject OpenCardButton;
    //public GameObject GuestUpgradeButton;

    void Start()
    {
       if(FirebaseManager.Instance.userTitle == "Facebook Users")
        {
            OpenCardButton.SetActive(true);
            //GuestUpgradeButton.SetActive(true);
        }
    }

}
