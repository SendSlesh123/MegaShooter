using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private void Start()
    {
        LevelCotroller.singleton.spawners.Add(this);
    }


    public void Spawn(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
