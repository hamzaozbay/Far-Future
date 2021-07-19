using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitEnemyBullet : MonoBehaviour {

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _speed = 10f;

    private Rigidbody2D _rb;
    private float _curveTime;
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


    private void Update() {
        _curveTime += Time.deltaTime;
        _rb.velocity = transform.right * _speed * _curve.Evaluate(_curveTime);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        if (!AudioManager.instance.IsEffectsOn) {
            _audioSource.enabled = false;
        }
        else {
            _audioSource.enabled = true; ;
        }
    }

    private void OnBecameInvisible() {
        this.gameObject.SetActive(false);
    }

    private void OnDisable() {
        if (!_initialized) return;
        
        _curveTime = 0f;
        _rb.velocity = Vector2.zero;
        _pool.ReturnObject(this.gameObject);
    }

}
