using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlantBullet : MonoBehaviour {

    [SerializeField] private float _speed = 6.5f, _downSpeed = 2.5f;
    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private ObjectPool _pool;
    private bool _initialized = false;



    private void Awake() {
        _pool = transform.parent.GetComponent<ObjectPool>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void Start() {
        _initialized = true;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("DestroyEnemy") || other.gameObject.CompareTag("Fall")) {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            this.gameObject.SetActive(false);
        }
    }



    private void OnDisable() {
        if (!_initialized) return;

        _rb.velocity = Vector2.zero;
        _pool.ReturnObject(this.gameObject);
    }

    private void OnEnable() {
        Fire();
    }

    private void Fire() {
        _rb.AddForce(transform.right * _speed + (-transform.up * _downSpeed), ForceMode2D.Impulse);

        if (!AudioManager.instance.IsEffectsOn) {
            _audioSource.enabled = false;
        }
        else {
            _audioSource.enabled = true; ;
        }

    }

}
