using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Parabolic Missile
/// < para > Calculating trajectory and steering </para >
/// <para>ZhangYu 2019-02-27</para>
/// </summary>
public class BallLaunch : MonoBehaviour
{

    public Transform target; //target
    public AttackManager _attackManager;
    public float hight = 16f; // parabolic height
    public float gravity = 9.8f; // gravitational acceleration
    private GameObject _bullet;
    private Vector3 position; //My position
    private Vector3 dest; //Target location
    private Vector3 Velocity; //Motion Velocity
    private float time = 0; // Motion time
    public bool BallFlow = true;
    public bool BallReverse = false;
    public GameObject CrackCanvas;
    public float ShieldCameraDistance;

    
     
    public void Awake()
    {
        
    }

    private void Start()
    {
        _attackManager = GameObject.Find("AttackManager").GetComponent<AttackManager>();
        CrackCanvas = GameObject.Find("CrackCanvas");
        dest = target.position + _attackManager._ballAndShieldOffsetToTargetTransform;
        position = transform.position;
        Velocity = PhysicsUtil.GetParabolaInitVelocity(position, dest, gravity, hight, 0);
        transform.LookAt(PhysicsUtil.GetParabolaNextPosition(position, Velocity, gravity, Time.deltaTime));
    }

    private void FixedUpdate()
    {
        if (BallFlow == true)
        {
            // Computational displacement
            float deltaTime = Time.deltaTime;
            position = PhysicsUtil.GetParabolaNextPosition(position, Velocity, gravity, deltaTime);
            transform.position = position;
            time += deltaTime;
            Velocity.y += gravity * deltaTime;

            // Computational steering
            transform.LookAt(PhysicsUtil.GetParabolaNextPosition(position, Velocity, gravity, deltaTime));
        }
        if (BallReverse == true)
        {
           // Vector3 Newdist = Camera.main.ScreenToWorldPoint(CrackCanvas.transform.GetChild(0).gameObject.transform.position);
           // Debug.Log(Newdist + "Panel Position");
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, Camera.main.transform.position, Time.deltaTime * 5);

            if (Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position) < (ShieldCameraDistance * .25)) //.14))
            {
                BallReverse = false;
             
                this.gameObject.transform.GetChild(6).gameObject.SetActive(true);
                //  CrackCanvas.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //Debug.Log(CrackCanvas);
                //Debug.Log(CrackCanvas.gameObject.transform.GetChild(0).gameObject + "child Panel name");
                //Debug.LogError("Ball Reverse Stopped");
                //Debug.Log(Camera.main.ScreenToWorldPoint (CrackCanvas.transform.position) + "  Camera Panel ScreenView");
            }
        }
      

        // Simply simulate collision detection
        // if (position.y <= dest.y) enabled = false;
    }

  /*  private void LateUpdate()
    {
        if (BallReverse == true)
        {
            // Vector3 Newdist = Camera.main.ScreenToWorldPoint(CrackCanvas.transform.GetChild(0).gameObject.transform.position);
            // Debug.Log(Newdist + "Panel Position");
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, Camera.main.transform.position, Time.deltaTime * 5);

            if (Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position) < (ShieldCameraDistance * .25)) //.14))
            {
                BallReverse = false;
                this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                //  CrackCanvas.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //Debug.Log(CrackCanvas);
                //Debug.Log(CrackCanvas.gameObject.transform.GetChild(0).gameObject + "child Panel name");
                //Debug.LogError("Ball Reverse Stopped");
                //Debug.Log(Camera.main.ScreenToWorldPoint (CrackCanvas.transform.position) + "  Camera Panel ScreenView");
            }
        }
    }
  */
    public void OnCollisionEnter(Collision col)
    {
       
        GameObject.Find("CANNON_ANIM_1").GetComponent<CannonShotController>().fixCameraRot = false;
        _bullet = this.gameObject;

        if (_attackManager._Shield == true)
        {
            if (col.gameObject.tag == "Shield Protection")
            {
                BallFlow = false;
                
                
                Camera.main.transform.parent = null;
               // this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
              //  this.gameObject.GetComponent<Rigidbody>().useGravity = false;
               


                //BallReverse = true;
                //Invoke("BallReturnDelay", .5f);
                BallReverse = true;
                ShieldCameraDistance = Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position);
               
                //  this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, Camera.main.transform.position, Time.deltaTime);

             
            }
        }
        else if (_attackManager._Shield == false)
        {
            GameObject attackedBuilding = _attackManager._TargetTransform.gameObject;

            _bullet.transform.GetChild(6).transform.parent = null;
            Camera.main.transform.parent = null;


            for (int i = 0; i < _bullet.transform.childCount; i++)
            {
                _bullet.transform.GetChild(i).gameObject.SetActive(true);
            }
            // Camera.main.transform.parent = null;

            attackedBuilding.transform.position = new Vector3(attackedBuilding.transform.position.x, _attackManager._buildingSinkPositionAmount, attackedBuilding.transform.position.z);
            attackedBuilding.transform.rotation = Quaternion.Euler(attackedBuilding.transform.eulerAngles.x, attackedBuilding.transform.eulerAngles.y, _attackManager._buildingTiltRotationAmount);
            Instantiate(_attackManager._destroyedSmokeEffectVFX, attackedBuilding.transform.position, Quaternion.identity, attackedBuilding.transform);

            while (_bullet.transform.childCount > 0)
            {
                foreach (Transform child in _bullet.transform)
                {
                    child.gameObject.transform.parent = null;
                }
            }

            _bullet.SetActive(false);
        }
    }

    public void BallReturnDelay()
    {
        BallReverse = true;
    }

}

