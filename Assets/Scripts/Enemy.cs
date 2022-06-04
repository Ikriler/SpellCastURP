using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : GeneralPossible
{
    [Header("Set in Inspector")]
    public float _health = 300f;
    public float _maxHealth = 300f;
    public float _damagePerSecond = 5f;
    public float _speed;
    public float _speedRotation;
    public float _rangeFind = 7;
    public float _attackRange = 5;
    public float _maxDistanceToPlayer = 128;
    public int _scoreOnKill = 300;

    [HideInInspector]
    public GameObject _player;

    [HideInInspector]
    public bool triggered = false;

    private CharacterController cc;
    private IEnumerator attackCorutine;
    private Vector3 directionToPlayer;
    private IEnumerator colorCoroutine;
    private GameManager gameManager;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        colorCoroutine = ChangeColor(Color.black);
        StartCoroutine(colorCoroutine);
        cc = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_player == null) return;
        GameObject go = other.gameObject.transform.root.gameObject;
        Hero hero = go.GetComponent<Hero>();
        if (hero != null)
        {
            _player = go;
            if (attackCorutine == null)
            {
                attackCorutine = GoAttack();
                StartCoroutine(attackCorutine);
            }
        }
    }
    void Update()
    {
        if (_player == null) return;
        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer < _rangeFind)
        {
            triggered = true;
        }
        if(triggered)
        {
            HuntPlayer();
        }
        if(distanceToPlayer > 128)
        {
            gameObject.SetActive(false);
            Vector3 spawnPoint = _player.transform.position + (Vector3.zero + Random.insideUnitSphere).normalized * _maxDistanceToPlayer / 2;
            transform.position = spawnPoint;
            triggered = false;
            gameObject.SetActive(true);
        }
        if (_health < 0)
        {
            if(gameManager != null)
            {
                gameManager._score += _scoreOnKill;
            }
            StopCoroutine(colorCoroutine);
            GetComponent<TrailRenderer>().enabled = false;
            gameObject.SetActive(false);
        }
        StableY(0);
    }

    private IEnumerator GoAttack()
    {
        while(_player != null)
        {
            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            if (distanceToPlayer <= _attackRange && !Hero.S.invulnerability)
            {
                StopCoroutine(colorCoroutine);
                colorCoroutine = ChangeColor(Color.red);
                StartCoroutine(colorCoroutine);
                Hero.S._life -= _damagePerSecond;
                Debug.Log("Player damaged!");
            }
            else if(distanceToPlayer <= _attackRange && Hero.S.invulnerability)
            {
                StopCoroutine(colorCoroutine);
                colorCoroutine = ChangeColor(Color.blue);
                StartCoroutine(colorCoroutine);
                _health -= Hero.S._damageToEnemy;
                Debug.Log("Enemy damaged!");
            }
            else
            {
                StopCoroutine(colorCoroutine);
                colorCoroutine = ChangeColor(Color.black);
                StartCoroutine(colorCoroutine);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void HuntPlayer()
    {
        directionToPlayer = _player.transform.position - transform.position;
        if (Hero.S.invulnerability) directionToPlayer = -directionToPlayer;
        Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedRotation * Time.deltaTime);
        cc.Move(transform.forward * _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _rangeFind);
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private void OnEnable()
    {
        _health = _maxHealth;
        triggered = false;
        colorCoroutine = ChangeColor(Color.black);
        StartCoroutine(colorCoroutine);
        StartCoroutine(ActivateTrailRender(4));
    }

    private IEnumerator ActivateTrailRender(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponent<TrailRenderer>().enabled = true;
    }
}
