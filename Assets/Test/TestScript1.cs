using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
    public float speed = 3.5f;
    public Transform building;

    private float mInitialPosition;
    private float mChangedPosition;

    void Update()
    {
        HorizontalPanning();
    }
    
    void HorizontalPanning()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mInitialPosition = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            mChangedPosition = Input.mousePosition.x;
            float difference = mChangedPosition - mInitialPosition;
            if (mChangedPosition < mInitialPosition)
            {
                // if hower move camera left
                //transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0), difference,Space.World);
                transform.RotateAround(building.transform.position , transform.up , -difference * Time.deltaTime);
            }
            if (mChangedPosition > mInitialPosition)
            {
                // if higher move camera right
                //transform.Rotate(new Vector3(0, +Input.GetAxis("Mouse X"), 0), difference, Space.World);
                transform.RotateAround(building.transform.position, transform.up, -difference * Time.deltaTime);
            }
        }
    }
}
