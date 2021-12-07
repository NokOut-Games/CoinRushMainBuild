using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteAlways]
public class TestScript : MonoBehaviour
{
    public GameObject cube;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject refgO = this.gameObject.transform.Find("Crate_Top").gameObject;
            Debug.Log(refgO);
        }
    }
}