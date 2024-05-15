using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    [SerializeField] private Slider _healthBar;

    private NavMeshAgent _agent;
    private EnemyMove _move;
    private void Start()
    {
        LevelCotroller.singleton.enemies.Add(this.gameObject);
        _agent = GetComponent<NavMeshAgent>();
        _move = GetComponent<EnemyMove>();
        _health = 100;
        _maxHealth = 100;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _health;

    }
    public void ReactToHit(int damage)
    {
        _health -= damage;
        _healthBar.value = _health;
        if (_health <= 0)
        {
            _healthBar.gameObject.SetActive(false);
            print("СМЕРТЬ!!!");
            _move.enabled = false;
            _agent.enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            LevelCotroller.singleton.enemies.Remove(this.gameObject);
            Destroy(gameObject, 0.5f);
        }
        else
        {
            Debug.LogFormat($"Попадание! Осталось {_health} здоровья.");
        }
    }

    
}
