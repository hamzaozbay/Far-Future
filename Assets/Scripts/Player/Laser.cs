using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _speed = 20f;

    private Rigidbody2D _rb;
    private float _curveTime = 0f;
    private ObjectPool _laserPool;
    private Animator _anim;
    private bool _canMove = true;
    private bool _initialized = false;



    private void Start() {
        _initialized = true;
        _anim = GetComponent<Animator>();
        _laserPool = this.transform.parent.GetComponent<ObjectPool>();
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update() {
        if (!_canMove) return;

        _curveTime += Time.deltaTime;
        _rb.velocity = transform.right * _speed * _curve.Evaluate(_curveTime);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        _rb.velocity = Vector2.zero;
        _canMove = false;
        _anim.Play("laserSplash", 0, 0f);
    }

    public void DisableObject() {
        this.gameObject.SetActive(false);
    }


    private void OnBecameInvisible() {
        this.gameObject.SetActive(false);
    }


    private void OnDisable() {
        if (_laserPool != null) {
            _laserPool.ReturnObject(this.gameObject);
        }
    }

    private void OnEnable() {
        if (!_initialized) return;

        _canMove = true;
        _anim.Play("laserFire", 0, 0f);
    }

}
