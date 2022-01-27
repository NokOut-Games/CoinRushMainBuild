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
       
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved && transform.position.z < _EndBoundary + mBounceDistance && transform.position.z > mStartBoundary - mBounceDistance)
                {
                    transform.position = transform.position + new Vector3(0, 0, -touch.deltaPosition.y) * mSlideSpeed;

                }
            }
            else
            {
                if (mStartBoundary < _EndBoundary)
                {
                    if (transform.position.z > _EndBoundary)
                    {
                        transform.position = Vector3.Lerp(transform.position, transform.position - (Vector3.forward * mBounceDistance), mLearpSpeed * Time.deltaTime);
                    }
                    else if (transform.position.z < mStartBoundary)
                    {
                        transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3.forward * mBounceDistance), mLearpSpeed * Time.deltaTime);
                    }
                }
            }
          
       
        

    }
}
