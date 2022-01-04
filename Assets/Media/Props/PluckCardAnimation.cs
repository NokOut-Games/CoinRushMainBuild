using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluckCardAnimation : MonoBehaviour
{
    private Animator anim;

    private IEnumerator Start()
    {
        anim = GetComponent<Animator>();

        while(true)
        {
            yield return new WaitForSeconds(5f);
            anim.SetInteger("IdleIndex", Random.Range(0, 2));
            anim.SetTrigger("Idle");
        }
    }
}
