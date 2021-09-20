using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera mCamRef;
    private float mInitialPosition;
    private float mChangedPosition;

    [Header("Horizontal Panning")]
    [SerializeField] private Transform mTargetToRotateAround;

    [Header("Vertical Zomming")]
    [SerializeField] private float mZoomSpeed;
    
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
        if (Input.GetMouseButtonDown(0))
        {
            mInitialPosition = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            mChangedPosition = Input.mousePosition.x;
            float difference = mChangedPosition - mInitialPosition;
            if (mChangedPosition < mInitialPosition || mChangedPosition > mInitialPosition)
            {
                Rotation(-difference);
            }
        }
    }
    private void Rotation(float inDifference)
    {
        if (mCamRef.transform.rotation.y > -0.5f && mCamRef.transform.rotation.y < 0.5f)
        {
            transform.RotateAround(mTargetToRotateAround.position, transform.up, inDifference * Time.deltaTime);
        }
    }

    /// <summary>
    /// We get get the input position in y on click.
    /// And keep updating the input.y position as save it to 
    /// </summary>
    private void VerticalZooming()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mInitialPosition = Input.mousePosition.y;
        }
        
        if (Input.GetMouseButton(0))
        {
            mChangedPosition = Input.mousePosition.y;

            #region "ClampZoom"
            float cameraZ = mCamRef.transform.position.z;
            cameraZ = Mathf.Clamp(cameraZ, -4f, -25f);
            #endregion

            if (mChangedPosition == mInitialPosition)
            {
                return;
            }
            if (mChangedPosition < mInitialPosition)
            {
                Zoom((mZoomSpeed * -1f) * Time.deltaTime , cameraZ);
            }
            if (mChangedPosition > mInitialPosition)
            {
                Zoom(mZoomSpeed * Time.deltaTime , cameraZ);
            }
        }
    }
    private void Zoom(float inZoomSpeed , float inCameraZ)
    {
        if (mCamRef.transform.position.z < -4f && mCamRef.transform.position.z > -25f)
        {
            transform.Translate(inCameraZ * inZoomSpeed * transform.forward);
        }
    }
}

//void residue()
//{
//    float cameraZ = mCamRef.transform.position.z;
//    float cameraRotY = mCamRef.transform.rotation.y;

//    cameraZ = Mathf.Clamp(cameraZ, -4f, -20f);
//    cameraRotY = Mathf.Clamp(cameraRotY, -60f, 60f);
//}