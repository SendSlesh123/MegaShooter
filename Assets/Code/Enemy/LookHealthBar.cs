using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookHealthBar : MonoBehaviour
{
     Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.LookAt(_player.position);
    }
}