//if (_attackManager._Shield == true)
//{
//    Debug.Log("Shield Activated");

//    for (int i = 0; i < _bullet.transform.childCount-2; i++)
//    {
//        _bullet.transform.GetChild(i).gameObject.SetActive(true);
//        _bullet.transform.GetChild(i).parent = null;

//        Debug.Log(_bullet.transform.GetChild(i).gameObject.name);
//    }
//    /* _bullet.transform.GetChild(0).gameObject.SetActive(true);
//     _bullet.transform.GetChild(1).gameObject.SetActive(true);
//     _bullet.transform.GetChild(0).parent = null;
//     _bullet.transform.GetChild(1).parent = null;
//    */
//}
//else
//{
//    Debug.Log("Shield Disabled");
//    Debug.Log(_bullet.transform.childCount + "Child Count");
//    for(int i=0; i < _bullet.transform.childCount; i++)
//    {
//        _bullet.transform.GetChild(i).gameObject.SetActive(true);
//        _bullet.transform.GetChild(i).parent = null;

//        Debug.Log(_bullet.transform.GetChild(i).gameObject.name);
//    }
//    /*  _bullet.transform.GetChild(0).gameObject.SetActive(true);
//      _bullet.transform.GetChild(1).gameObject.SetActive(true);
//      _bullet.transform.GetChild(2).gameObject.SetActive(true);
//      _bullet.transform.GetChild(3).gameObject.SetActive(true);
//      _bullet.transform.GetChild(0).parent = null;
//      _bullet.transform.GetChild(1).parent = null;
//      _bullet.transform.GetChild(2).parent = null;
//      _bullet.transform.GetChild(3).parent = null;
//    */
//}
//Camera.main.transform.parent = null;

//_bullet.SetActive(false);

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

///// <summary>
///// Parabolic Missile
///// < para > Calculating trajectory and steering </para >
///// <para>ZhangYu 2019-02-27</para>
///// </summary>
//public class BallLaunch : MonoBehaviour
//{

