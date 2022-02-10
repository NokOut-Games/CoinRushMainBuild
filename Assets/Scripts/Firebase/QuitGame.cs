using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quittinggame()
    {
        FirebaseManager.Instance.WritePlayerDataToFirebase();
        FirebaseManager.Instance.WriteBuildingDataToFirebase();
        FirebaseManager.Instance.WriteCardDataToFirebase();
        Invoke("AppQuit", 1f);
    }

    public void AppQuit()
    {
        Debug.Log("Application is Quitting");
        FirebaseManager.Instance.auth.SignOut();
    }
}
