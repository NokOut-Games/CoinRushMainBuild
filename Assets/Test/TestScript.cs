using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Camera mCamRef;
    [SerializeField] private Transform mTargetToRotateAround;
    [SerializeField] private Vector3 mDistanceToTarget;
    private Vector3 mPreviousPosition;


    Vector3 intialPosition;
    private void Update()
    {
        HorizontalPanningWithRotation();
        //VerticalZooming();
    }

    /// <summary>
    /// Responsible for Making the camera move right & left along with rotation.
    /// 1. We store first touch position as previous position or initial position when we click mouseButtonDown
    /// 2. With mouseButtonDown being true we keep tracking the mouseposition and store it to newPosition and then we take the initial/previous position
    /// and check the differnce and store it in as direction as it says which direction are we moving
    /// </summary>
    private void HorizontalPanningWithRotation()
    {
        //First - Get the Initial Position
        if (Input.GetMouseButtonDown(0))
        {
            mPreviousPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
        }

        //Second - the difference amount and change in x
        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = mPreviousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180;
            mCamRef.transform.position = mTargetToRotateAround.position;
            mCamRef.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
            mCamRef.transform.Translate(new Vector3(mDistanceToTarget.x, mDistanceToTarget.y, -mDistanceToTarget.z));
            mPreviousPosition = newPosition;
        }
    }

    //private void VerticalZooming()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        intialPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        Vector3 newPosition = mCamRef.ScreenToViewportPoint(Input.mousePosition);
    //        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - newPosition);

    //        Vector3 move = pos.y * 5 * transform.forward;
    //        transform.Translate(move, Space.World);
    //        intialPosition = newPosition;
    //    }
    //}
}

