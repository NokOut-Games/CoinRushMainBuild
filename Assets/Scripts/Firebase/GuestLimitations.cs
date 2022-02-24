using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestLimitations : MonoBehaviour
{
    public GameObject OpenCardButton;

    private void Update()
    {
        if (FirebaseManager.Instance.userTitle == "Facebook Users")
        {
            OpenCardButton.SetActive(true);
        }
        else
        {
            OpenCardButton.SetActive(false);
        }
    }
}
