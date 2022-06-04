using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject _EnemyPrefab;
    public GameObject _LakePrefab;
    public int _enemiesCount = 40;
    public int _lakeCount = 10;
    public int _score = 0;
    public int _scoreForSpawnEnemy = 300;
    public int _scoreForSpawnLake = 100;
    public int _scoreForSeconds = 30;
    public Text _scoreText;
    public GameObject _player;
    public float _maxDistanceToPlayer = 128f;

    private List<GameObject> enemies;
    private List<GameObject> lakes;
    private int enemyScoreTimer = 0;
    private int lakeScoreTimer = 0;

    private void Awake()
    {
        enemies = CreateUnits(_enemiesCount, _EnemyPrefab);
        lakes = CreateUnits(_lakeCount, _LakePrefab);
        StartCoroutine(EnemyController());
        StartCoroutine(LightLakeController());
    }

    private void Start()
    {
        StartCoroutine(ScoreTimer());
    }

    private IEnumerator LightLakeController()
    {
        while (_player != null)
        {
            if (lakeScoreTimer >= _scoreForSpawnLake)
            {
                for (int i = 0; i < lakes.Count; i++)
                {
                    if (lakes[i].activeSelf)
                    {
                        continue;
                    }
                    else
                    {
                        Vector3 spawnPoint = transform.position + (Random.insideUnitSphere + transform.position).normalized * _maxDistanceToPlayer / 2;
                        lakes[i].transform.position = spawnPoint;
                        lakes[i].SetActive(true);
                        lakeScoreTimer = 0;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    

    private IEnumerator ScoreTimer()
    {
        while(_player != null && Hero.S._life > 0)
        {
            _score += _scoreForSeconds;
            enemyScoreTimer += _scoreForSeconds;
            lakeScoreTimer += _scoreForSeconds;

            _scoreText.text = _score.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator EnemyController()
    {
        while (_player != null)
        {
            if (enemyScoreTimer >= _scoreForSpawnEnemy)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].activeSelf)
                    {
                        continue;
                    }
                    else
                    {
                        Vector3 spawnPoint = transform.position + (Random.insideUnitSphere + transform.position).normalized * _maxDistanceToPlayer / 3;
                        enemies[i].transform.position = spawnPoint;
                        enemies[i].SetActive(true);
                        enemyScoreTimer = 0;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }


    private List<GameObject> CreateUnits(int unitCount, GameObject prefab)
    {
        List<GameObject> createUnits = new List<GameObject>();
        for(int i = 0; i < unitCount; i++)
        {
            GameObject go = Instantiate<GameObject>(prefab);
            go.SetActive(false);
            createUnits.Add(go);
        }
        return createUnits;
    }
    
}
