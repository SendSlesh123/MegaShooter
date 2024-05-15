using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns: MonoBehaviour
{
    public float shootTimer, shootInterval, reloadTime;
    public int numberBullets, magazine, attackDamage;

    public Camera _camera;
    public GameObject hitEffect;

    public GameObject fireEffect;
    public Transform shootPointPos;

    public void ShowFireEffect()
    {
        GameObject effect = Instantiate(fireEffect, shootPointPos.position, transform.rotation);
        effect.transform.parent = gameObject.transform;
        Destroy(effect, 0.05f);
    }


    public void Shoot()
    {
        ShowFireEffect();
        Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit; // структура данных хранящая в себе информацию о пересечении луча
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                target.ReactToHit(attackDamage);
                Quaternion targetRotation = Quaternion.LookRotation(transform.position);
                GameObject effect = Instantiate(hitEffect, hit.point, targetRotation);
                effect.transform.parent = target.transform;
                Destroy(effect, 1);
            }
        }
    }
}
