using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] GameObject _hitEffect;


    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit; // структура данных хранящая в себе информацию о пересечении луча
            if(Physics.Raycast(ray, out hit))
            {
                // Debug.Log("Hit " + hit.point); // если произошло столкновение то появляются координаты где это произошло
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    //Debug.Log("Target hit");
                    target.ReactToHit(30);
                    Quaternion targetRotation = Quaternion.LookRotation(transform.position);
                    GameObject effect = Instantiate(_hitEffect, hit.point, targetRotation);
                    effect.transform.parent = target.transform;
                    Destroy(effect, 1);

                    /*
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = hit.point;
                    sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    sphere.GetComponent<MeshRenderer>().material.color = Color.red;
                    sphere.transform.parent = target.transform;
                    Destroy(sphere, 1);
                    */

                }
                /*
                else
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = hit.point;
                    Destroy(sphere, 1);
                }
                */
            }
        }
    }

}
