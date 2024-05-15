using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimations : MonoBehaviour
{
    public Animator animator;
    public GameObject aim;
    public GameObject fireEffect;
    [SerializeField] Transform _shootPointPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("fire");
            GameObject effect = Instantiate(fireEffect, _shootPointPos.position, transform.rotation);
            effect.transform.parent = gameObject.transform;
            Destroy(effect, 0.05f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("aim", true);
            aim.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("aim", false);
            aim.SetActive(true);
        }
    }
}
