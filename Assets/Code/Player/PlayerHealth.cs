using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;

    void Start()
    {
        maxHealth = 100;
        health = 100;
    }

    public void TakeDamage(int value)
    {
        health -= value;
        if(health <= 0)
        {
            GameObject.FindObjectOfType<UI>().ShowDefeatPanel();
        }
    }
}
