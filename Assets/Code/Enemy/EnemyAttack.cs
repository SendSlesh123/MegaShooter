using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private int _damage;
    [SerializeField] private float _hitInterval;
    [SerializeField] private float _hitTimer;

    private void Start()
    {
        _damage = LevelCotroller.singleton.level * 5 + 3;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        { 
            if (_hitTimer <= 0)
            {
                other.GetComponent<PlayerHealth>().TakeDamage(_damage);
                _hitTimer = _hitInterval;
            }
        }
    }

    private void Update()
    {
        if (_hitTimer > 0)
        {
            _hitTimer -= Time.deltaTime;
        }
    }
}
