using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGuns : Guns
{
    public Animator animator;
    public GameObject aim;

    void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
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

    }
}
