using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCameraController : MonoBehaviour
{
    public float rotationSpeed = 10;
    public int _RotationLimit = 30;
    public bool _CameraFreeRoam = true;
    [SerializeField] private float mHorizontalPanSpeed;
    public float _CameraLeftBound = 0;
    public float _CameraRightBound = 0;
    Vector3 mPreTouchMovementVector;

    void Update()
    {
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

        /*private void HorizontalPanningWithRotation()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mInitialPositionX = Input.mousePosition.x;
            }

            if (_CameraFreeRoam)
            {
                mChangedPositionX = Input.mousePosition.x;
                float rotateDegrees = 0f;
                if (mChangedPositionX < mInitialPositionX)
                {
                    rotateDegrees += 50f * Time.deltaTime;
                }
                if (mChangedPositionX > mInitialPositionX)
                {
                    rotateDegrees -= 50f * Time.deltaTime;
                }
                Vector3 currentVector = transform.position - mTargetToRotateAround.position;
                currentVector.y = 0;
                float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).y > 0 ? 1 : -1);
                float newAngle = Mathf.Clamp(angleBetween + rotateDegrees, -_RotationLimit, _RotationLimit);
                rotateDegrees = newAngle - angleBetween;

                transform.RotateAround(mTargetToRotateAround.position, Vector3.up, rotateDegrees);

                mInitialPositionX = mChangedPositionX;
            }
        }

        public void HorizontalPanning()
        {
            //float panSpeed = 0;

            if (Input.GetMouseButtonDown(0))
            {
                mInitialPositionX = Input.mousePosition.x;
            }

            if (_CameraFreeRoam)
            {
                mChangedPositionX = Input.mousePosition.x;

                if (mChangedPositionX == mInitialPositionX)
                {
                    return;
                }
                if (mChangedPositionX < mInitialPositionX - 100f)
                {
                    //panSpeed = mZoomSpeed * -1f * Time.deltaTime;
                    Pan(mHorizontalPanSpeed * Time.deltaTime);
                }
                if (mChangedPositionX > mInitialPositionX + 100f)
                {
                    //panSpeed = mZoomSpeed * Time.deltaTime;
                    Pan(mHorizontalPanSpeed * -1f * Time.deltaTime);
                }
            }

            if (!Input.GetMouseButton(0))
            {
                //PlayCameraBoundEffectX();
                // PlayCameraBoundEffect(_CameraParent.position.x, _CameraRightBound, _CameraLeftBound, new Vector3(_CameraLeftBound, _CameraParent.position.y, _CameraParent.position.z), new Vector3(_CameraRightBound, _CameraParent.position.y, _CameraParent.position.z));
            }

            //if ((transform.position.x <= _CameraRightBound + 30 && panSpeed > 0) || (transform.position.x >= _CameraLeftBound - 2 && panSpeed < 0))
            //{
            //    transform.Translate(panSpeed * transform.right);
            //}

        }

        private void Pan(float inPanSpeed) //New Addition
        {
            if ((transform.position.x <= _CameraRightBound + 30 && inPanSpeed > 0) || (transform.position.x >= _CameraLeftBound - 30 && inPanSpeed < 0))
            {
                transform.Translate(inPanSpeed * transform.right);
            }
        }*/

    }