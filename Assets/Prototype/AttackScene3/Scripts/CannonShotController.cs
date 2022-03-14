using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{

    public Rigidbody  _bullet;
    public Transform _TargetTransform;
    public AttackManager _AttackManager;
    public Vector3 CannonAttackPosition;
    public bool fixCameraRot = false;
    public bool Halfwayreached = true;
    public bool ishalfwayreached = true;
    float CannonTargetDistance;
    float BulletSpeed = 0f;
    float deceleration = 125;
    public GameObject shieldPref;

    public Vector3 cameraTargetPos;
    [SerializeField] Animator ballAnimation;
    [SerializeField] GameObject coinParticle;
    [SerializeField] GameObject energyParticle;


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
    public void AssignPos(Transform tran)
    {
        
        _TargetTransform = tran;
        this.transform.position = new Vector3(_TargetTransform.position.x, CannonAttackPosition.y, _TargetTransform.position.z-1000);//427 CannonAttackPosition.y, CannonAttackPosition.z);
        this.gameObject.SetActive(true);
        if (_AttackManager.reward > 100)
        {
            coinParticle.SetActive(true);
        }
        else
        {
            energyParticle.SetActive(true);
        }
        ballAnimation.SetBool("SHIELDED", _AttackManager._Shield);
       // Invoke("ShootBullet", 2.5f);
    }

    public void ShootBullet()
    {
        if (_AttackManager._Shield)
            ballAnimation.Play("ATTACK");
        else
            ballAnimation.Play("SHIELD");
    }


    public void FollowCamera()
    {
        Camera.main.transform.parent = _bullet.transform;
        CannonTargetDistance = Vector3.Distance(this.gameObject.transform.position, _TargetTransform.position);
        fixCameraRot = true; 
    }

    
}
