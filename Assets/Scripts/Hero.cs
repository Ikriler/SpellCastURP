using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : GeneralPossible
{
    [Header("Set in Inspector")]
    public float _speed = 2f;
    public float _life = 300;
    public Joystick _joystick;
    public float _damageInSecond = 4f;
    public float _damageToEnemy = 20f;
    public GameObject deathScreen;

    [HideInInspector]
    public float maxLife;
    [HideInInspector]
    public bool invulnerability = false;

    private CharacterController cc;
    private IEnumerator colorCoroutine;

    public static Hero S;

    public void GoInvul()
    {
        if (!invulnerability)
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = ChangeColor(Color.red);
            StartCoroutine(colorCoroutine);
            _speed *= 1.5f;
            _damageInSecond *= 10;
            invulnerability = true;
        }
        else
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = ChangeColor(Color.yellow);
            StartCoroutine(colorCoroutine);
            _speed /= 1.5f;
            _damageInSecond /= 10;
            invulnerability = false;
        }
    }

    private void Awake()
    {
        Hero.S = this;
        colorCoroutine = ChangeColor(Color.yellow);
        StartCoroutine(colorCoroutine);
        maxLife = _life;
        cc = GetComponent<CharacterController>();
        StartCoroutine(extinction());
    }

    void Update()
    {
        Move();

        if (_life > maxLife) _life = maxLife;

        if (_life <= 0)
        {
            deathScreen.SetActive(true);
            //Destroy(gameObject);
        }

        StableY(0.4f);
    }

    private IEnumerator extinction()
    {
        while(_life > 0)
        {
            _life -= _damageInSecond;
            yield return new WaitForSeconds(1);
        }
    }
    private void Move()
    {
        float newPosX = _joystick.Horizontal;
        float newPosY = _joystick.Vertical;

        Vector3 tempPos = Vector3.zero;
        tempPos.x = newPosX * _speed * Time.deltaTime;
        tempPos.z = newPosY * _speed * Time.deltaTime;

        cc.Move(tempPos);
    }

    public Vector3 currentPosition
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
}
