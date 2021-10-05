using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Build : MonoBehaviour
{
    public GameObject buildcardpanel;
    private void Start()
    {
        GetComponent<UI_Build>();
    }
    public void Build()
    {
        buildcardpanel.SetActive(true);
    }
    public void BackInBuildPanel()
    {
        buildcardpanel.SetActive(false);
    }
}
