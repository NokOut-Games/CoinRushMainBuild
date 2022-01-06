using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitgame : MonoBehaviour
{
    public void Quittinggame()
    {
        FirebaseManager.Instance.WritePlayerDataToFirebase();
        FirebaseManager.Instance.WriteBuildingDataToFirebase();
        FirebaseManager.Instance.CalculateLogOutTime();
        Invoke("AppQuit", 1f);
    }

    private void AppQuit()
    {
        Application.Quit();
    }
}
