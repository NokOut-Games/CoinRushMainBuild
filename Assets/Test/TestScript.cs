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
            cube.GetComponent<Rigidbody>().AddForce(Vector3.up * 750);
        }
    }
}