//    public Transform target; //target
//    public AttackManager _attackManager;
//    public float hight = 16f; // parabolic height
//    public float gravity = 9.8f; // gravitational acceleration
//    private GameObject _bullet;
//    private Vector3 position; //My position
//    private Vector3 dest; //Target location
//    private Vector3 Velocity; //Motion Velocity
//    private float time = 0; // Motion time
//    public bool BallFlow = true;
//    public bool BallReverse = false;
//    public GameObject CrackCanvas;
//    private float ShieldCameraDistance;

//    private Vector3 _ballOffseToHitTargetTransform = new Vector3(0, 250, 0);

//    public void Awake()
//    {

//    }

//    private void Start()
//    {
//        _attackManager = GameObject.Find("AttackManager").GetComponent<AttackManager>();
//        CrackCanvas = GameObject.Find("CrackCanvas");
//        dest = target.position + _ballOffseToHitTargetTransform;
//        position = transform.position;
//        Velocity = PhysicsUtil.GetParabolaInitVelocity(position, dest, gravity, hight, 0);
//        transform.LookAt(PhysicsUtil.GetParabolaNextPosition(position, Velocity, gravity, Time.deltaTime));
//    }

//    private void FixedUpdate()
//    {
//        if (BallFlow == true)
//        {
//            // Computational displacement
//            float deltaTime = Time.deltaTime;
//            position = PhysicsUtil.GetParabolaNextPosition(position, Velocity, gravity, deltaTime);
//            transform.position = position;
//            time += deltaTime;
//            Velocity.y += gravity * deltaTime;

//            // Computational steering
//            transform.LookAt(PhysicsUtil.GetParabolaNextPosition(position, Velocity, gravity, deltaTime));
//        }
//        if (BallReverse == true)
//        {
//            // Vector3 Newdist = Camera.main.ScreenToWorldPoint(CrackCanvas.transform.GetChild(0).gameObject.transform.position);
//            // Debug.Log(Newdist + "Panel Position");
//            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, Camera.main.transform.position, Time.deltaTime * 5);

//            if (Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position) < (ShieldCameraDistance * .25)) //.14))
//            {
//                BallReverse = false;
//                this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
//                //  CrackCanvas.gameObject.transform.GetChild(0).gameObject.SetActive(true);
//                //Debug.Log(CrackCanvas);
//                //Debug.Log(CrackCanvas.gameObject.transform.GetChild(0).gameObject + "child Panel name");
//                //Debug.LogError("Ball Reverse Stopped");
//                //Debug.Log(Camera.main.ScreenToWorldPoint (CrackCanvas.transform.position) + "  Camera Panel ScreenView");
//            }
//        }


//        // Simply simulate collision detection
//        // if (position.y <= dest.y) enabled = false;
//    }

//    /*  private void LateUpdate()
//      {
//          if (BallReverse == true)
//          {
//              // Vector3 Newdist = Camera.main.ScreenToWorldPoint(CrackCanvas.transform.GetChild(0).gameObject.transform.position);
//              // Debug.Log(Newdist + "Panel Position");
//              this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, Camera.main.transform.position, Time.deltaTime * 5);

//              if (Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position) < (ShieldCameraDistance * .25)) //.14))
//              {
//                  BallReverse = false;
//                  this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
//                  //  CrackCanvas.gameObject.transform.GetChild(0).gameObject.SetActive(true);
//                  //Debug.Log(CrackCanvas);
//                  //Debug.Log(CrackCanvas.gameObject.transform.GetChild(0).gameObject + "child Panel name");
//                  //Debug.LogError("Ball Reverse Stopped");
//                  //Debug.Log(Camera.main.ScreenToWorldPoint (CrackCanvas.transform.position) + "  Camera Panel ScreenView");
//              }
//          }
//      }
//    */
//    public void OnCollisionEnter(Collision col)
//    {

//        GameObject.Find("CANNON_ANIM_1").GetComponent<CannonShotController>().fixCameraRot = false;
//        _bullet = this.gameObject;

