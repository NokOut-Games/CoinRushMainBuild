using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCameraController : MonoBehaviour
{
    public float rotationSpeed = 10;
    public float _CameraLeftBound = 0;
    public float _CameraRightBound = 0;
    Vector3 mPreTouchMovementVector;
    public bool _CameraFreeRoam;

    void Update()
    {
        if (!_CameraFreeRoam) return;
             HandleTouch();
    }

    public void staticMovement()
    {
        Vector3 rotation = transform.eulerAngles;

        rotation.y += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.eulerAngles = rotation;

    }
    bool IsBoundary(Touch touch)
    {
        if (transform.position.x > _CameraRightBound + 60 && -touch.deltaPosition.x > 0) return true;
        else if (transform.position.x < _CameraLeftBound - 60 && -touch.deltaPosition.x < 0) return true;
        else return false;
    }
    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                //Debug.Log(touchMovedTime);
                if (IsBoundary(touch)) return;
                mPreTouchMovementVector = new Vector3(-touch.deltaPosition.x, 0, 0);
                transform.position = transform.position + mPreTouchMovementVector * 0.4f;
            }
        }
        else
        {
            if (transform.position.x > _CameraRightBound)
            {
                transform.Translate(Vector3.left * 5f, Space.World);
            }
            else if (transform.position.x < _CameraLeftBound)
            {
                transform.Translate(Vector3.right * 5f, Space.World);
            }

        }
    }
}