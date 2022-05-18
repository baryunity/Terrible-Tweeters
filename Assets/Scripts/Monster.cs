using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    
    bool _hasDied;
    Rigidbody2D _rb;
    static float _amt0 = -1.15f;
    static float _amt3 = -1.50f;
    static bool _oneShot0 = true;
    static bool _oneShot3 = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Monster0")
        {
            //print(gameObject.transform.position.x);
            if (gameObject.transform.position.x < -9f || gameObject.transform.position.x > -1.72f)
                if (_oneShot0)
                {
                    _amt0 = -_amt0;
                    _amt3 = -_amt3;
                    _oneShot0 = false;
                }
            _rb.velocity = new Vector2(_amt0, 0f);
            _oneShot0 = true;
        }
        if (gameObject.tag == "Monster3")
        {
            //print(gameObject.transform.position.x);
            if (gameObject.transform.position.x < -10f || gameObject.transform.position.x > -2.5f)
                if (_oneShot3)
                {
                    _amt0 = -_amt0;
                    _amt3 = -_amt3;
                    _oneShot3 = false;
                }
            _rb.velocity = new Vector2(_amt3, 0f);
            _oneShot3 = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (collision.contacts[0].normal.y < -0.5f)
            return true;

        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
}
