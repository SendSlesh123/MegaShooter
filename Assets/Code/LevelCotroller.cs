using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelCotroller : MonoBehaviour
{
    public static LevelCotroller singleton;

    public List<Spawner> spawners = new List<Spawner>();
    public List<GameObject> enemies = new List<GameObject>();

    public int level;
    public int remainingEnemies { get; private set; }
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _spawnInterval;

    private UI _UI;
    public bool finished;

   private void Awake()
    {
        singleton = this;
        level = PlayerPrefs.GetInt("Level", 1);
    }
    
    void Start()
    {
        _UI = GameObject.FindFirstObjectByType<UI>();
        finished = false;
        //level = 1;
        remainingEnemies = level * 2 + 10;
        //_remainingEnemies = 0;
    }

    private void Update()
    {
        if (finished != true)
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0 && remainingEnemies > 0)
            {
                int count = Random.Range(1, Mathf.Min(5, remainingEnemies));
                for (int j = 0; j < count; j++)
                {
                    int i = Random.Range(0, spawners.Count - 1);
                    spawners[i].Spawn(enemyPrefab);
                }

                _spawnTimer = _spawnInterval;
                remainingEnemies -= count;
            }
            if (enemies.Count <= 0 && remainingEnemies <= 0)
            {
                print(enemies.Count);
                print("Поздравляю с блестательной победой! Начало новой волны " + level);
                level++;
                PlayerPrefs.SetInt("Level", level);
                _UI.ShowDefeatPanel();
                finished = true;
            }
        }
    }
}
