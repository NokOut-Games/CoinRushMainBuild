using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    public Transform _CameraParent;
    private float mInitialPositionX;
    private float mChangedPositionX;
    private float mInitialPositionY;
    private float mChangedPositionY;

    [Header("Horizontal Panning")]
    [SerializeField] private Transform mTargetToRotateAround;

    [Header("Vertical Zooming")]
    [SerializeField] private float mZoomSpeed;
    private float mCameraNearBound = -40;
    private float mCameraFarBound = -90;


    [Header("Camera Views")]
    private Transform _currentView;

    private Vector3 initialVector = Vector3.forward;
    private Vector2 _MouseDownPosition = Vector2.zero;

    public Transform[] _views;
    public float _transitionSpeed;
    public RectTransform _DrawButtonRectTransform;

    public bool _DrawButtonClicked = false;
    public bool _CameraFreeRoam = true;

    public int _RotationLimit = 30;

    private void Start()
    {
        _CameraParent = transform.parent;

        if (mTargetToRotateAround != null)
        {
            initialVector = transform.position - mTargetToRotateAround.position;
            initialVector.y = 0;
        }
    }

    public void DrawButtonClicked()
    {
        _DrawButtonClicked = true;
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        _CameraFreeRoam = false;
    }

    public void SetCameraFreeRoam()
    {
        _CameraFreeRoam = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _MouseDownPosition = Input.mousePosition;
            Vector2 localMousePosition = _DrawButtonRectTransform.InverseTransformPoint(Input.mousePosition);
            if (!_DrawButtonRectTransform.rect.Contains(localMousePosition))
            {
                _DrawButtonClicked = false;
                Invoke("SetCameraFreeRoam", 0.11f);
            }
        }

        if ( _CameraFreeRoam && !Input.GetMouseButton(0))
        {
            _CameraFreeRoam = false;
        }

        if (_DrawButtonClicked)
        {
            _currentView = _views[1];

            _CameraParent.position = Vector3.Lerp(_CameraParent.position, _currentView.position, 0.1f);// Time.deltaTime * _transitionSpeed);

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, 0.1f),// Time.deltaTime * _transitionSpeed),
                Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, 0.1f),//Time.deltaTime * _transitionSpeed),
                Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, 0.1f));//Time.deltaTime * _transitionSpeed));

            _CameraParent.eulerAngles = currentAngle;
        }
        else 
        {
            if (Mathf.Floor(_CameraParent.rotation.eulerAngles.x) != _views[0].rotation.eulerAngles.x)
            {
                _currentView = _views[0];
                _CameraParent.position = Vector3.Lerp(_CameraParent.position, _currentView.position, Time.deltaTime * _transitionSpeed);

                Vector3 currentAngle = new Vector3(
                    Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, Time.deltaTime * _transitionSpeed),
                    Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, Time.deltaTime * _transitionSpeed),
                    Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, Time.deltaTime * _transitionSpeed));

                _CameraParent.eulerAngles = currentAngle;
            }


            HorizontalPanningWithRotation();
            VerticalZooming();
        }
    }

    /// <summary>
    /// Responsible for Making the camera move right & left along with rotation.
    /// 1. We store first touch position as previous position or initial position when we click mouseButtonDown
    /// 2. With mouseButtonDown being true we keep tracking the mouseposition and store it to newPosition and then we take the initial/previous position
    /// and check the differnce and store it in as direction as it says which direction are we moving
    /// </summary>
    private void HorizontalPanningWithRotation()
    {
        if (Input.GetMouseButtonDown(0) )
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

    /// <summary>
    /// We get the input position in y on click.
    /// And keep updating the input.y position as save it to mInitialPosition and keep taking count of whats the changed Position and see if its greater or lesser
    /// and do action accordingly
    /// </summary>
    private void VerticalZooming()
    {
        if (Input.GetMouseButtonDown(0))        {
            mInitialPositionY = Input.mousePosition.y;
        }

        if (_CameraFreeRoam)
        {
            mChangedPositionY = Input.mousePosition.y;

            if (mChangedPositionY == mInitialPositionY)
            {
                return;
            }
            if (mChangedPositionY < mInitialPositionY)
            {
                Zoom(mZoomSpeed * -1f * Time.deltaTime);
            }
            if (mChangedPositionY > mInitialPositionY)
            {
                Zoom(mZoomSpeed * Time.deltaTime);
            }
        }

        //if (!Input.GetMouseButton(0))
        //{
        //    PlayCameraBoundEffect();
        //}
    }
    /// <summary>
    /// Zoom Condition
    /// 
    /// check for camera near and far bounds, check conditions independently
    /// Give a small buffer value to bring in rubber band effect for the camera
    /// Buffervalue being 2
    /// </summary>
    /// <param name="inZoomSpeed"></param>
    private void Zoom(float inZoomSpeed)
    {
        if ((_CameraParent.position.z <= mCameraNearBound + 2 && inZoomSpeed > 0) || (_CameraParent.position.z >= mCameraFarBound - 2 && inZoomSpeed < 0))
        {
            _CameraParent.Translate(inZoomSpeed * _CameraParent.forward);
        }
    }

    /// <summary>
    /// Reset the camera position when touch is released, to set it back to its closest bound, either far or near. 
    /// </summary>
    //public void PlayCameraBoundEffect()
    //{
    //    Vector3 newCameraParentPos = Vector3.zero;

    //    if (_CameraParent.position.z > mCameraNearBound || _CameraParent.position.z < mCameraFarBound)
    //    {

    //        if (Mathf.Abs(mCameraFarBound - _CameraParent.position.z) < Mathf.Abs(mCameraNearBound - _CameraParent.position.z))
    //        {
    //            newCameraParentPos = new Vector3(_CameraParent.position.x, _CameraParent.position.y, mCameraFarBound);
    //        }
    //        else
    //        {
    //            newCameraParentPos = new Vector3(_CameraParent.position.x, _CameraParent.position.y, mCameraNearBound);
    //        }

    //        _CameraParent.position = Vector3.Lerp(_CameraParent.position, newCameraParentPos, 0.1f);
    //    }
    //}
}



