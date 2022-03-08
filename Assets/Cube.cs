using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.RemoteConfig;

public class Cube : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttribute { }

    public bool CubeIsRed = false;

    public Material blue, red;
    public MeshRenderer rend;

    //private void Awake()
    //{
    //    ConfigManager.FetchCompleted += SetColor;
    //    ConfigManager.FetchConfigs<userAttributes, appAttribute>(new userAttributes(), new appAttribute());
    //}

    //void SetColor(ConfigResponse response)
    //{
    //    CubeIsRed = ConfigManager.appConfig.GetBool("CubeIsRed");

    //    if(CubeIsRed)
    //    {
    //        rend.material = red;
    //    }
    //    else
    //    {
    //        rend.material = blue;
    //    }
    //}

    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        ConfigManager.FetchConfigs<userAttributes, appAttribute>(new userAttributes(), new appAttribute());
    //    }
    //}

    //private void OnDestroy()
    //{
    //    ConfigManager.FetchCompleted -= SetColor;
    //}
}
