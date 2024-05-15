using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private GameObject _target;

    private void Awake()
    {
        _target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        _agent.SetDestination(_target.transform.position);
        if(_agent.remainingDistance <= 2f)
        {
            Vector3 direction = _target.transform.position - transform.position;
            direction.y = 0f; 
            // Поворачиваем в сторону игрока
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 5 * Time.deltaTime);
        }
       
    }
}
