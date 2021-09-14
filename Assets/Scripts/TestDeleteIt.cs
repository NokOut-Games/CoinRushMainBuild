using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeleteIt : MonoBehaviour
{

    Vector3 hitposition = Vector3.zero;
    Vector3 currentposition = Vector3.zero;
    Vector3 cameraposition = Vector3.zero;
   

   

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitposition = Input.mousePosition;
            cameraposition = transform.position;

        }
        if (Input.GetMouseButton(0))
        {
            currentposition = Input.mousePosition;
            LeftMouseDrag();
        }
    }

    void LeftMouseDrag()
    {
        
        currentposition.z = hitposition.z = cameraposition.y;

         
        Vector3 direction = Camera.main.ScreenToWorldPoint(currentposition) - Camera.main.ScreenToWorldPoint(hitposition);

        direction = direction * -1;

        Vector3 position = cameraposition + direction;

        transform.position = position;
    }

}

