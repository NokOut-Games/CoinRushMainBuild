using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{

    public Rigidbody _bulletPrefab, _bullet;
    public Transform _shotPoint;
    public Transform _TargetTransform;
    public AttackManager _AttackManager;
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
    BallLaunch mBallLaunch;

    void FixedUpdate()
    {
        if (fixCameraRot)
        {
            Camera.main.transform.LookAt(_TargetTransform);
            if (Vector3.Distance(_bullet.transform.position, _TargetTransform.position) < (CannonTargetDistance * .85))
            {
                Camera.main.transform.position += Time.fixedDeltaTime * BulletSpeed * Vector3.back;
                BulletSpeed += deceleration * Time.fixedDeltaTime;
            }
            if ((Vector3.Distance(_bullet.transform.position, _TargetTransform.position) < (CannonTargetDistance * .65)) && ishalfwayreached == true)
            {    
                Halfwayreached = false;
                if (_AttackManager._Shield == true)
                {
                    GameObject ShieldPrefab = Instantiate(shieldPref, _TargetTransform.position + _AttackManager._ballAndShieldOffsetToTargetTransform, Quaternion.identity);
                   
                }
                ishalfwayreached = false;
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
        
        return result;

    }

    /// <summary>
    /// Cannon look at the Target and Instantiate the Bullet Prefab
    /// </summary>
    /// <param name="tran"></param>
    public void AssignPos(Transform tran)
    {
        
        _TargetTransform = tran;
        this.transform.position = new Vector3(_TargetTransform.position.x, CannonAttackPosition.y, CannonAttackPosition.z);
        this.gameObject.SetActive(true);
        Invoke("ShootBullet", 2.5f);
    }

    public void ShootBullet()
    {
        _bullet = Instantiate(_bulletPrefab, _shotPoint.position, Quaternion.identity);
        _bullet.GetComponent<BallLaunch>().target = _TargetTransform;     
          Invoke("FollowCamera", .1f);
    }


    public void FollowCamera()
    {
        Camera.main.transform.parent = _bullet.transform;
        CannonTargetDistance = Vector3.Distance(this.gameObject.transform.position, _TargetTransform.position);
        fixCameraRot = true; 
    }

    
}
