using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMonsterExplosion : MonoBehaviour {

    private ObjectPool _pool;
    private Animator _anim;
    private bool _initialized = false;



    private void Awake() {
        _anim = GetComponent<Animator>();
        _pool = transform.parent.GetComponent<ObjectPool>();
    }

    private void Start() {
        _initialized = true;
    }



    private void OnEnable() {
        if (!_initialized) return;

        _anim.Play("explodeMonsterExplosion", 0, 0f);
    }


    private void OnDisable() {
        if (!_initialized) return;

        _pool.ReturnObject(this.gameObject);
    }



    private void DisableObject() {
        this.gameObject.SetActive(false);
    }


}
