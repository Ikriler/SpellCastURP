using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLake : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float _healInSecond = 30f;
    public float _radius = 4;
    public int _maxSeconds = 10;
    public float _maxDistanceToPlayer = 128f;

    private IEnumerator healCorutine;
    private GameObject _player;
    private Hero hero;
    private int _seconds = 10;

    private void Awake()
    {
        _seconds = _maxSeconds;
        _player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<SphereCollider>().radius = _radius;
        healCorutine = Heal();
    }

    private void Start()
    {
        hero = Hero.S;
    }

    private void Update()
    {
        if (_seconds <= 0) gameObject.SetActive(false);

        if (_player == null) return;

        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);

        if (distanceToPlayer > _maxDistanceToPlayer)
        {
            Vector3 spawnPoint = _player.transform.position + (Vector3.zero + Random.insideUnitSphere).normalized * _maxDistanceToPlayer / 2;
            transform.position = spawnPoint;
        }
        StableY(-0.2f);
    }


    private void StableY(float y)
    {
        Vector3 stablePos = transform.position;
        stablePos.y = y;
        transform.position = stablePos;
    }

    private void OnEnable()
    {
        _seconds = _maxSeconds;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject.transform.root.gameObject;
        if (go == null || go.tag != "Player") return;

        if (hero.maxLife < hero._life) return;

        StartCoroutine(healCorutine);
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject.transform.root.gameObject;

        if (go == null || go.tag != "Player") return;

        StopCoroutine(healCorutine);
    }

    private IEnumerator Heal()
    {
        for(int i = 0; i < _seconds; _seconds--)
        {
            if(hero != null && hero._life < hero.maxLife)
            {
                hero._life += _healInSecond;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
