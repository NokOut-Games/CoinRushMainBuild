using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    [SerializeField] float mSlideSpeed= 0.005f;
    [SerializeField] float mStartBoundary;
    public float _EndBoundary;
    [SerializeField] float mBounceDistance;
    [SerializeField] float mLearpSpeed;
    void Update()
    {
       
        if (Input.touchCount > 0/*&& !GameManager.Instance.isInTutorial*/)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved && transform.position.z < _EndBoundary + mBounceDistance && transform.position.z > mStartBoundary - mBounceDistance)
            {
                if (IsBound(touch)) return;
                transform.position = transform.position + new Vector3(0, -touch.deltaPosition.y, 0) * mSlideSpeed;

            }
        }
        else
        {
            if (mStartBoundary < _EndBoundary)
            {
                if (transform.position.y > _EndBoundary)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position - (Vector3.up* mBounceDistance), mLearpSpeed * Time.deltaTime);
                }
                else if (transform.position.y < mStartBoundary)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3.up * mBounceDistance), mLearpSpeed * Time.deltaTime);
                }
            }
        }
    }

    bool IsBound(Touch touch)
    {
        /*if (transform.position.z > _EndBoundary + mBounceDistance && -touch.deltaPosition.y > 0) return true;
        else if (transform.position.z < mStartBoundary - mBounceDistance && -touch.deltaPosition.y < 0) return true;
        else*/ if (transform.position.y > _EndBoundary + mBounceDistance && -touch.deltaPosition.y > 0) return true;
        else if (transform.position.y < mStartBoundary - mBounceDistance && -touch.deltaPosition.y < 0) return true;
        else return false;
    }
}
