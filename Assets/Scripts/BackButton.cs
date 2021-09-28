using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void BackButtonS()
    {
        SceneManager.LoadScene(0);
    }
}
