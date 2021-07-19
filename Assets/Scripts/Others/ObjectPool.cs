using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int _poolSize = 10;
    private Queue<GameObject> _objectPool = new Queue<GameObject>();



    private void Start() {
        for (int i = 0; i < _poolSize; i++) {
            GameObject obj = Instantiate(objectPrefab, this.gameObject.transform);
            _objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }


    public GameObject GetObject() {
        if (_objectPool.Count > 0) {
            GameObject obj = _objectPool.Dequeue();
            return obj;
        }
        else {
            GameObject obj = Instantiate(objectPrefab, this.gameObject.transform);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj) {
        _objectPool.Enqueue(obj);
        obj.SetActive(false);
    }


    public void ReturnAll() {
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).gameObject.activeSelf) {
                ReturnObject(transform.GetChild(i).gameObject);
            }
        }
    }


}
