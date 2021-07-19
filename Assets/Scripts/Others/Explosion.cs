using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    [SerializeField] private GameObject parent;

    private AudioSource _audioSource;
    private ObjectPool _explosionPool;
    private bool _initialized = false;



    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _explosionPool = parent.transform.parent.GetComponent<ObjectPool>();
    }

    private void Start() {
        _initialized = true;
    }

    public void DisableObject() {
        parent.gameObject.SetActive(false);
    }


    private void OnDisable() {
        if (!_initialized) return;

        _explosionPool.ReturnObject(parent.gameObject);
    }

    private void OnEnable() {
        if (!AudioManager.instance.IsEffectsOn) {
            _audioSource.enabled = false;
        }
        else {
            _audioSource.enabled = true; ;
        }
    }

}
