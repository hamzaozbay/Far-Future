using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaBossLaser : MonoBehaviour {

    [SerializeField] private float _speed = 2f;

    private Animator _anim;
    private ObjectPool _pool;
    private bool _canGo = false;
    private bool _initialized = false;



    private void Awake() {
        _anim = this.GetComponent<Animator>();
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


    public void LookAtPlayer() {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        this.transform.right = playerPos - this.transform.position;
    }


    private void CanGoTrue() {
        _canGo = true;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Fall") || other.gameObject.CompareTag("DestroyEnemy")) {
            this.gameObject.SetActive(false);
        }
    }



    private void OnEnable() {
        if (!_initialized) return;

        _canGo = false;
        _anim.Play("Laser1", 0, 0f);
    }
    private void OnDisable() {
        if (!_initialized) return;

        _pool.ReturnObject(this.gameObject);
    }



}
