using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCards : MonoBehaviour
{
    public List<Transform> _OpenCardTransformPoint;
    private void Awake()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            _OpenCardTransformPoint.Add(this.gameObject.transform.GetChild(i).transform);
        }
    }
}
