using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour {

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "HitWith")
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("Hit");


        }
    }
}
