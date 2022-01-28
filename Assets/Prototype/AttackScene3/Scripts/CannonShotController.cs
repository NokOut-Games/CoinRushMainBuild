using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{

    public Rigidbody _bulletPrefab, _bullet;
    public Transform _shotPoint;
    public Transform _TargetTransform;
    public AttackManager _AttackManager;
    //  public Quaternion CameraAttackRotation;
    // public float CameraAttackPositionZ = -665f;
    public Vector3 CannonAttackPosition;
    Quaternion rot;
    public bool fixCameraRot = false;
    public bool Halfwayreached = true;
    public bool ishalfwayreached = true;
    float CannonTargetDistance;
    float BulletSpeed = 0f;
    float deceleration = 125;
    public GameObject shieldPref;

    public Vector3 cameraTargetPos;
    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fixCameraRot)
        {
            Camera.main.transform.LookAt(_TargetTransform);
            if (Vector3.Distance(_bullet.transform.position, _TargetTransform.position) < (CannonTargetDistance * .85))
            {
                Debug.Log("Quaterway reached");
                Debug.Log(CannonTargetDistance / 3);

                //cameraTargetPos = Camera.main.transform.position + Time.deltaTime * BulletSpeed * Vector3.back;

                //Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cameraTargetPos, ref velocity, 0.1f);

                Camera.main.transform.position += Time.fixedDeltaTime * BulletSpeed * Vector3.back;
                BulletSpeed += deceleration * Time.fixedDeltaTime;
                
                Debug.Log(" Speed " + BulletSpeed);

                ////  fixCameraRot = false;
                //if ((Vector3.Distance(_bullet.transform.position, _TargetTransform.position) < (CannonTargetDistance * .5)) && ( Halfwayreached == true))
                //{
                //    Debug.Log("halfway region entered");
                //    Halfwayreached = false;
                //    Debug.Log("Halfway region false");

                //}

            }
            if ((Vector3.Distance(_bullet.transform.position, _TargetTransform.position) < (CannonTargetDistance * .65)) && ishalfwayreached == true)
            {
                Debug.Log("halfway region entered");
                Halfwayreached = false;
                if (_AttackManager._Shield == true)
                {
                    GameObject ShieldPrefab = Instantiate(shieldPref, _TargetTransform.position, Quaternion.identity);
                    Debug.LogError(shieldPref.name + "shield Name");
                }
                ishalfwayreached = false;
                Debug.Log(CannonTargetDistance / 2);
                Debug.Log("Halfway region false");

            }
        }
    }



    /// <summary>
    ///  Calcuate the Projectile  of the Bullet from from Origin to Target
    /// </summary>
    /// <param name="target"></param>
    /// <param name="origin"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //Define 
        Vector3 _distance = target - origin;
        Vector3 _distanceXZ = _distance;
        _distanceXZ.y = 0f;

        //Distance Value

        float sY = _distance.y;
        float sXZ = _distanceXZ.magnitude;

        float Vxz = sXZ / time;
        float Vy = sY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = _distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        Debug.Log("Vector3 value" + result);
        return result;

    }

    /// <summary>
    /// Cannon look at the Target and Instantiate the Bullet Prefab
    /// </summary>
    /// <param name="tran"></param>
    public void AssignPos(Transform tran)
    {
        Debug.LogError("cannon shift " + tran);
        _TargetTransform = tran;
        this.transform.position = new Vector3(_TargetTransform.position.x, CannonAttackPosition.y, CannonAttackPosition.z);
        this.gameObject.SetActive(true);
        Invoke("ShootBullet", 2.5f);
    }

    public void ShootBullet()
    {
        _bullet = Instantiate(_bulletPrefab, _shotPoint.position, Quaternion.identity);
        _bullet.GetComponent<BallLaunch>().target = _TargetTransform;
        Debug.Log("Cannon fired");
        
          Invoke("FollowCamera", .1f);
        // Invoke("DetachCamera",1.5f);
        //Invoke("DestroyBullet", 2.0f);
    }


    public void FollowCamera()
    {
        //  Vector3 velocity = Vector3.zero;

        // rot = Camera.main.transform.rotation;
        Camera.main.transform.parent = _bullet.transform;
        CannonTargetDistance = Vector3.Distance(this.gameObject.transform.position, _TargetTransform.position);
        Debug.Log(this.gameObject.transform.position);
        Debug.Log(_TargetTransform.position);
        Debug.Log(CannonTargetDistance + " display distance values");

        //Quaternion smooth = Quaternion.Lerp(_bullet.transform.rotation, Camera.main.transform.rotation, Time.deltaTime * 1f);
        //Camera.main.transform.rotation = rot;
        //transform.position = Vector3.SmoothDamp(Camera.main.transform.position, _bullet.transform.position, ref velocity, .3f);
      
        //Camera.main.transform.LookAt(_bullet.transform);
        fixCameraRot = true;
        //Camera.main.transform.LookAt(_TargetTransform);
       
       
    }

    
}
