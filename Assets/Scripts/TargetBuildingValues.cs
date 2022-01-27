using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBuildingValues : MonoBehaviour
{

    public Sprite _Sprite;

    public void ChangeBuildingValue()
    {
        this.gameObject.GetComponent<Image>().sprite = _Sprite;
    }


}
