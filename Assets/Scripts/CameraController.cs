using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    private float mInitialPosition;
    private float mChangedPosition;

    [Header("Horizontal Panning")]
    [SerializeField] private Transform mTargetToRotateAround;

    [Header("Vertical Zomming")]
    [SerializeField] private float mZoomSpeed;

    [Header("Camera Views")]
    private Transform _currentView;
    
    private Vector3 initialVector = Vector3.forward;

    public Transform[] _views;
    public float _transitionSpeed;

    public bool _DrawButtonClicked = false;

    private void Start()
    {
        if (mTargetToRotateAround != null)
        {
            initialVector = transform.position - mTargetToRotateAround.position;
            initialVector.y = 0;
        }
    }

    private void Update()
    {
        HorizontalPanningWithRotation();
        //VerticalZooming();
        //Debug.Log(transform.eulerAngles.y);

        if (_DrawButtonClicked)
        {
            _currentView = _views[1];

            transform.position = Vector3.Lerp(transform.position, _currentView.position, Time.deltaTime * _transitionSpeed);
            //Lerp position

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(transform.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, Time.deltaTime * _transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, Time.deltaTime * _transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, Time.deltaTime * _transitionSpeed));

            transform.eulerAngles = currentAngle;
        }
        else
        {
            _currentView = _views[0];
            transform.position = Vector3.Lerp(transform.position, _currentView.position, Time.deltaTime * _transitionSpeed);
            //Lerp position

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(transform.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, Time.deltaTime * _transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, Time.deltaTime * _transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, Time.deltaTime * _transitionSpeed));

            transform.eulerAngles = currentAngle;
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
        if (Input.GetMouseButtonDown(0))
        {
            _DrawButtonClicked = false;
            mInitialPosition = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            mChangedPosition = Input.mousePosition.x;
            //float difference = mChangedPosition - mInitialPosition;
            float rotateDegrees = 0f;
            if (mChangedPosition < mInitialPosition)
            {
                //Rotation(-difference);
                rotateDegrees += 100f * Time.deltaTime;
            }
            if (mChangedPosition > mInitialPosition)
            {
                rotateDegrees -= 100f * Time.deltaTime;
            }
            Vector3 currentVector = transform.position - mTargetToRotateAround.position;
            currentVector.y = 0;
            float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).y > 0 ? 1 : -1);
            float newAngle = Mathf.Clamp(angleBetween + rotateDegrees, -60, 60);
            rotateDegrees = newAngle - angleBetween;

            transform.RotateAround(mTargetToRotateAround.position, Vector3.up, rotateDegrees);
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

            if (mChangedPosition == mInitialPosition)
            {
                return;
            }
            if (mChangedPosition < mInitialPosition)
            {
                Zoom(mZoomSpeed * -1f * Time.deltaTime);
            }
            if (mChangedPosition > mInitialPosition)
            {
                Zoom(mZoomSpeed * Time.deltaTime);
            }
        }
    }
    /// <summary>
    /// Zoom Condition
    /// </summary>
    /// <param name="inZoomSpeed"></param>
    private void Zoom(float inZoomSpeed)
    {
        if (transform.position.z <= -40f && transform.position.z >= -90f)
        {
            transform.Translate(inZoomSpeed * transform.forward);
        }
    }
}



