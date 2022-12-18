using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManeser : MonoBehaviour
{

    public List<Transform> points = new List<Transform>();
    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxMonsters = 10;

    public GameObject monster;
    public float createTime = 3.0f;
    private bool isGameOver;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    public static GameManeser instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        CreateMonsterPool();
        Transform spawnPointGroup = GameObject.Find("SpawnPontGroup")?.transform;
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    void CreateMonsterPool()
    {
        for (int i = 0; i<maxMonsters; i++)
        {
            var _monster = Instantiate<GameObject>(monster);
            _monster.name = $"Monster_{i:00}";
            monsterPool.Add(_monster);
        }
    }
    void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);
        //Instantiate(monster, points[idx].position, points[idx].rotation);
        GameObject _monster = GetMonsterInPool();
        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
        _monster?.SetActive(true);
    }

    public GameObject GetMonsterInPool()
    {
        foreach (var _monster in monsterPool)
        {
            if (_monster.activeSelf == false)
            {
                return _monster;
            }
        }
        return null;
    }
}
