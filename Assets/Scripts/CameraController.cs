using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mCamRef;
    private Vector3 mInitialTouchPosition;

    private void Start()
    {
        mCamRef = Camera.main;
    }
    private void Update()
    {
        //First - Get the Initial Position
        if (Input.GetMouseButtonDown(0))
        {
            mInitialTouchPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
        }

        //Second - the difference amount and change in x
        if (Input.GetMouseButton(0))
        { 
            Vector3 difference = mInitialTouchPosition - mCamRef.ScreenToViewportPoint(Input.mousePosition);
            //mCamRef.transform.position += difference;
            mCamRef.transform.Rotate(new Vector3(1, 0, 0), difference.y * 180f);
            mCamRef.transform.Rotate(new Vector3(0, 1, 0), -difference.x * 180, Space.World);
            //mCamRef.transform.Translate(new Vector3(0, 0, -10));
            mInitialTouchPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
        }

        //zoom around in z-axis

        //Also add a little rotation in end
        
    }
}
