using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffExplosion : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private ObjectPool _pool;
    private bool _initialized = false;



    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pool = transform.parent.GetComponent<ObjectPool>();
    }


    private void Start() {
        _initialized = true;
        AudioManager.instance.PlayRandomSound("Puff");
    }


    public void DisableObject() {
        this.gameObject.SetActive(false);
    }


    private void OnDisable() {
        if (!_initialized) return;

        _pool.ReturnObject(this.gameObject);
    }

    private void OnEnable() {
        if (!_initialized) return;

        AudioManager.instance.PlayRandomSound("Puff");
    }



    public Color TintColor {
        get { return _spriteRenderer.color; }
        set { _spriteRenderer.color = value; }
    }

}
