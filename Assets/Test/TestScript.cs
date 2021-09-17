using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [Header("Horizontal Panning")]
    [SerializeField] private Camera mCamRef;
    [SerializeField] private Transform mTargetToRotateAround;
    [SerializeField] private Vector3 mDistanceToTarget;
    private Vector3 mInitialPosition;

    [Header("Vertical Zomming")]
    [SerializeField] private float zoomSpeed;

    Vector3 intialPosition;
    public bool canMove = false;
    private void Update()
    {
        Debug.Log( mCamRef.ScreenToViewportPoint(Input.mousePosition));
        //HorizontalPanningWithRotation();
        VerticalZooming();
    }

    /// <summary>
    /// Responsible for Making the camera move right & left along with rotation.
    /// 1. We store first touch position as previous position or initial position when we click mouseButtonDown
    /// 2. With mouseButtonDown being true we keep tracking the mouseposition and store it to newPosition and then we take the initial/previous position
    /// and check the differnce and store it in as direction as it says which direction are we moving
    /// </summary>
    //private void HorizontalPanningWithRotation()
    //{
    //    //First - Get the Initial Position
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        mInitialPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
    //        //Debug.Log(mInitialPosition);
    //    }

    //    //Second - the difference amount and change in x
    //    if (Input.GetMouseButton(0))
    //    {
    //        Vector3 newPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
    //        Vector3 difference = mInitialPosition - newPosition;

    //        float rotationAroundYAxis = -difference.x * 120;
    //        mCamRef.transform.position = mTargetToRotateAround.position;

    //        mCamRef.transform.Rotate(Vector3.up, rotationAroundYAxis, Space.World);
    //        mCamRef.transform.Translate(new Vector3(mDistanceToTarget.x, mDistanceToTarget.y, -mDistanceToTarget.z));
    //        mInitialPosition = newPosition;
    //    }
    //}

    private void VerticalZooming()
    {
        Vector3 initialPosition = new Vector3();
        Vector3 newPosition = new Vector3();

        if (Input.GetMouseButtonDown(0))
        {
            initialPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
        }
        
        if (Input.GetMouseButton(0))
        {
            newPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);   

            if (newPosition.y < initialPosition.y)
            {
                Zoom((zoomSpeed * -1f) * Time.deltaTime);
                Debug.Log(zoomSpeed);
            }
            if (newPosition.y > initialPosition.y)
            {
                Zoom(zoomSpeed * Time.deltaTime);
                Debug.Log(zoomSpeed);
            }
        }

        if(newPosition.y < initialPosition.y)
        {
            Debug.Log("Y is Lower");
        }
        if (newPosition.y > initialPosition.y)
        {
            Debug.Log("Y is Higher");
        }
    }

    private void Zoom(float inZoomSpeed)
    {
        transform.Translate(mCamRef.transform.position.z * inZoomSpeed * transform.forward);
    }
}

