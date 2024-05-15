using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistols : Guns
{
    public Animator animator;
    public GameObject aim;
    Coroutine zoom;

    private void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();

                animator.SetTrigger("fire");

                numberBullets--;
                if (numberBullets <= 0)
                {
                    numberBullets = magazine;
                    shootTimer = reloadTime;
                }
                else
                {
                    shootTimer = shootInterval;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("aim", true);
            aim.SetActive(false);
            if(zoom != null)
            {
                StopCoroutine(zoom);
            }
            zoom = StartCoroutine(SetFov(50, 0.3f));
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("aim", false);
            aim.SetActive(true);
            if (zoom != null)
            {
                StopCoroutine(zoom);
            }
            zoom = StartCoroutine(SetFov(60, 0.3f));
        }
    }

    private IEnumerator SetFov(float value, float duration)
    {
        float counter = 0;
        float fromView = _camera.fieldOfView;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float viewTime = counter / duration;
            _camera.fieldOfView = Mathf.Lerp(fromView, value, viewTime);
            yield return null;
        }

    }
}