//        if (_attackManager._Shield == true)
//        {
//            if (col.gameObject.tag == "Shield Protection")
//            {
//                BallFlow = false;


//                Camera.main.transform.parent = null;
//                // this.gameObject.GetComponent<Rigidbody>().useGravity = false;
//                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
//                //  this.gameObject.GetComponent<Rigidbody>().useGravity = false;



//                //BallReverse = true;
//                //Invoke("BallReturnDelay", .5f);
//                BallReverse = true;
//                ShieldCameraDistance = Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position);

//                //  this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, Camera.main.transform.position, Time.deltaTime);


//            }
//        }
//        else if (_attackManager._Shield == false)
//        {

//            GameObject attackedBuilding = _attackManager._TargetTransform.gameObject;
//            _bullet.transform.GetChild(4).transform.parent = null;
//            Camera.main.transform.parent = null;




//            for (int i = 0; i < _bullet.transform.childCount; i++)
//            {

//                _bullet.transform.GetChild(i).gameObject.SetActive(true);
//                //  _bullet.transform.GetChild(i).parent = null;
//            }

//            attackedBuilding.transform.position = new Vector3(attackedBuilding.transform.position.x, _attackManager._buildingSinkPositionAmount, attackedBuilding.transform.position.z);
//            attackedBuilding.transform.rotation = Quaternion.Euler(attackedBuilding.transform.eulerAngles.x, attackedBuilding.transform.eulerAngles.y, _attackManager._buildingTiltRotationAmount);
//            Instantiate(_attackManager._destroyedSmokeEffectVFX, attackedBuilding.transform.position, Quaternion.identity, attackedBuilding.transform);

//            // Tilt the building.
//            // Camera.main.transform.parent = null;
//            while (_bullet.transform.childCount > 0)
//            {
//                foreach (Transform child in _bullet.transform)
//                {
//                    child.gameObject.transform.parent = null;
//                }
//            }


//            _bullet.SetActive(false);
//            //_bullet.SetActive(false);
//        }

//        // Camera.main.transform.parent = null;

//        // _bullet.SetActive(false);
//    }

//    public void BallReturnDelay()
//    {
//        BallReverse = true;
//    }

//}

////if (_attackManager._Shield == true)
////{
////    Debug.Log("Shield Activated");

////    for (int i = 0; i < _bullet.transform.childCount-2; i++)
////    {
////        _bullet.transform.GetChild(i).gameObject.SetActive(true);
////        _bullet.transform.GetChild(i).parent = null;

////        Debug.Log(_bullet.transform.GetChild(i).gameObject.name);
////    }
////    /* _bullet.transform.GetChild(0).gameObject.SetActive(true);
////     _bullet.transform.GetChild(1).gameObject.SetActive(true);
////     _bullet.transform.GetChild(0).parent = null;
////     _bullet.transform.GetChild(1).parent = null;
////    */
////}
////else
////{
////    Debug.Log("Shield Disabled");
////    Debug.Log(_bullet.transform.childCount + "Child Count");
////    for(int i=0; i < _bullet.transform.childCount; i++)
////    {
////        _bullet.transform.GetChild(i).gameObject.SetActive(true);
////        _bullet.transform.GetChild(i).parent = null;

////        Debug.Log(_bullet.transform.GetChild(i).gameObject.name);
////    }
////    /*  _bullet.transform.GetChild(0).gameObject.SetActive(true);
////      _bullet.transform.GetChild(1).gameObject.SetActive(true);
////      _bullet.transform.GetChild(2).gameObject.SetActive(true);
////      _bullet.transform.GetChild(3).gameObject.SetActive(true);
////      _bullet.transform.GetChild(0).parent = null;
////      _bullet.transform.GetChild(1).parent = null;
////      _bullet.transform.GetChild(2).parent = null;
////      _bullet.transform.GetChild(3).parent = null;
////    */
////}
////Camera.main.transform.parent = null;

////_bullet.SetActive(false);