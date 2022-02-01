using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardParticle : MonoBehaviour
{
    public Transform parent;
    bool isMove;
    private void Update()
    {
        if (isMove)
        {
            transform.position = parent.transform.position;
            transform.rotation = parent.transform.rotation;
        }
        
    }


    public void _Init(Transform parent)
    {
        this.parent = parent;
        isMove = true;
    }
}
