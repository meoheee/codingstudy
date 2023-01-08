using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManeser : MonoBehaviour
{

    public List<Transform> points = new List<Transform>();
    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxMonsters = 10;

    public GameObject monster;
    public float createTime = 3.0f;
    private bool isGameOver;
    public TMP_Text scoreText;
    private int totalScore=0;

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
        totalScore = PlayerPrefs.GetInt("Total_Score", 0);
        DisplayScore(0);
    }

    void CreateMonsterPool()
    {
        for (int i = 0; i<maxMonsters; i++)
        {
            var _monster = Instantiate<GameObject>(monster);
            _monster.name = $"Monster_{i:00}";
            _monster.SetActive(false);
            monsterPool.Add(_monster);
        }
    }
    void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);
        //Instantiate(monster, points[idx].position, points[idx].rotation);
        GameObject _monster = GetMonsterInPool();
        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
        //_monster?.Monsster.state = Monsster.State.IDLE;
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

    public void DisplayScore(int score)
    {
        totalScore += score;
        scoreText.text = $"<color=#00ff00>SCORE :</color><color=#f00000> {totalScore:#,##0}</color>";
        PlayerPrefs.SetInt("Total-Score", totalScore);
    }
}
