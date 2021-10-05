using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float rotateSpeed = 20.0f;
    public float angleMax = 30.0f;
    public Transform target;

    private Vector3 initialVector = Vector3.forward;

    // Use this for initialization
    void Start()
    {

        if (target != null)
        {
            initialVector = transform.position - target.position;
            initialVector.y = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            float rotateDegrees = 0f;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotateDegrees += rotateSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotateDegrees -= rotateSpeed * Time.deltaTime;
            }

            Vector3 currentVector = transform.position - target.position;
            currentVector.y = 0;
            float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).y > 0 ? 1 : -1);
            float newAngle = Mathf.Clamp(angleBetween + rotateDegrees, -angleMax, angleMax);
            rotateDegrees = newAngle - angleBetween;

            transform.RotateAround(target.position, Vector3.up, rotateDegrees);
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