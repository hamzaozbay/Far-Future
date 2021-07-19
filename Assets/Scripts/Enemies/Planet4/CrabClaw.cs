using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabClaw : MonoBehaviour {

    [SerializeField] private float _speed = 4f;

    private ObjectPool _pool;
    private Animator _anim;
    private bool _canGo = false;
    private bool _initialized = false;



    private void Awake() {
        _anim = GetComponent<Animator>();
        _pool = this.transform.parent.GetComponent<ObjectPool>();
    }

    private void Start() {
        _initialized = true;
    }




    private void Update() {
        if (_canGo) {
            transform.position += transform.right * _speed * Time.deltaTime;
        }
    }


    private void Attack() {
        _canGo = true;
        _anim.Play("ClawAttack", 0, 0f);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("DestroyEnemy")) {
            this.gameObject.SetActive(false);
        }
    }


    private void OnEnable() {
        if (!_initialized) return;

        _canGo = false;
        _anim.Play("ClawGrow", 0, 0f);
    }

    private void OnDisable() {
        if (!_initialized) return;

        _pool.ReturnObject(this.gameObject);
    }



}
