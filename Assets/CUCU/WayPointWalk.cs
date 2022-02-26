using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WayPointWalk : MonoBehaviour
{
    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    public float _walkSpeed = 30f;

    public float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = true;

    public Animator anim;

    public Transform camera;
    bool changeIndex;
    private void Start()
    {  
        anim.SetBool("Walking", true);
        _waiting = false;
    }

    private void Update()
    {
        if (_waiting)
        {
            transform.LookAt(camera.position);

            if(changeIndex)
            {
                anim.SetInteger("DanceIndex", Random.Range(0, 8)); // always 1 extra value for animation using random function
                changeIndex = false;         
            }
            anim.SetBool("Walking", false);
            anim.SetBool("Dance", true);
           
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
            {
                return;
            }
            _waiting = false;
        }

        if(_waiting==false)
        {
           anim.SetBool("Dance", false);
            anim.SetBool("Walking", true);
        }
        Transform waypoint = waypoints[_currentWaypointIndex];
        if (Vector3.Distance(transform.position, waypoint.position) < 0.01f)
        {
            transform.position = waypoint.position;
            _waitCounter = 0f;
            _waiting = true;
            changeIndex = true;
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,waypoint.position,_walkSpeed * Time.deltaTime);
            transform.LookAt(waypoint.position);
        }
    }
}


